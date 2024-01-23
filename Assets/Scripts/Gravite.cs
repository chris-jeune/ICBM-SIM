using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script definit le centre de masse d'un missile en utilisant un Rigidbody2D. 
/// La variable "CenterOfMass2" est un vecteur qui definit la position du centre de masse du missile,
/// et est utilisee pour affecter le centre de masse du Rigidbody2D. La methode "FixedUpdate" est
/// appelee a chaque frame et met a jour le centre de masse du Rigidbody2D. La methode "OnDrawGizmos"
/// dessine une sphere rouge pour visualiser le centre de masse du missile dans l'editeur.
/// Ce script est annote avec l'attribut "ExecuteInEditMode", ce qui permet d'executer le script 
/// pendant que l'on travaille dans l'editeur Unity.
/// </summary>
[ExecuteInEditMode]
public class Gravite : MonoBehaviour
{
    public Vector3 CenterOfMass2;// Vecteur qui correspond au centre de masse du missile

    protected Rigidbody2D r;// Rigidbody2D du missile

    void Start()
    {
        r = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        r.centerOfMass = CenterOfMass2;// assignation du vecteur au centre de masse du missile
        r.WakeUp();
       
    }

    /// <summary>
	/// Dessine une sphere de couleur rouge, afin de visualiser le centre de masse dans l'editeur
	/// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + transform.rotation * CenterOfMass2, 0.1f/10);
    }


}
