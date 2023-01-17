using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPinchAndZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public float maxHorizontalPos = 3f;
    public float minHorizontalPos = -3f;
    public float maxVerticalPos = 12f;
    public float minVerticalPos = 8f;
    public float cameraSpeed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = new Vector3(direction.x, direction.y, 0f);
            // Camera.main.transform.position += direction;
            Vector3 newCameraPosition = Camera.main.transform.position + direction;
            // Camera.main.transform.position = newCameraPosition;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newCameraPosition, cameraSpeed);
            Camera.main.transform.position = new Vector3(
                Mathf.Clamp(
                    Camera.main.transform.position.x, minHorizontalPos, maxHorizontalPos
                ),
                Mathf.Clamp(
                    Camera.main.transform.position.y, minVerticalPos, maxVerticalPos
                ),
                Camera.main.transform.position.z
            );
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}