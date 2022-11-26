using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFlowStateMachine : MonoBehaviour
{
    public GameStates mCurrentState;

    public void Awake()
    {
        mCurrentState = GameStates.MainMenu;
    }

    public void Handle_StartGameClicked()
    {
        Resolver.Resolve<MainMenuUIController>().HideUI();
        //Resolver.Resolve<OptionsUIController>().ShowUI();
    }

    public void Handle_OptionsClicked()
    {
        Resolver.Resolve<MainMenuUIController>().HideUI();
        Resolver.Resolve<OptionsUIController>().ShowUI();
    }

    public void Handle_ExitClicked()
    {
        Application.Quit();
    }

    public void Handle_BackOptionsClicked()
    {
        Resolver.Resolve<OptionsUIController>().HideUI();
        Resolver.Resolve<MainMenuUIController>().ShowUI();
    }

    public enum GameStates
    {
        MainMenu,
        Options,
        PlayerSelection,
        LoadLevel,
        BeginLevel,
        Player1Turn,
        Player2Turn,
        Player3Turn,
        Player4Turn,
        EndOfLevel,
        GameOver,
    }
}
