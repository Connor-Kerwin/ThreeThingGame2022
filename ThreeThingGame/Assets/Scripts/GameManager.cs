using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int m_maxLevels = 0;

    public Action<string> OnPlayerTurnChange;
    public Action<int> OnTurnsRemainingUpdated;

    public GameStates m_CurrentState = GameStates.MainMenu;

    [SerializeField]
    private GameObject m_ball;

    private int m_currentLevel = 1;
    private int m_turnsRemaining = 0;

    // Cached player names
    private string m_player1Name = string.Empty;
    private string m_player2Name = string.Empty;
    private string m_player3Name = string.Empty;
    private string m_player4Name = string.Empty;

    private List<int> m_playOrder;
    private int m_currentPlayer;
    private bool m_randomPlayOrder = false;
    private Hashtable m_playerBalls = new Hashtable();

    public void Awake()
    {
        if (Resolver.Resolve<GameManager>())
        {
            Destroy(this.gameObject);
            return;
        }

        Resolver.Register<GameManager>(this);
    }

    public void Start()
    {
        SceneManager.LoadScene(1);
    }

    public int GetLevelNum()
    {
        return m_currentLevel;
    }

    public string GetCurrentPlayerName()
    {
        switch (m_currentPlayer)
        {
            case 0:
                return m_player1Name;
            case 1:
                return m_player2Name;
            case 2:
                return m_player3Name;
            case 3:
                return m_player4Name;
            default:
                return null;
        }
    }

    public List<string> GetPlayerNames()
    {
        List<string> names = new List<string>();
        if (m_player1Name != string.Empty)
        {
            names.Add(m_player1Name);
        }
        if (m_player2Name != string.Empty)
        {
            names.Add(m_player2Name);
        }
        if (m_player3Name != string.Empty)
        {
            names.Add(m_player3Name);
        }
        if (m_player4Name != string.Empty)
        {
            names.Add(m_player4Name);
        }
        return names;
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

    public bool IsOnLastLevel()
    {
        if ((m_currentLevel - 1) == m_maxLevels)
        {
            return true;
        }
        return false;
    }

    public void SwitchControllToNextPlayer()
    {
        m_currentPlayer++;
        if (m_currentPlayer >= m_playOrder.Count)
        {
            m_currentPlayer = 0;
            m_turnsRemaining--;
            OnTurnsRemainingUpdated.Invoke(m_turnsRemaining);

            if (m_turnsRemaining == 0)
            {
                m_CurrentState = GameStates.EndOfLevel;
            }
        }

        // Begin shooting
        if (m_CurrentState != GameStates.GameLoop)
        {
            return;
        }

        // Check and set balls
        CheckBalls(m_playOrder[m_currentPlayer]);
        SetBall(m_playOrder[m_currentPlayer]);

        var shoot = Resolver.Resolve<ShootInput>();
        shoot.BeginShooting();

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

    public void Handle_HowToPlayClicked()
    {
        Resolver.Resolve<MainMenuUIController>().HideUI();
        Resolver.Resolve<HowToPlayUIController>().ShowUI();
        m_CurrentState = GameStates.Options;
    }

    public void Handle_ExitClicked()
    {
        Application.Quit();
    }

    public void Handle_BackToMainMenuClicked()
    {
        Resolver.Resolve<HowToPlayUIController>().HideUI();
        Resolver.Resolve<PlayerSelectionUIController>().HideUI();
        Resolver.Resolve<MainMenuUIController>().ShowUI();
        m_CurrentState = GameStates.MainMenu;
    }

    public void Handle_PlayClicked(bool _randomPlayOrder)
    {
        Resolver.Resolve<ScoreManager>().ResetScores();
        if (m_player1Name != string.Empty)
        {
            Resolver.Resolve<ScoreManager>().AddPlayerToScoreList(m_player1Name);
        }
        if (m_player2Name != string.Empty)
        {
            Resolver.Resolve<ScoreManager>().AddPlayerToScoreList(m_player2Name);
        }
        if (m_player3Name != string.Empty)
        {
            Resolver.Resolve<ScoreManager>().AddPlayerToScoreList(m_player3Name);
        }
        if (m_player4Name != string.Empty)
        {
            Resolver.Resolve<ScoreManager>().AddPlayerToScoreList(m_player4Name);
        }

        m_randomPlayOrder = _randomPlayOrder;

        Resolver.Resolve<PlayerSelectionUIController>().HideUI();
        m_CurrentState = GameStates.LoadLevel;
        m_currentLevel = 2;

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

        // Randomize
        if (m_randomPlayOrder)
        {
            Random rand = new Random();
            m_playOrder = m_playOrder.OrderBy(_ => rand.Next()).ToList();
        }

        m_CurrentState = GameStates.GameLoop;

        // Check and set balls
        m_playerBalls = new Hashtable();
        CheckBalls(m_playOrder[m_currentPlayer]);
        SetBall(m_playOrder[m_currentPlayer]);

        // Begin shooting
        var shoot = Resolver.Resolve<ShootInput>();
        shoot.BeginShooting();


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

        m_turnsRemaining = Resolver.Resolve<LevelManager>().GetNumberOfTurns();
        OnTurnsRemainingUpdated?.Invoke(m_turnsRemaining);
    }

    public void Handle_NextLevel()
    {
        m_CurrentState = GameStates.LoadLevel;

        // Check to make sure we are not at the end of the levels
        if ((m_currentLevel - 1) == m_maxLevels)
        {
            m_currentLevel = 1;
            SceneManager.LoadScene(m_currentLevel);
            m_CurrentState = GameStates.MainMenu;

            m_player1Name = string.Empty;
            m_player2Name = string.Empty;
            m_player3Name = string.Empty;
            m_player4Name = string.Empty;
            return;
        }

        m_currentLevel++;

        SceneManager.LoadScene(m_currentLevel);

        m_CurrentState = GameStates.BeginLevel;
    }

    public void AddTurn()
    {
        m_turnsRemaining++;
        OnTurnsRemainingUpdated.Invoke(m_turnsRemaining);
    }

    // Checks to ensure a player has balls
    private void CheckBalls(int _playerNum)
    {
        if (!m_playerBalls.ContainsKey(_playerNum))
        {
            BallSpawnManager ballSpawnManager = Resolver.Resolve<BallSpawnManager>();
            GameObject newGolfBall = Instantiate(m_ball, ballSpawnManager.GetSpawnPoint(_playerNum).transform.position, ballSpawnManager.GetSpawnPoint(_playerNum).transform.rotation);
            m_playerBalls.Add(_playerNum, newGolfBall);
        }
    }

    private void SetBall(int _playerNum)
    {
        GameObject entry = (GameObject)m_playerBalls[_playerNum];
        Resolver.Resolve<BallHandler>().SetCurrentBall(entry.GetComponent<Golfball>());
        Resolver.Resolve<CameraController>().Orbit.Target = entry.transform;
    }

    private void OnDestroy()
    {
        Resolver.Unregister<GameManager>();
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