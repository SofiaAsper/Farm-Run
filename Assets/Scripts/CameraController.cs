using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
 public Camera Camera;
    public bool Rotate;
    protected Plane Plane;
    public Text text;

    public float minleft;
    public float maxright;
    public float maxup;
    public float mindown;

    [SerializeField] GameObject storeUI;

    private Vector3 bottomLeft;
    private Vector3 topRight;


    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
        Camera.transform.position = new Vector3(-2f, 21f, -0.85f);

        bottomLeft = Camera.ScreenToWorldPoint(new Vector3(0, 0, Camera.transform.position.y - Camera.transform.position.y*0.07f ));
        topRight = Camera.ScreenToWorldPoint(new Vector3(Camera.pixelWidth, Camera.pixelHeight, Camera.transform.position.y + Camera.transform.position.y * 0.51f));

        minleft = bottomLeft.x;
        maxright = topRight.x - 5f;
        maxup = topRight.z;
        mindown = bottomLeft.z;
    }



    private void Update()
    {

        text.text = Camera.transform.position.y.ToString();
        bottomLeft = Camera.ScreenToWorldPoint(new Vector3(0, 0, Camera.transform.position.y - Camera.transform.position.y*0.07f ));
        topRight = Camera.ScreenToWorldPoint(new Vector3(Camera.pixelWidth, Camera.pixelHeight, Camera.transform.position.y + Camera.transform.position.y * 0.51f));


        //Update Plane
        if (Input.touchCount >= 1 && storeUI.activeSelf == false)
            Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Scroll
        if (Input.touchCount >= 1 && storeUI.activeSelf == false)
        {
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Camera.transform.position.y < 18f )
            {
                Camera.transform.Translate(Delta1, Space.World);
                
                // set the camera position regarding the max and min values

                if (topRight.x >= maxright)
                    Camera.transform.position = new Vector3(Camera.transform.position.x - (topRight.x - maxright), Camera.transform.position.y, Camera.transform.position.z);
                if (bottomLeft.x <= minleft)
                    Camera.transform.position = new Vector3(Camera.transform.position.x + (minleft-bottomLeft.x), Camera.transform.position.y, Camera.transform.position.z);
                if (topRight.z >= maxup)
                    Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z - (topRight.z - maxup));
                if (bottomLeft.z <= mindown)
                    Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z + (mindown-bottomLeft.z));


                // set the x and z values to be between the min and max values with Clamp function
                // Camera.transform.position = new Vector3(Mathf.Clamp(Camera.transform.position.x, minleft, maxright), Camera.transform.position.y, Mathf.Clamp(Camera.transform.position.z, mindown, maxup));

            }

        }

        //Pinch
        if (Input.touchCount == 2 && storeUI.activeSelf == false)
        {
            var pos1  = PlanePosition(Input.GetTouch(0).position);
            var pos2  = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            //edge case
            if (zoom == 0 || zoom > 10)
                return;


            // move the camera if its y value is beyond 7 and under 23
            if (Camera.transform.position.y >= 7 && Camera.transform.position.y <= 22)
                Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);

            else if (Camera.transform.position.y >= 22 && zoom > 1)
                Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);

            else if (Camera.transform.position.y <= 7 && zoom < 1)
                Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);
            

            //Move cam amount the mid ray

            if (Rotate && pos2b != pos2)
                Camera.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
        }

    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

}
