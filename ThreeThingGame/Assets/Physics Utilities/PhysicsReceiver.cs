using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsReceiver : MonoBehaviour
{
    public float Threshold = 1.0f;

    public UnityEvent<Collision> OnPhysicsEventReceived;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= Threshold)
        {
            OnPhysicsEventReceived?.Invoke(collision);
        }
    }

    public void SelfDestruct()
    {
        GameObject.Destroy(gameObject);
    }
}