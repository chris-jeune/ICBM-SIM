using TMPro;
using UnityEngine;

/// <summary>
/// Permet de gerer la vitesse de la simulation en temps reel, et de la mettre en pause si necessaire.
/// La classe possede des variables pour stocker l'etat de pause du temps, et le temps precedent avant
/// la pause. Elle contient egalement des methodes pour diminuer ou augmenter la vitesse du temps, 
/// ainsi qu'une methode pour mettre en pause le temps. L'affichage de la vitesse actuelle du temps
/// est egalement gere dans cette classe a travers un objet TextMeshProUGUI.
/// </summary>
/// 
public class GestionTemps : MonoBehaviour
{
    // texte qui affiche la vitesse de la simulation
    public GameObject scaleTimeText;
    TextMeshProUGUI actualText;


    private bool isPause = false;// booleen pour l'etat du TimeScale, s'il est sur pause ou non
    private float previousTime = 0f;

    private void Start()
    {
        Time.timeScale = 1f;// initialisation du temps a 1
        actualText = scaleTimeText.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
	/// Pour gerer le bouton qui diminue la vitesse de simulation
	/// </summary>
    public void Previous()
    {
        // Diminution de la vitesse du temps de 0.25 si celle-ci est entre 0.5 et 1
        if (Time.timeScale >= 0.50f && Time.timeScale <= 1f)
        {
            Time.timeScale -= 0.25f;
            actualText.text = "x" + Time.timeScale.ToString();
        }
        // Diminution de l'ecoulement du temps en la divisant par deux si celle-ci est plus grande que 1
        else if (Time.timeScale > 1f)
        {
            Time.timeScale /= 2;
            actualText.text = "x" + Time.timeScale.ToString();
        }

    }

    /// <summary>
	/// Pour gerer le bouton qui augmente la vitesse de simulation
	/// </summary>
    public void Foward()
    {
        // Augmentation de l'ecoulement du temps en l'augmentant de 1 si le temps est mis a pause
        if (Time.timeScale == 0f)
        {
            Time.timeScale += 1f;
            actualText.text = "x" + Time.timeScale.ToString();
        }

        // Augmentation de l'ecoulement du temps en le multipliant par deux si celle-ci est plus grande que 1
        // mais plus petite que 16
        else if (Time.timeScale >= 1f && Time.timeScale < 16f)
        {
            Time.timeScale *= 2;
            actualText.text = "x" + Time.timeScale.ToString();
        }
        // Augmentation de la vitesse du temps de 0.25 si c'est plus petit que 1
        else if (Time.timeScale < 1)
        {
            Time.timeScale += 0.25f;
            actualText.text = "x" + Time.timeScale.ToString();
        }

    }

    /// <summary>
	/// Pour gerer le bouton qui met la simulation a pause
	/// </summary>
    public void Pause()
    {
        // si la simulation est deja a pause
        if (isPause)
        {
            Time.timeScale += previousTime;// on donne la vitesse de la simulation avant la pause
            isPause = false;
            actualText.text = "x" + Time.timeScale.ToString();
        }
        // sinon la simulation est mis a pause
        else
        {
            previousTime = Time.timeScale;// on enregistre le temps de la simulation avant la pause
            Time.timeScale = 0f;
            isPause = true;
            actualText.text = "||";
        }

    }
}
