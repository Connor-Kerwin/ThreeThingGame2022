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
        // HACK: This stops issues with OnDestroy spawning objects when exiting play mode
        if (!gameObject.scene.isLoaded)
        {
            return;
        }

        OnBeforeDestroy?.Invoke();
    }
}