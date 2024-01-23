using UnityEngine;

/// <summary>
/// Ce script permet de faire suivre une camera a un missile.
/// La distance entre la camera et le missile dans la 3eme dimension 
/// et la vitesse de deplacement de la camera sont reglables. 
/// L'offset initial entre la camera et le missile est calcule au demarrage,
/// puis la camera suit le missile avec une interpolation lineaire.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform missileTransform; // reference vers le transform du missile
    public float cameraDistance = 10f; // distance entre la camera et le missile dans la 3e dimension
    public float cameraSpeed = 5f; // vitesse de deplacement de la camera
    private Vector3 cameraOffset; // distance entre la camera et le missile a un instant donne en 2D

    void Start()
    {
        // Calculer l'offset initial entre la camera et le missile
        cameraOffset = transform.position - missileTransform.position;
    }

    void LateUpdate()
    {
        // Suivre le missile
            Vector3 targetPosition = missileTransform.position + cameraOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }

}
