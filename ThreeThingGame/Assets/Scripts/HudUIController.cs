using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudUIController : UIController
{
    [SerializeField]
    private GameObject m_preGameUIRoot;
    [SerializeField]
    private GameObject m_gameUIRoot;
    [SerializeField]
    private GameObject m_postGameUIRoot;

    [SerializeField]
    private TMP_Text m_playersTurnLabel;
    [SerializeField]
    private TMP_Text m_turnsRemainingLabel;

    [SerializeField]
    private List<TMP_Text> m_playerScoreLabels;

    // Start is called before the first frame update
    private void Start()
    {
        AttachLiseners();

        m_gameUIRoot.SetActive(false);
        m_postGameUIRoot.SetActive(false);
    }

    public void Handle_PlayerTurnChange(string _playerTurn)
    {
        // Handle this and setup the UI for the next player
        m_playersTurnLabel.text = _playerTurn + "'s turn";
    }

    public void Handle_DebugSkipClicked()
    {
        // Skip the players turn for testing purposes during debugging
        Resolver.Resolve<ApplicationFlowStateMachine>().SwitchControllToNextPlayer();
    }

    public void Handle_BeginClicked()
    {
        m_preGameUIRoot.SetActive(false);
        m_gameUIRoot.SetActive(true);
        Resolver.Resolve<ApplicationFlowStateMachine>().Handle_StartLevelClicked();
        Handle_TurnsRemaining(Resolver.Resolve<LevelManager>().GetNumberOfTurns());
    }

    public void Handle_NextLevelClicked()
    {
        Resolver.Resolve<ApplicationFlowStateMachine>().Handle_NextLevel();
    }

    public void Handle_TurnsRemaining(int _turnsLeft)
    {
        m_turnsRemainingLabel.text = "Turns Left: " + _turnsLeft;

        if (_turnsLeft <= 0)
        {
            m_gameUIRoot.SetActive(false);
            m_postGameUIRoot.SetActive(true);

            // Set the scores
            List<string> playerNames = Resolver.Resolve<ApplicationFlowStateMachine>().GetPlayerNames();
            ScoreManager scoreManager = Resolver.Resolve<ScoreManager>();

            int i;
            for (i = 0; i < playerNames.Count; i++)
            {
                string playerName = playerNames[i];
                int score = scoreManager.GetScore(playerName);
                m_playerScoreLabels[i].text = playerName + "'s Score: " + score.ToString();
            }
            // Setting I to I is intentional, please ignore error
            for (i = i; i < 4; i++)
            {
                m_playerScoreLabels[i].gameObject.SetActive(false);
            }
        }
    }

    private void AttachLiseners()
    {
        Resolver.Resolve<ApplicationFlowStateMachine>().OnPlayerTurnChange += Handle_PlayerTurnChange;
        Resolver.Resolve<ApplicationFlowStateMachine>().OnTurnsRemainingUpdated += Handle_TurnsRemaining;
    }

    private void DettachLiseners()
    {
        // Its posiable the ApplicationFlowStateMachine has already been cleaned up
        // if the application is closing so we put a try catch here
        try
        {
            Resolver.Resolve<ApplicationFlowStateMachine>().OnPlayerTurnChange -= Handle_PlayerTurnChange;
            Resolver.Resolve<ApplicationFlowStateMachine>().OnTurnsRemainingUpdated -= Handle_TurnsRemaining;
        }
        catch { } 
    }

    private void OnDestroy()
    {
        DettachLiseners();
    }
}
