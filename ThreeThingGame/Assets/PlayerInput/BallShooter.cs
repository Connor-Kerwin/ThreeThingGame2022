using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Rigidbody rBody;
    public float Force;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var cam = Camera.main;
            var camRay = cam.ScreenPointToRay(Input.mousePosition);

            var plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(camRay, out float enter))
            {
                var hitPoint = camRay.GetPoint(enter);

                // Match the Y level
                var srcPoint = transform.position;
                srcPoint.y = hitPoint.y;

                var dir = hitPoint - srcPoint;
                Debug.DrawLine(srcPoint, hitPoint);

                rBody.AddForce(dir * Force, ForceMode.Impulse);
            }
        }
    }
}