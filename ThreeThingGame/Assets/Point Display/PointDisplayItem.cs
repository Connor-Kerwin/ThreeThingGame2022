using UnityEngine;
using UnityEngine.Events;

public class PointDisplayItem : MonoBehaviour
{
    private Camera cam;

    public float Lifetime = 1.0f;
    public float MoveSpeed = 0.1f;

    public UnityEvent<string> SyncContent;

    private void Start()
    {
        cam = Camera.main;

        // hack
        Destroy(gameObject, Lifetime);
    }

    private void Update()
    {
        var dir = (transform.position - cam.transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        transform.position += Vector3.up * MoveSpeed * Time.deltaTime;
    }

    public void SetContent(string content)
    {
        SyncContent.Invoke(content);
    }
}