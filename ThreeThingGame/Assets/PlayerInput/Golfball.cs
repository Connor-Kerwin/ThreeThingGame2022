using UnityEngine;

public class Golfball : MonoBehaviour
{
    private BallHandler ballHandler;

    public float BoxThreshold;

    [SerializeField] protected Rigidbody physicsBody;
    [SerializeField] protected GameObject boxCollider;
    [SerializeField] protected GameObject sphereCollider;

    public bool IsConsideredStationary()
    {
        return false;
    }

    private void Start()
    {
        ballHandler = Resolver.Resolve<BallHandler>();
        ballHandler.AddBall(this);

        // HACK: DONT DO THIS
        ballHandler.SetCurrentBall(this);
    }

    private void OnDestroy()
    {
        if(ballHandler != null)
        {
            ballHandler.RemoveBall(this);
        }
    }

    private void Update()
    {
        var velocity = physicsBody.velocity;
        if(velocity.magnitude < BoxThreshold)
        {
            boxCollider.gameObject.SetActive(true);
            sphereCollider.gameObject.SetActive(false);
        }
        else
        {
            boxCollider.gameObject.SetActive(false);
            sphereCollider.gameObject.SetActive(true);
        }
    }

    public void Fire(Vector3 force)
    {
        physicsBody.AddForce(force, ForceMode.Impulse);
    }
}
