using UnityEngine;

public class ShootInput : MonoBehaviour
{
    private Camera camera;

    public ShootPhase phase;
    public Vector2 clickOrigin;
    public Vector2 inputDelta;

    private Vector3 SelectedPoint;
    private float SelectedPower;

    public float PowerModifier;
    public float ShootForceModifier;
    public Rigidbody Rigidbody;

    // hack
    public LineRenderer LineRenderer;
    public GameObject FireIndicator;

    private void Start()
    {
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

    private float GetPowerInput()
    {
        Vector3 mousePosition = Input.mousePosition;
        float height = Screen.height;

        float norm = (mousePosition.y / height) + 0.5f;

        return norm;
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

                    if (Input.GetMouseButtonDown(0))
                    {
                        SelectedPoint = worldMouse; 

                        SetPhase(ShootPhase.SelectPower);
                    }

                    Vector3 src = transform.position;
                    src.y = 0.0f;

                    //LineRenderer.SetPosition(0, src);
                    //LineRenderer.SetPosition(1, worldMouse);

                    FireIndicator.transform.position = worldMouse;
                }
                break;
            case ShootPhase.SelectPower:
                {
                    if(!TryResolveMousePoint(out Vector3 worldMouse))
                    {
                        return;
                    }

                    float power = Mathf.Clamp(Vector3.Distance(SelectedPoint, worldMouse), 0.01f, PowerModifier);

                    Vector3 dir = -(worldMouse - SelectedPoint).normalized;
                    LineRenderer.SetPosition(0, transform.position);
                    LineRenderer.SetPosition(1, transform.position + dir * power);

                    //float power = GetPowerInput();

                    if (Input.GetMouseButtonUp(0))
                    {
                        SelectedPower = power;

                        Fire(dir, power);
                        SetPhase(ShootPhase.Rolling);
                    }

                }
                break;
            case ShootPhase.Rolling:
                {
                    // Check for stop behaviour

                    // HACK
                    SetPhase(ShootPhase.None);
                }
                break;
        }
    }

    private Vector2 GetRelativeMousePosition()
    {
        float w = Screen.width;
        float h = Screen.height;

        Vector3 mouse = Input.mousePosition;

        return new Vector2(mouse.x / w, mouse.y / h);
    }

    private void Fire(Vector3 direction, float power)
    {
        Vector3 dir = direction * power * ShootForceModifier;
        Rigidbody.AddForce(dir, ForceMode.Impulse);

        //var cam = Camera.main;
        //var fwd = cam.transform.forward;
        //fwd.y = 0.0f;

        //var right = cam.transform.right;
        //right.y = 0.0f;

        //var dir = inputDelta; //.normalized;


        //Vector2 src = new Vector2(0.5f, 0.5f);
        //Vector2 cur = GetRelativeMousePosition();

        //Vector2 screenDirection = cur - src;

        //float ang = Vector2.Angle(Vector2.up, screenDirection);
        //Debug.Log(ang);

        //Vector3 start = transform.position;
        //Vector3 end = start + (fwd * screenDirection.y) + (right * screenDirection.x);

        //Vector3 worldDirection = (end - start).normalized;


        ////Vector3 angle = new Vector3(inputDelta.x, 0.0f, inputDelta.y);
        ////Quaternion rotation = Quaternion.Euler(angle);

        ////Vector3 dir = rotation * Vector3.up;

        //Debug.DrawLine(start, end);

        //// hack
        //if (!Input.GetMouseButton(1))
        //{
        //   // SetPhase(BallPhase.None);
        //}

    }

    private void SetPhase(ShootPhase phase)
    {
        this.phase = phase;

        switch (phase)
        {
            case ShootPhase.None:
                {
                    FireIndicator.SetActive(false);
                    LineRenderer.enabled = false;
                }
                break;
            case ShootPhase.SelectPosition:
                {
                    FireIndicator.SetActive(true);
                    LineRenderer.enabled = false;
                }
                break;
            case ShootPhase.SelectPower:
                {
                    FireIndicator.SetActive(false);
                    LineRenderer.enabled = true;
                }
                break;
            case ShootPhase.Rolling:
                {
                    FireIndicator.SetActive(false);
                    LineRenderer.enabled = false;
                }
                break;
        }
    }
}
