using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_ballSpawns;

    private void Awake()
    {
        Resolver.Register<BallSpawnManager>(this);
    }

    public GameObject GetSpawnPoint(int _spawn)
    {
        return m_ballSpawns[_spawn - 1];
    }

    private void OnDestroy()
    {
        Resolver.Unregister<BallSpawnManager>();
    }
}
