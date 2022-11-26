using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyAfterTime : MonoBehaviour
{
    public float DestructionTime = 1.0f;

    public UnityEvent OnBeforeDestroy;

    public void BeginDestructionCountdown()
    {
        Destroy(gameObject, DestructionTime);
    }

    private void OnDestroy()
    {
        OnBeforeDestroy?.Invoke();
    }
}