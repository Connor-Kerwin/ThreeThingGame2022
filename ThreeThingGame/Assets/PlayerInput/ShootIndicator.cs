using UnityEngine;

public class ShootIndicator : MonoBehaviour
{
    private Vector3 origin;
    public Vector3 direction;
    private float power;
    private bool shown;

    public LineRenderer Indicator;
    public float PowerScaleModifier;

    private void Awake()
    {
        Resolver.Register<ShootIndicator>(this);
    }

    private void OnDestroy()
    {
        Resolver.Unregister<ShootIndicator>();
    }

    public void SetOrigin(Vector3 position)
    {
        origin = position;
        SyncLine();
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        SyncLine();
    }

    public void SetPower(float power)
    {
        this.power = power;
        SyncLine();
    }

    public void Toggle(bool state)
    {
        shown = state;
        SyncLine();
    }

    private void SyncLine()
    {
        if (!shown)
        {
            Indicator.gameObject.SetActive(false);
        }
        else
        {
            Indicator.gameObject.SetActive(true);
            Indicator.SetPosition(0, origin);
            Indicator.SetPosition(1, origin + direction * power * PowerScaleModifier);
        }
    }
}
