using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationFlowStateMachine : MonoBehaviour
{
    public GameStates m_CurrentState;

    // Cached player names
    private string m_player1Name = string.Empty;
    private string m_player2Name = string.Empty;
    private string m_player3Name = string.Empty;
    private string m_player4Name = string.Empty;

    public void Awake()
    {
        if (Resolver.Resolve<ApplicationFlowStateMachine>())
        {
            Destroy(this.gameObject);
            return;
        }

        Resolver.Register<ApplicationFlowStateMachine>(this);

        m_CurrentState = GameStates.MainMenu;
    }

    public void SetPlayerName(string _playerName, int _playerNum)
    {
        switch (_playerNum)
        {
            case 1:
                {
                    m_player1Name = _playerName;
                }
                break;
            case 2:
                {
                    m_player2Name = _playerName;
                }
                break;
            case 3:
                {
                    m_player3Name = _playerName;
                }
                break;
            case 4:
                {
                    m_player4Name = _playerName;
                }
                break;
        }
    }

    public void Handle_StartGameClicked()
    {
        Resolver.Resolve<MainMenuUIController>().HideUI();
        Resolver.Resolve<PlayerSelectionUIController>().ShowUI();
        m_CurrentState = GameStates.PlayerSelection;
    }

    public void Handle_OptionsClicked()
    {
        Resolver.Resolve<MainMenuUIController>().HideUI();
        Resolver.Resolve<OptionsUIController>().ShowUI();
        m_CurrentState = GameStates.Options;
    }

    public void Handle_ExitClicked()
    {
        Application.Quit();
    }

    public void Handle_BackToMainMenuClicked()
    {
        Resolver.Resolve<OptionsUIController>().HideUI();
        Resolver.Resolve<PlayerSelectionUIController>().HideUI();
        Resolver.Resolve<MainMenuUIController>().ShowUI();
        m_CurrentState = GameStates.MainMenu;
    }

    public void Handle_PlayClicked()
    {
        Resolver.Resolve<PlayerSelectionUIController>().HideUI();
        m_CurrentState = GameStates.LoadLevel;

        SceneManager.LoadScene(1);
    }

    private void OnDestroy()
    {
        Resolver.Unregister<ApplicationFlowStateMachine>();
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
