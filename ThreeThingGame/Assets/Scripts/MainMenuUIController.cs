using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIController : UIController
{
    private void Awake()
    {
        Resolver.Register<MainMenuUIController>(this);
    }

    public void Handle_PlayGameClicked()
    {
        Resolver.Resolve<GameManager>().Handle_StartGameClicked();
    }

    public void Handle_HowToPlayClicked()
    {
        Resolver.Resolve<GameManager>().Handle_HowToPlayClicked();
    }

    public void Handle_ExitClicked()
    {
        Resolver.Resolve<GameManager>().Handle_ExitClicked();
    }

    private void OnDestroy()
    {
        Resolver.Unregister<MainMenuUIController>();
    }
}
