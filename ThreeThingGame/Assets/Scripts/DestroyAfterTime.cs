using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float DestructionTime = 1.0f;

    public void BeginDestructionCountdown()
    {
        Destroy(gameObject, DestructionTime);
    }
}
