using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragPhase
{
    None,
    Dragging
}

public class BallShooter : MonoBehaviour
{
    public Rigidbody rBody;
    public float Force;

    private DragPhase phase;

    private void Update()
    {
        var cam = Camera.main;

        if (phase == DragPhase.None)
        {
            if (Input.GetMouseButtonDown(0))
            {
                phase = DragPhase.Dragging;
            }
        }

        if(phase == DragPhase.Dragging)
        {

        }

        var camRay = cam.ScreenPointToRay(Input.mousePosition);

        var plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(camRay, out float enter))
        {
            var hitPoint = camRay.GetPoint(enter);

            // Match the Y level
            var srcPoint = transform.position;
            srcPoint.y = hitPoint.y;

            var dir = (hitPoint - srcPoint).normalized;
            Debug.DrawLine(srcPoint, hitPoint);

            switch (phase)
            {
                case DragPhase.None:
                    {

                    }
                    break;
                case DragPhase.Dragging:
                    {

                    }
                    break;
            }

            if (Input.GetMouseButtonDown(0))
            {
                rBody.AddForce(dir * Force, ForceMode.Impulse);
            }
        }
    }
}