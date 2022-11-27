using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTextSetter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_levelTitle;

    // Start is called before the first frame update
    void Start()
    {
        int level = Resolver.Resolve<GameManager>().GetLevelNum();
        m_levelTitle.text = "Level: " + level.ToString();
    }
}
