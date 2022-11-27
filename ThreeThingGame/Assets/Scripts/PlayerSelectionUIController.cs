using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class PlayerSelectionUIController : UIController
{
    [SerializeField]
    private TMP_InputField m_Player1NameInput;
    [SerializeField]
    private TMP_InputField m_Player2NameInput;
    [SerializeField]
    private TMP_InputField m_Player3NameInput;
    [SerializeField]
    private TMP_InputField m_Player4NameInput;

    [SerializeField]
    private Image m_Player1Indicator;
    [SerializeField]
    private Image m_Player2Indicator;
    [SerializeField]
    private Image m_Player3Indicator;
    [SerializeField]
    private Image m_Player4Indicator;

    [SerializeField]
    private Button m_PlayButton;

    [SerializeField]
    private Toggle m_randomToggle;

    [SerializeField]
    private Color m_NotInUseColor;
    [SerializeField]
    private Color m_InUseColor;

    private bool m_player1In = false;
    private bool m_player2In = false;
    private bool m_player3In = false;
    private bool m_player4In = false;

    private void Awake()
    {
        Resolver.Register<PlayerSelectionUIController>(this);
        m_PlayButton.interactable = false;
    }

    public void Player1NameUpdated(string _newName)
    {
        PlayerNameUpdated(_newName, 1);
    }

    public void Player2NameUpdated(string _newName)
    {
        PlayerNameUpdated(_newName, 2);
    }

    public void Player3NameUpdated(string _newName)
    {
        PlayerNameUpdated(_newName, 3);
    }

    public void Player4NameUpdated(string _newName)
    {
        PlayerNameUpdated(_newName, 4);
    }

    private void PlayerNameUpdated(string _newName, int _playerNum)
    {
        switch (_playerNum)
        {
            case 1:
                {
                    if (_newName != string.Empty)
                    {
                        m_Player1Indicator.color = m_InUseColor;
                        m_player1In = true;
                    }
                    else
                    {
                        m_Player1Indicator.color = m_NotInUseColor;
                        m_player1In = false;
                    }
                }
                break;
            case 2:
                {
                    if (_newName != string.Empty)
                    {
                        m_Player2Indicator.color = m_InUseColor;
                        m_player2In = true;
                    }
                    else
                    {
                        m_Player2Indicator.color = m_NotInUseColor;
                        m_player2In = false;
                    }
                }
                break;
            case 3:
                {
                    if (_newName != string.Empty)
                    {
                        m_Player3Indicator.color = m_InUseColor;
                        m_player2In = true;
                    }
                    else
                    {
                        m_Player3Indicator.color = m_NotInUseColor;
                        m_player3In = false;
                    }
                }
                break;
            case 4:
                {
                    if (_newName != string.Empty)
                    {
                        m_Player4Indicator.color = m_InUseColor;
                        m_player4In = true;
                    }
                    else
                    {
                        m_Player4Indicator.color = m_NotInUseColor;
                        m_player4In = false;
                    }
                }
                break;
        }

        // Quickly check to see if there is at least 1 player ready to go
        if (m_player1In || m_player2In || m_player3In || m_player4In)
        {
            m_PlayButton.interactable = true;
        }
        else
        {
            m_PlayButton.interactable = false;
        }

        Resolver.Resolve<GameManager>().SetPlayerName(_newName, _playerNum);
    }

    public void Handle_PlayClicked()
    {
        Resolver.Resolve<GameManager>().Handle_PlayClicked(m_randomToggle.isOn);
    }

    private void OnDestroy()
    {
        Resolver.Unregister<PlayerSelectionUIController>();
    }
}
