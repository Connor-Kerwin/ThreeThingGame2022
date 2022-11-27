using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointDisplay : MonoBehaviour
{
    private BallHandler ballHandler;

    public PointDisplayItem Prefab;
    public Vector3 SpawnOffset;

    private void Awake()
    {
        Resolver.Register<PointDisplay>(this);
    }

    private void OnDestroy()
    {
        Resolver.Unregister<PointDisplay>();
    }

    private void Start()
    {
        ballHandler = Resolver.Resolve<BallHandler>();
    }

    public void DisplayPoints(int score)
    {
        var ball = ballHandler.ResolveCurrentBall();
        if(ball == null)
        {
            return;
        }

        var item = Instantiate<PointDisplayItem>(Prefab);
        item.SetContent(score.ToString());

        item.transform.position = ball.transform.position + SpawnOffset;
    }
}
