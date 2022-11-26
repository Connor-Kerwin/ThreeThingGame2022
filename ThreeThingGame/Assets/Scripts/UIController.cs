using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    protected GameObject mMainUICanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUI()
    {
        mMainUICanvas?.SetActive(true);
    }

    public void HideUI()
    {
        mMainUICanvas?.SetActive(false);
    }
}
