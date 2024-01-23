
using UnityEngine;
/// <summary>
/// Ce script permet de gerer la creation des plateformes que le missile doit traverser lors de son parcours.
/// Le script est attache aux gameObjects "Platform Releaser(1)" et "Platform Releaser(2)" qui permettent
/// de generer dependamment de l'orientation du missile.
/// </summary>
public class PlatformReleaser : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab; // un prefab pour le sol du missile
    public GameObject missile;// le gameObject "vraimissile"
    public GameObject p1;// le gameObject "Platform Releaser(1)"
    public GameObject p2;// le gameObject "Platform Releaser(2)"
    private Rigidbody2D rb;// Rigidbody2D du missile
    private Rigidbody2D rb2;// Rigidbody2D des platforms releaser
    private float currentXSpawnPosition = 49.83f;// position a laquelle on ajoute une autre partie du sol

    private void Start()
    {
        rb = missile.GetComponent<Rigidbody2D>();
        rb2 = gameObject.GetComponent<Rigidbody2D>();

        // si la vitesse du platform releaser est plus grande que 0 la position a 
        // laquelle on ajoute une autre partie du sol reste la meme sinon elle devient negative
        if (rb2.velocity.x > 0)
        {
            currentXSpawnPosition = 49.83f;

        }
        else
        {
            currentXSpawnPosition = -49.83f;

        }
    }
    private void FixedUpdate()
    {

        // si la vitesse du missile est plus petite ou egale a zero le platform releaser 1 est active
        // et le 2 desactive. L'inverse quand la vitesse est superieur a 0
        if (rb.velocity.x <= 0f)
        {
            p1.SetActive(true);
            p2.SetActive(false);

        }

        else
        {
            p1.SetActive(false);
            p2.SetActive(true);
        }




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si le missile se deplace vers la droite
        if (rb.velocity.x > 0)
        {
            if (collision.CompareTag("Collidable"))// si le platform releaser touche une partie de sol
            {
                // ajout d'une partie du sol a la position de la collision et mise a jour de cette position pour la prochaine collision
                GameObject platform = Instantiate(platformPrefab);
                platform.transform.position = new Vector3(currentXSpawnPosition, -1.58f, 0);
                currentXSpawnPosition += 49.83f;
            }
        }

        // si le missile se deplace vers la gauche meme principe
        else
        {
            if (collision.CompareTag("Collidable"))
            {
                GameObject platform = Instantiate(platformPrefab);
                platform.transform.position = new Vector3(currentXSpawnPosition, -1.58f, 0);
                currentXSpawnPosition -= 49.83f;
            }
        }
    }

}
