using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    private PointDisplay pointDisplay;
    private ApplicationFlowStateMachine flowManager;
    private ScoreManager scoreManager;

    public int ScoreToAdd;

    private void Start()
    {
        pointDisplay = Resolver.Resolve<PointDisplay>();
        flowManager = Resolver.Resolve<ApplicationFlowStateMachine>();
        scoreManager = Resolver.Resolve<ScoreManager>();
    }

    public void AddSomeScore()
    {
        pointDisplay.DisplayPoints(ScoreToAdd);

        var currentPlayer = flowManager.GetCurrentPlayerName();
        scoreManager.IncreementScore(ScoreToAdd, currentPlayer);

        // TODO: Score should be added here...
    }
}
