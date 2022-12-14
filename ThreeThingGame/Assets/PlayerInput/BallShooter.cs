//using Mono.Cecil.Cil;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.AnimatedValues;
//using UnityEngine;

//public enum ShootPhase
//{
//    None,
//    SelectPosition,
//    SelectPower,
//    Rolling
//}

//public class BallShooter : MonoBehaviour
//{
//    public Rigidbody rBody;
//    public float Force;
//    public float BoxEnableForce;

//    public GameObject SphereCollider;
//    public GameObject BoxCollider;

//    public LineRenderer AimLine;
//    public CameraController CameraController;

//    private ShootPhase phase;

//    private bool TryResolveMousePoint(out Vector3 point)
//    {
//        var cam = Camera.main;
//        var camRay = cam.ScreenPointToRay(Input.mousePosition);

//        var plane = new Plane(Vector3.up, Vector3.zero);
//        if (plane.Raycast(camRay, out float enter))
//        {
//            var hitPoint = camRay.GetPoint(enter);

//            // Match the Y level
//            var srcPoint = transform.position;
//            srcPoint.y = hitPoint.y;

//            point = hitPoint;
//            return true;
//        }

//        point = default;
//        return false;
//    }

//    private Vector3 ResolveAimVector(Vector3 source, Vector3 point)
//    {
//        var dir = point - source;
//        var aimDir = -dir;

//        var mod = 1.0f;
//        return aimDir * mod;
//    }

//    private Vector3 ResolveAimLineEndpoint(Vector3 aimVector)
//    {
//        return transform.position + aimVector;
//    }

//    private void Update()
//    {
//        if (phase == ShootPhase.None)
//        {
//            AimLine.gameObject.SetActive(false);

//            if (Input.GetMouseButtonDown(0))
//            {
//                phase = ShootPhase.SelectPosition;
//            }
//        }

//        if (phase == ShootPhase.SelectPosition)
//        {
//            AimLine.gameObject.SetActive(true);

//            if (!TryResolveMousePoint(out Vector3 hitPoint))
//            {
//                Debug.Log("didnt resolve");
//                return;
//            }

//            var aimVector = ResolveAimVector(transform.position, hitPoint);
//            var end = ResolveAimLineEndpoint(aimVector);

//            AimLine.SetPosition(0, transform.position);
//            AimLine.SetPosition(1, end);

//            //CameraController.SetFixedRotationTrackingMode(aimVector);

//            if (!Input.GetMouseButton(0))
//            {
//                var force = aimVector;
//                rBody.AddForce(force, ForceMode.Impulse);

//                BoxCollider.SetActive(false);
//                SphereCollider.SetActive(true);
//                phase = ShootPhase.Rolling;
//            }
//        }

//        else if(phase == ShootPhase.Rolling)
//        {
//            var velocity = rBody.velocity;
//            Debug.Log(rBody.velocity);

//            if(velocity.magnitude < BoxEnableForce)
//            {
//                BoxCollider.SetActive(true);
//                SphereCollider.SetActive(false);
//            }

//            if(velocity.magnitude <= 0.001f)
//            {
//                phase = ShootPhase.None;
//            }
//        }

//        //var cam = Camera.main;
//        //var camRay = cam.ScreenPointToRay(Input.mousePosition);

//        //var plane = new Plane(Vector3.up, Vector3.zero);
//        //if (plane.Raycast(camRay, out float enter))
//        //{
//        //    var hitPoint = camRay.GetPoint(enter);

//        //    // Match the Y level
//        //    var srcPoint = transform.position;
//        //    srcPoint.y = hitPoint.y;

//        //    var dir = (hitPoint - srcPoint).normalized;
//        //    Debug.DrawLine(srcPoint, hitPoint);

//        //    switch (phase)
//        //    {
//        //        case DragPhase.None:
//        //            {

//        //            }
//        //            break;
//        //        case DragPhase.Dragging:
//        //            {

//        //            }
//        //            break;
//        //    }

//        //    if (Input.GetMouseButtonDown(0))
//        //    {
//        //        rBody.AddForce(dir * Force, ForceMode.Impulse);
//        //    }
//        //}
//    }
//}