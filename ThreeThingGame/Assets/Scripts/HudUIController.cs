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
    }

    private void AttachLiseners()
    {
        Resolver.Resolve<ApplicationFlowStateMachine>().OnPlayerTurnChange += Handle_PlayerTurnChange;
    }

    private void DettachLiseners()
    {
        // Its posiable the ApplicationFlowStateMachine has already been cleaned up
        // if the application is closing so we put a try catch here
        try
        {
            Resolver.Resolve<ApplicationFlowStateMachine>().OnPlayerTurnChange -= Handle_PlayerTurnChange;
        }
        catch { } 
    }

    private void OnDestroy()
    {
        DettachLiseners();
    }
}