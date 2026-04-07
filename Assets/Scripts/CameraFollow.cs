using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Limites caméra
    public float minX, maxX, minY, maxY;

    void Start()
    {
        // Trouver le bon joueur selon la sélection
        if (CharacterSelection.selectedCharacter == 0)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null) target = player.transform;
        }
        else
        {
            GameObject player = GameObject.Find("PlayerW");
            if (player != null) target = player.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed
        );

        float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}