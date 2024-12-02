using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 40_000f;
    public float smoothTime = 0.5f;
    public float horizontalBorder = 9.5f;
    public float verticalBorder = 4.5f;

    public float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    private Vector3 dragOrigin;
    private Vector3 velocity = Vector3.zero;
    private float targetZoom;

    void Start()
    {
        targetZoom = Camera.main.orthographicSize;
        InvokeRepeating(nameof(TryToSetBounds), 0.5f, 0.5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 targetPosition = transform.position + new Vector3(-pos.x * panSpeed * Time.deltaTime, -pos.y * panSpeed * Time.deltaTime, 0);
            targetPosition.x = Mathf.Clamp(targetPosition.x, -horizontalBorder, horizontalBorder);
            targetPosition.y = Mathf.Clamp(targetPosition.y, -verticalBorder, verticalBorder);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            dragOrigin = Input.mousePosition;
        }

        HandleZoom();
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            targetZoom -= scroll * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }

    private void TryToSetBounds()
    {
        if (CanvasManager.Instance != null)
        {
            horizontalBorder = CanvasManager.Instance.CanvasWidth * 0.2f / 2;
            verticalBorder = CanvasManager.Instance.CanvasHeight * 0.2f / 2;
            CancelInvoke(nameof(TryToSetBounds));
        }
    }
}
