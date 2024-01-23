using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Ce code contient une classe qui permet de charger des scenes dans Unity. 
/// Il contient trois methodes, "VerifSelec", "LoadNextLevel", et deux methodes
/// IEnumerator, "LoadLevel" et "LoadCountry". La methode "VerifSelec" verifie
/// quelle scene de pays a ete selectionnee et charge la scene correspondante. 
/// La methode "LoadNextLevel" charge la scene suivante dans l'ordre des scenes. 
/// Les deux methodes IEnumerator "LoadLevel" et "LoadCountry" gerent la transition 
/// et le chargement de la scene de maniere asynchrone.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public Animator transition;// animation de transition
    public float transitionTime = 1f;// temps de transition
    public GameObject canvas;// le canvas sur lequels sont les boutons des pays

    /// <summary>
	/// Verifie la selection du pays et load la scene en consequence
	/// </summary>
    public void VerifSelec()
    {
        // Si le booleen du pays respectif est vrai sa scene est loade
        if (canvas.GetComponent<SelectionPays>().us)
        {
            StartCoroutine(LoadCountry("Etat-Unis"));
        }
        else if (canvas.GetComponent<SelectionPays>().russ)
        {
            StartCoroutine(LoadCountry("Russie"));
        }
        else if (canvas.GetComponent<SelectionPays>().chine)
        {
            StartCoroutine(LoadCountry("Chine"));
        }
        else if (canvas.GetComponent<SelectionPays>().france)
        {
            StartCoroutine(LoadCountry("France"));
        }
        else if (canvas.GetComponent<SelectionPays>().gb)
        {
            StartCoroutine(LoadCountry("Angleterre"));
        }
    }

    /// <summary>
	/// Amorce la transition et change de scene a celle dont l'index est 1 de plus
	/// </summary>
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    /// <summary>
    /// Gestionnaire de la transition et du changement de scene entre "Page d'accueil" et "Choisi Pays"
    /// </summary>
    /// <param name="levelIndex">Index de la scene a charger.</param>
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");// debut de la transition

        // une seconde apres le debut de l'animation on load la scene desire
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }

    /// <summary>
    /// Gestionnaire de la transition et du changement de scene entre "Choisi Pays" et les differents pays
    /// </summary>
    /// <param name="country">Nom de la scene a charger.</param>
    IEnumerator LoadCountry(string country)
    {
        transition.SetTrigger("Start");// debut de la transition

        // une seconde apres le debut de l'animation on load la scene desire
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(country);
    }
}
