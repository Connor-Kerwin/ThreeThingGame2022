using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayUIController : UIController
{
    private void Awake()
    {
        Resolver.Register<HowToPlayUIController>(this);
    }

    public void Handle_BackClicked()
    {
        Resolver.Resolve<GameManager>().Handle_BackToMainMenuClicked();
    }

    private void OnDestroy()
    {
        Resolver.Unregister<HowToPlayUIController>();
    }
}
