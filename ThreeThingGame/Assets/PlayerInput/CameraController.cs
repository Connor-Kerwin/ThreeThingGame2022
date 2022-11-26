using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] protected Camera sourceCamera;

    private Transform target;

    public TrackingMode mode;

    public float LerpRate = 0.2f;

    public float OffsetHeight;

    public OrbitParameters Orbit;

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
        switch (mode)
        {
            case TrackingMode.Orbit:
                {
                    // No target, don't do anything
                    if(Orbit.Target == null)
                    {
                        return;
                    }

                    float scroll = Input.mouseScrollDelta.y;
                    Orbit.Zoom = Mathf.Clamp(Orbit.Zoom - scroll, Orbit.MinZoom, Orbit.MaxZoom);

                    // Should orbit occur?
                    if (Input.GetMouseButton(1))
                    {
                        float x = Input.GetAxis("Mouse X") * Orbit.RotationSpeed * Time.deltaTime;
                        float y = Input.GetAxis("Mouse Y") * Orbit.RotationSpeed * Time.deltaTime;

                        Orbit.Yaw += x;
                        Orbit.Pitch = Mathf.Clamp(Orbit.Pitch - y, Orbit.MinPitch, Orbit.MaxPitch);
                    }

                    Vector3 orbitAngle = new Vector3(Orbit.Pitch, Orbit.Yaw, 0.0f);
                    Quaternion orbitRotation = Quaternion.Euler(orbitAngle);

                    Vector3 dir = orbitRotation * Vector3.forward;

                    Quaternion targetRot = orbitRotation;
                    Vector3 targetPos = Orbit.Target.position + (dir * -Orbit.Zoom);

                    sourceCamera.transform.position = Vector3.Lerp(sourceCamera.transform.position, targetPos, LerpRate);
                    sourceCamera.transform.rotation = Quaternion.Lerp(sourceCamera.transform.rotation, targetRot, LerpRate);
                }
                break;
            case TrackingMode.Static:
                {
                    // Do nothing when static cam?
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
        public float Yaw = 0.0f;
        public float Pitch = 0.0f;
        public float Zoom = 0.5f;
        public Transform Target = null;

        public float RotationSpeed = 360.0f;
        public float MinZoom = 0.5f;
        public float MaxZoom = 5.0f;

        public float MinPitch = 10.0f;
        public float MaxPitch = 70.0f;
    }
}
