using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPhysicsReceiver
{
    void ReceivePhysicsEvent(Collision collision);
}

public class PhysicsReceiver : MonoBehaviour, IPhysicsReceiver
{
    public float Threshold = 1.0f;

    public UnityEvent<Collision> OnPhysicsEventReceived;

    public void ReceivePhysicsEvent(Collision collision)
    {
        Debug.DrawLine(collision.contacts[0].point, collision.contacts[0].point + collision.relativeVelocity, Color.red, 100);

        if(collision.relativeVelocity.magnitude >= Threshold)
        {
            OnPhysicsEventReceived?.Invoke(collision);
        }
    }

    public void SelfDestruct()
    {
        GameObject.Destroy(gameObject);
    }
}

public class SpawnPhysicsImposter : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;

    public void SpawnImposter(Collision collision)
    {
        var instance = Instantiate<GameObject>(prefab);
        var physicsBody = instance.GetComponent<Rigidbody>();
        
        //physicsBody.velocity = collision.norm
    }
}