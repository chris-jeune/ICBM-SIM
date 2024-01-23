using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce code est une classe qui gere la destruction du missile lorsqu'il entre en collision
/// avec un objet portant le tag "Collidable" ou "Hors Limites". Si le missile a ete lance
/// et qu'il touche le sol, l'etat de lancement du missile est modifie, le jeu est mis en
/// pause et une explosion est cree a l'endroit ou le missile s'est ecrase. Si le missile
/// va hors limite, le jeu est egalement mis en pause et on affiche l'ecran "Attention".
/// </summary>
public class DestroyMissile : MonoBehaviour
{
    public GameObject image; // image du missile
    [SerializeField] private GameObject explosionPrefab; // prefab de l'explosion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collidable")) // si le missile touche le sol
        {
            if (gameObject.GetComponent<Mouvements>().launch)// si le missile a ete lance
            {
                // l'etat de lancement du missile est change
                gameObject.GetComponent<Mouvements>().launch = false;
                GameplayManager.Instance.OnLaunchFinished();

                // le missile est desactive
                image.SetActive(false);
                gameObject.SetActive(false);

                // une explosion est creee a l'endroit ou le missile s'est ecrase
                GameObject explosion = Instantiate(explosionPrefab);
                explosion.transform.position = transform.position;
            }
        }

        if (collision.CompareTag("Hors Limites"))// si le missile va hors limite
        {
            // le jeu est mis en pause et on affiche l'ecran "Attention"
            Time.timeScale = 0;
            GameplayManager.Instance.OnOutOfBounds();
        }
    }


}
