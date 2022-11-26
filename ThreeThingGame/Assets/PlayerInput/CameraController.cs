using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Vector3 aimDirection;

    public TrackingMode mode;

    public float OffsetHeight;
    

    public float OffsetBehind;

    public void SetTrackingTarget(Transform target)
    {
        this.target = target;
    }

    public void SetTrackingMode(TrackingMode mode)
    {
        this.mode = mode;
    }

    public void SetFixedRotationTrackingMode(Vector3 aimDirection)
    {
        SetTrackingMode(TrackingMode.FixedRotation);
        this.aimDirection = aimDirection;
    }

    public void SetStaticTrackingMode()
    {
        SetTrackingMode(TrackingMode.Static);
    }

    private void Update()
    {
        if(target == null)
        {
            return;
        }

        switch (mode)
        {
            case TrackingMode.FixedRotation:
                {
                    transform.position = target.position + (Vector3.up * OffsetHeight) - (target.forward * OffsetBehind);
                    transform.rotation = Quaternion.LookRotation(aimDirection, Vector3.up);
                }
                break;
            case TrackingMode.Static:
                {

                }
                break;
            default:
                break;
        }
    }

    public enum TrackingMode
    {
        FixedRotation,
        Static
    }
}
