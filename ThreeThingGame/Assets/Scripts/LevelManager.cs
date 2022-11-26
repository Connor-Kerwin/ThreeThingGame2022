using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int m_numberOfTurnsForLevel;

    private void Awake()
    {
        Resolver.Register<LevelManager>(this);
    }

    public int GetNumberOfTurns()
    {
        return m_numberOfTurnsForLevel;
    }

    private void OnDestroy()
    {
        Resolver.Unregister<LevelManager>();
    }
}
