using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public void Spawn(GameObject target)
    {
        var obj = Instantiate(target);
        obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}