using UnityEngine;

public enum ShootPhase
{
    None,
    SelectPosition,
    SelectPower,
    Rolling
}

public class ShootInput : MonoBehaviour
{
    private BallHandler ballHandler;
    private ShootIndicator shootIndicator;

    private Golfball trackedBall;

    private Camera camera;
    private Vector3 SelectedPoint;

    public ShootPhase phase;

    [Tooltip("The scalar to multiply against the normalized power value between 0 and 1")]
    public float PowerModifier;

    [Tooltip("The scalar to muliply against the power to apply force to the rigidbody")]
    public float ShootForceModifier;

    private void Start()
    {
        ballHandler = Resolver.Resolve<BallHandler>();
        shootIndicator = Resolver.Resolve<ShootIndicator>();

        // TODO: Correctly resolve camera...
        camera = Camera.main;

        // hack
        SetPhase(ShootPhase.None);
    }

    private bool TryResolveMousePoint(out Vector3 point)
    {
        var camRay = camera.ScreenPointToRay(Input.mousePosition);

        var plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(camRay, out float enter))
        {
            var hitPoint = camRay.GetPoint(enter);

            // Match the Y level
            var srcPoint = transform.position;
            srcPoint.y = hitPoint.y;

            point = hitPoint;
            return true;
        }

        point = default;
        return false;
    }

    private void Update()
    {
        switch (phase)
        {
            case ShootPhase.None:
                {
                    // TODO: DOn't do this
                    SetPhase(ShootPhase.SelectPosition);
                }
                break;
            case ShootPhase.SelectPosition:
                {
                    if (!TryResolveMousePoint(out Vector3 worldMouse))
                    {
                        return;
                    }

                    // Wait for mouse down to begin input
                    if (Input.GetMouseButtonDown(0))
                    {
                        SelectedPoint = worldMouse; 
                        SetPhase(ShootPhase.SelectPower);
                    }
                }
                break;
            case ShootPhase.SelectPower:
                {
                    // It's possible to select the sky
                    if(!TryResolveMousePoint(out Vector3 worldMouse))
                    {
                        return;
                    }

                    // It's possible for there to be no ball
                    var currentBall = ballHandler.ResolveCurrentBall();
                    if(currentBall == null)
                    {
                        return;
                    }

                    float power = Mathf.Clamp(Vector3.Distance(SelectedPoint, worldMouse), 0.01f, PowerModifier);

                    Vector3 dir = -(worldMouse - SelectedPoint).normalized;

                    shootIndicator.SetOrigin(currentBall.transform.position);
                    shootIndicator.SetDirection(dir);
                    shootIndicator.SetPower(power);

                    // Perform fire when mouse is released
                    if (Input.GetMouseButtonUp(0))
                    {
                        Fire(dir, power);
                        SetPhase(ShootPhase.Rolling);
                    }
                }
                break;
            case ShootPhase.Rolling:
                {
                    if(trackedBall == null)
                    {
                        return;
                    }

                    // Wait for the ball to finish moving before allowing more movement
                    if (trackedBall.IsConsideredStationary())
                    {
                        // TODO: Notify next turn here...
                        SetPhase(ShootPhase.None);
                    }
                }
                break;
        }
    }

    private void Fire(Vector3 direction, float power)
    {
        Vector3 force = direction * power * ShootForceModifier;

        var currentBall = ballHandler.ResolveCurrentBall();
        if(currentBall != null)
        {
            currentBall.Fire(force);
            trackedBall = currentBall;
        }
    }

    private void SetPhase(ShootPhase phase)
    {
        this.phase = phase;

        switch (phase)
        {
            case ShootPhase.None:
            case ShootPhase.SelectPosition:
            case ShootPhase.Rolling:
                {
                    shootIndicator.Toggle(false);
                }
                break;
            case ShootPhase.SelectPower:
                {
                    shootIndicator.Toggle(true);
                }
                break;
        }
    }
}
