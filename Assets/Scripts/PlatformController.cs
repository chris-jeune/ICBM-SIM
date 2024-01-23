
using UnityEngine;

/// <summary>
/// Ce script est utilise pour synchroniser la vitesse du missile avec celle du trigger platform releaser
/// Il recupere le Rigidbody2D du missile et assigne sa vitesse a celle du Rigidbody2D du releaser.
/// </summary>
public class PlatformController : MonoBehaviour
{
    public GameObject missile; // le missile
    private Rigidbody2D rb;// le rigidbody 2d du missile
    [SerializeField] private Rigidbody2D rb2d;// le rigidbody 2d du gameObject "Platform Releaser(1 ou 2)"

    private void Start()
    {
        rb = missile.GetComponent<Rigidbody2D>(); // on assigne le rigidbody du missile
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb2d.velocity = rb.velocity; // la vitesse du "Platform Releaser(1 ou 2)" est la vitesse du missile

    }
}
