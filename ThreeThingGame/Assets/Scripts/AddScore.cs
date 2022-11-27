using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    private PointDisplay pointDisplay;

    public int ScoreToAdd;

    private void Start()
    {
        pointDisplay = Resolver.Resolve<PointDisplay>();
    }

    public void AddSomeScore()
    {
        pointDisplay.DisplayPoints(ScoreToAdd);

        // TODO: Score should be added here...
    }
}
