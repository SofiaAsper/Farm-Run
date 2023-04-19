using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    // update the size of the map to the size of the screen **************************
    public MeshRenderer meshRenderer;
    Camera mainCamera;
    float  orthoSize;

    // movement of the camera *****************************************

    Vector3 touchStart;

    // zoom of the camera *****************************************

    Vector3 normalizedCameraPosition;
    float currentZoomLevel;

    void Start()
    {
        orthoSize = meshRenderer.bounds.size.z * Screen.height / Screen.width * 0.3f;    

        Camera.main.orthographicSize = orthoSize;

        mainCamera = GetComponent<Camera>();
    }

    // void Update() {
    //     if(Input.GetMouseButtonDown(0)) {
    //         touchStart = GetWorldPosition(0);
    //     }
    //     if(Input.GetMouseButton(0)) {
    //         Vector3 direction = touchStart - GetWorldPosition(0);
    //         mainCamera.transform.position += direction;
    //     }
    // }
    
    // private Vector3 GetWorldPosition(float y)
    // {
    //     Ray mousePos = mainCamera.ScreenPointToRay(Input.mousePosition);
    //     Plane ground = new Plane(Vector3.up, new Vector3(0,y,0));
    //     float distance;
    //     ground.Raycast(mousePos, out distance);
    //     return mousePos.GetPoint(distance);
    // }




}
