using UnityEngine;
using UnityEngine.UI;

///<summary>
///Ce script permet de controler l'activation et la desactivation du canvas en reponse a un clic sur un bouton. 
///Lorsque le bouton est clique, le canvas change d'etat, passant de l'etat actif a l'etat desactive ou vice versa.
/// </summary>
public class CanvasManager : MonoBehaviour
{
    public GameObject canvas; // Reference a l'objet de type GameObject representant le canvas dans la scene.

    public void OnButtonClick()
    {
        canvas.SetActive(!canvas.activeSelf); // Activer ou desactive le canvas en inversant son etat actuel.
    }
}

