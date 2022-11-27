using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    Hashtable m_Scores;

    private void Awake()
    {
        if (Resolver.Resolve<ScoreManager>())
        {
            Destroy(this.gameObject);
            return;
        }

        Resolver.Register<ScoreManager>(this);
    }

    public void ResetScores()
    {
        m_Scores = new Hashtable();
    }

    public void AddPlayerToScoreList(string _playerName)
    {
        m_Scores.Add(_playerName, 0);
    }

    [Obsolete("Please use the players name for accessing the score manager")]
    public void AddPlayerToScoreList(int _playerNum)
    {
        m_Scores.Add(_playerNum, 0);
    }

    public void IncreementScore(int _score, string _playerName)
    {
        int currentScore = (int)m_Scores[_playerName];
        currentScore += _score;
        m_Scores[_playerName] = currentScore;
    }

    [Obsolete("Please use the players name for accessing the score manager")]
    public void IncreementScore(int _score, int _playerNum)
    {
        int currentScore = (int)m_Scores[_playerNum];
        currentScore += _score;
        m_Scores[_playerNum] = currentScore;
    }

    public int GetScore(string _playerName)
    {
        return (int)m_Scores[_playerName];
    }

    [Obsolete("Please use the players name for accessing the score manager")]
    public int GetScore(int _playerNum)
    {
        return (int)m_Scores[_playerNum];
    }

    private void OnDestroy()
    {
        Resolver.Unregister<ScoreManager>();
    }
}
