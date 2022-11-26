using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class ApplicationFlowStateMachine : MonoBehaviour
{
    public Action<string> OnPlayerTurnChange;

    public GameStates m_CurrentState;

    private int m_currentLevel = 0;

    // Cached player names
    private string m_player1Name = string.Empty;
    private string m_player2Name = string.Empty;
    private string m_player3Name = string.Empty;
    private string m_player4Name = string.Empty;

    private List<int> m_playOrder;
    private int m_currentPlayer;
    private bool m_randomPlayOrder = false;

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

    public int GetLevelNum()
    {
        return m_currentLevel;
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

    public void SwitchControllToNextPlayer()
    {
        m_currentPlayer++;
        if (m_currentPlayer >= m_playOrder.Count)
        {
            m_currentPlayer = 0;
        }
  
        switch (m_playOrder[m_currentPlayer])
        {
            case 1:
                {
                    OnPlayerTurnChange.Invoke(m_player1Name);
                }
                break;
            case 2:
                {
                    OnPlayerTurnChange.Invoke(m_player2Name);
                }
                break;
            case 3:
                {
                    OnPlayerTurnChange.Invoke(m_player3Name);
                }
                break;
            case 4:
                {
                    OnPlayerTurnChange.Invoke(m_player4Name);
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

    public void Handle_PlayClicked(bool _randomPlayOrder)
    {
        m_randomPlayOrder = _randomPlayOrder;

        Resolver.Resolve<PlayerSelectionUIController>().HideUI();
        m_CurrentState = GameStates.LoadLevel;
        m_currentLevel = 1;

        SceneManager.LoadScene(m_currentLevel);

        m_CurrentState = GameStates.BeginLevel;
    }

    public void Handle_StartLevelClicked()
    {
        // Figure out an order for the players to play in
        m_playOrder = new List<int>();
        if (m_player1Name != String.Empty)
        {
            m_playOrder.Add(1);
        }
        if (m_player2Name != String.Empty)
        {
            m_playOrder.Add(2);
        }
        if (m_player3Name != String.Empty)
        {
            m_playOrder.Add(3);
        }
        if (m_player4Name != String.Empty)
        {
            m_playOrder.Add(4);
        }
        m_currentPlayer = 0;

        // Randomise
        if (m_randomPlayOrder)
        {
            Random rand = new Random();
            m_playOrder = m_playOrder.OrderBy(_ => rand.Next()).ToList();
        }

        m_CurrentState = GameStates.GameLoop;

        switch (m_playOrder[m_currentPlayer])
        {
            case 1:
                {
                    OnPlayerTurnChange.Invoke(m_player1Name);
                }
                break;
            case 2:
                {
                    OnPlayerTurnChange.Invoke(m_player2Name);
                }
                break;
            case 3:
                {
                    OnPlayerTurnChange.Invoke(m_player3Name);
                }
                break;
            case 4:
                {
                    OnPlayerTurnChange.Invoke(m_player4Name);
                }
                break;
        }
    }

    public void Handle_NextLevel()
    {
        m_CurrentState = GameStates.LoadLevel;
        m_currentLevel++;

        SceneManager.LoadScene(m_currentLevel);

        m_CurrentState = GameStates.BeginLevel;
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
        GameLoop,
        EndOfLevel,
        GameOver,
    }
}