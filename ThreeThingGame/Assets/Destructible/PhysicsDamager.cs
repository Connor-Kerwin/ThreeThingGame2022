using UnityEngine;
using UnityEngine.Events;

public class PhysicsDamager : MonoBehaviour
{
    [SerializeField] protected Rigidbody physicsBody;

    public float MagnitudeThreshold;
    public int DamageOnHit;

    private void OnCollisionEnter(Collision collision)
    {
        var destructible = collision.gameObject.GetComponent<IPhysicsReceiver>();
        if(destructible == null)
        {
            return;
        }

        var mag = physicsBody.velocity.magnitude;
        if(mag >= MagnitudeThreshold)
        {
            destructible.ReceivePhysicsEvent(collision);
        }
    }
}
