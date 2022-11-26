using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsUIController : UIController
{
    private void Awake()
    {
        Resolver.Register<OptionsUIController>(this);
    }

    private void OnDestroy()
    {
        Resolver.Unregister<OptionsUIController>();
    }
}
