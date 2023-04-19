// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class LimitCameraMovement : MonoBehaviour
// {
//     public CameraController cameraController;

//     public enum limitDirection
//     {
//         up,
//         down,
//         left,
//         right
//     }
//     public limitDirection limit;

//     private void OnBecameVisible() {
//         //if the limit direction is up, then the camera can only move down
//         if (limit.ToString() == "up") {
//             cameraController.limitUp = true;
//         }
//         //if the limit direction is down, then the camera can only move up
//         if (limit.ToString() == "down") {
//             cameraController.limitDown = true;
//         }
//         //if the limit direction is left, then the camera can only move right
//         if (limit.ToString() == "left") {
//             cameraController.limitLeft = true;
//         }
//         //if the limit direction is right, then the camera can only move left
//         if (limit.ToString() == "right") {
//             cameraController.limitRight = true;
//         }


//     }
//     private void OnBecameInvisible() {
//         //if the limit direction is up, then the camera can only move down
//         if (limit.ToString() == "up") {
//             cameraController.limitUp = false;
//         }
//         //if the limit direction is down, then the camera can only move up
//         if (limit.ToString() == "down") {
//             cameraController.limitDown = false;
//         }
//         //if the limit direction is left, then the camera can only move right
//         if (limit.ToString() == "left") {
//             cameraController.limitLeft = false;
//         }
//         //if the limit direction is right, then the camera can only move left
//         if (limit.ToString() == "right") {
//             cameraController.limitRight = false;
//         }
//     }
// }
