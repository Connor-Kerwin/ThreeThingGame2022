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

    // Helper function that notifys the game manager and adds a turn
    public void Handle_ReindeerDestruction()
    {
        // Hack, define a value in editor

        Resolver.Resolve<GameManager>().AddTurn();
        Resolver.Resolve<GameManager>().AddTurn();
        Resolver.Resolve<GameManager>().AddTurn();
    }
}