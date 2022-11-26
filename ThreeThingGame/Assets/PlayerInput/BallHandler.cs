using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    private List<Golfball> balls;
    private Golfball currentBall;

    private void Awake()
    {
        balls = new List<Golfball>();

        Resolver.Register<BallHandler>(this);
    }

    private void OnDestroy()
    {
        Resolver.Unregister<BallHandler>();
    }

    public void AddBall(Golfball ball)
    {
        balls.Add(ball);
    }

    public void RemoveBall(Golfball ball)
    {
        balls.Remove(ball);

        if(currentBall == ball)
        {
            currentBall = null;
        }
    }

    public Golfball ResolveCurrentBall()
    {
        return currentBall;
    }

    public void SetCurrentBall(Golfball ball)
    {
        currentBall = ball;
    }
}
