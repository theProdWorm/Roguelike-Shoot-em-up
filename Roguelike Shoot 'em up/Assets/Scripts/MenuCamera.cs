using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public float PanSpeed;
    public float zoomSpeed;

    private Vector3 oldPos;
    private Vector3 panOrigin;

    void LateUpdate()
    {
        float panSpeed = Camera.main.orthographicSize * PanSpeed;

        if (Input.GetMouseButtonDown(0))
        {
            oldPos = transform.position;
            panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin; //Get the difference between where the mouse clicked and where it moved
            transform.position = oldPos - pos * panSpeed ;
        }

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (Camera.main.orthographicSize < 0)
            Camera.main.orthographicSize = 1;
    }
}
