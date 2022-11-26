using UnityEngine;
using UnityEngine.Events;

public class JointNotifier : MonoBehaviour
{
    public UnityEvent JointBreak;

    private void OnJointBreak(float breakForce)
    {
        Debug.Log($"Joint breaking with force {breakForce}");
        JointBreak?.Invoke();
    }
}