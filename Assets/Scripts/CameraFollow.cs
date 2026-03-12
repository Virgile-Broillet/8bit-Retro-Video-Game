using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // Joueur à suivre
    public float smoothSpeed = 0.125f; // Fluidité
    public Vector3 offset;             // Décalage caméra / joueur

    // Limites de la caméra
    public float minX, maxX, minY, maxY;

    private void LateUpdate()
    {
        if (target != null)
        {
            // Position désirée
            Vector3 desiredPosition = target.position + offset;

            // Lissage
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Clamping pour rester dans les limites
            float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

            // Appliquer la position finale
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}