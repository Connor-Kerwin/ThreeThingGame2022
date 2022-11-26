using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    public TrackingMode mode;

    public float LerpRate = 0.2f;

    public float OffsetHeight;

    public OrbitParameters Orbit;


    public float OffsetBehind;

    public void SetTrackingTarget(Transform target)
    {
        this.target = target;
    }

    public void SetTrackingMode(TrackingMode mode)
    {
        this.mode = mode;
    }

    public void SetStaticTrackingMode()
    {
        SetTrackingMode(TrackingMode.Static);
    }

    private void Update()
    {
        //if(target == null)
        //{
        //    return;
        //}

        switch (mode)
        {
            case TrackingMode.Orbit:
                {


                    float scroll = Input.mouseScrollDelta.y;
                    Orbit.Zoom += scroll;

                    if (Input.GetMouseButton(1))
                    {
                        float x = Input.GetAxis("Mouse X") * Orbit.RotationSpeed * Time.deltaTime;
                        float y = Input.GetAxis("Mouse Y") * Orbit.RotationSpeed * Time.deltaTime;

                        Orbit.Yaw += x;
                        Orbit.Pitch -= y;
                    }

                    Vector3 orbitAngle = new Vector3(Orbit.Pitch, Orbit.Yaw, 0.0f);
                    Quaternion orbitRotation = Quaternion.Euler(orbitAngle);

                    Vector3 dir = orbitRotation * Vector3.forward;


                    //Vector3 targetPos = target.position + (Vector3.up * OffsetHeight) - (aimDirection * OffsetBehind);
                    // Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position, Vector3.up);

                    //Vector2 lookVector = new Vector2(Orbit.Pitch, Orbit.Yaw);
                    //Quaternion lookRotation = Quaternion.Euler(lookVector);

                    //Vector3 aimDirection = lookRotation * Vector3.forward;

                    Quaternion targetRot = orbitRotation;
                    Vector3 targetPos = Orbit.Target.position + (dir * Orbit.Zoom);

                    transform.position = Vector3.Lerp(transform.position, targetPos, LerpRate);

                    // transform.position = Vector3.Lerp(transform.position, targetPos, LerpRate);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, LerpRate);
                }
                break;
            //case TrackingMode.VelocityBased:
            //    {

            //    }
                //break;
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
        Orbit,
        Static
    }

    [System.Serializable]
    public class OrbitParameters
    {
        public float Yaw;
        public float Pitch;
        public float Zoom;
        public Transform Target;

        public float RotationSpeed;
        public float MinZoom;
        public float MaxZoom;
    }
}
