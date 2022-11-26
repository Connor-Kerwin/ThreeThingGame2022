using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIController : UIController
{
    private void Awake()
    {
        Resolver.Register<MainMenuUIController>(this);
    }

    private void OnDestroy()
    {
        Resolver.Unregister<MainMenuUIController>();
    }
}
