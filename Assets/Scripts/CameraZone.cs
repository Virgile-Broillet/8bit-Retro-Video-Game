using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [Header("Camera")]
    public Camera mainCamera;

    [Header("Zoom")]
    public float zoomInSize = 2.5f;
    public float zoomOutSize = 4f;

    [Header("Smooth")]
    public float smoothSpeed = 2f;

    private float targetSize;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        targetSize = mainCamera.orthographicSize;
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = Mathf.Lerp(
                mainCamera.orthographicSize,
                targetSize,
                Time.deltaTime * smoothSpeed
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetSize = zoomOutSize;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetSize = zoomInSize;
        }
    }
}