using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchAndZoom : MonoBehaviour
{
# if UNITY_ANDROID
    public Camera mainCamera;
    public Rigidbody cameraBody;
    public float horizontalMoveSpeed;
    public float maxX;
    public float minX;

    Vector3 originPos;
    float originSize;

    // ZOOM
    public float zoomModifierSpeed;
    float touchesPrevDifference, touchesCurDifference, zoomModifier;
    Vector2 firstTouchPrevPos, secondTouchPrevPos;
    public float maxOrthoSize, minOrthoSize;

    // GOBACK
    public float touchDelayGoBack;
    public float goBackSpeed;
    public float unZoomSpeed;
    [SerializeField]
    bool goback = false;
    float dispacementEpsilon = 0.1f;
    float timeTouch;

    void Start()
    {
        cameraBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        originPos = transform.position;
        originSize = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) { timeTouch = Time.time; }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended && Time.time - timeTouch < touchDelayGoBack) { goback = true; }
            else
            {
                goback = false;
                HorizontalForce();
            }
        }
        else if (Input.touchCount == 2)
        {
            goback = false;
            Zoom();
        }
        ClampTransform();
        if (goback) { GoBack(); }
    }


    void HorizontalForce()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            Vector3 force = new Vector3(-touchDeltaPosition.x * horizontalMoveSpeed, 0f, 0f);
            cameraBody.AddForce(force, ForceMode.Impulse);
        }
    }

    void ClampTransform()
    {
        if (transform.position.x >= maxX)
        {
            cameraBody.velocity = new Vector3(
                Mathf.Min(cameraBody.velocity.x, 0f),
                cameraBody.velocity.y,
                cameraBody.velocity.z
            );
            transform.position = new Vector3(
                Mathf.Min(transform.position.x, maxX),
                transform.position.y,
                transform.position.z
            );
        }
        else if (transform.position.x <= minX)
        {
            cameraBody.velocity = new Vector3(
                Mathf.Max(cameraBody.velocity.x, 0f),
                cameraBody.velocity.y,
                cameraBody.velocity.z
            );
            transform.position = new Vector3(
                Mathf.Max(transform.position.x, minX),
                transform.position.y,
                transform.position.z
            );
        }
    }

    void Zoom()
    {
        Touch firstTouch = Input.GetTouch(0);
        Touch secondTouch = Input.GetTouch(1);

        firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
        secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

        touchesPrevDifference = (secondTouchPrevPos - firstTouchPrevPos).magnitude;
        touchesCurDifference = (secondTouch.position - firstTouch.position).magnitude;

        zoomModifier = (secondTouch.deltaPosition - firstTouch.deltaPosition).magnitude * zoomModifierSpeed;

        if (touchesPrevDifference < touchesCurDifference) { mainCamera.orthographicSize -= zoomModifier; }
        if (touchesPrevDifference > touchesCurDifference) { mainCamera.orthographicSize += zoomModifier; }

        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minOrthoSize, maxOrthoSize);
    }

    void GoBack()
    {
        Vector3 displacement = goBackSpeed * Time.deltaTime * (originPos - transform.position);
        float distance = (originPos - transform.position).magnitude;
        if (distance > dispacementEpsilon) { transform.position += displacement; }

        float zoomDisplacement = unZoomSpeed * Time.deltaTime * (originSize - mainCamera.orthographicSize);
        float zoomDistance = Mathf.Abs(originSize - mainCamera.orthographicSize);
        if (zoomDistance > dispacementEpsilon) { mainCamera.orthographicSize += zoomDisplacement; }

        if (distance <= dispacementEpsilon && zoomDistance <= dispacementEpsilon)
        {
            goback = false;
        }


    }
# endif
}
