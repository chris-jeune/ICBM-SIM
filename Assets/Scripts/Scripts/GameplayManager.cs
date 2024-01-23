using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Le gestionnaire de jeu est responsable de la gestion generale du deroulement du jeu. Il 
/// suit la progression du joueur et passe entre les etats de jeu en fonction des resultats
/// et des entrees utilisateur. Le gestionnaire de jeu est un singleton et peut etre accede dans 
/// n'importe quel script en utilisant la syntaxe GameplayManager.Instance.
/// </summary>
public class GameplayManager : MonoBehaviour
{
	// L'instance singleton statique du gestionnaire de jeu.
	public static GameplayManager Instance { get; private set; }

	float maxHeight;
	float maxSpeed;


	void Awake()
	{
		// Enregistrer ce script en tant qu'instance singleton.
		Instance = this;
	}

	void Start()
	{
		// Afficher l'ecran de tutoriel.
		UIManager.Instance.ShowScreen("Tutorial");
	}


	/// <summary>
	/// Appeler cette fonction lorsque le missile s'ecrase
	/// </summary>
	public void OnLaunchFinished()
	{
		UIManager.Instance.ShowScreen("Game Over");
	}

	/// <summary>
	/// Appeler cette fonction lorsque le missile va hors limites
	/// </summary>
	public void OnOutOfBounds()
    {
		UIManager.Instance.ShowScreen("Attention");
	}

	/// <summary>
	/// Appeler cette fonction lorsqu'on clique sur le bouton "Start"
	/// </summary>
	public void OnStartGame()
    {
		UIManager.Instance.ShowScreen("");
	}

	/// <summary>
	/// Appeler cette fonction pour recharger le niveau actuel. La progression du joueur sera reinitialisee.
	/// </summary>
	public void OnRetryLevel()
	{
		UIManager.Instance.ShowScreen("");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	/// <summary>
	/// Appeler cette fonction pour acceder a la page de selection de pays
	/// </summary>
	public void AutrePays()
    {
		UIManager.Instance.ShowScreen("");
		SceneManager.LoadScene(1);
	}

	/// <summary>
	/// Appeler cette fonction pour acceder a la page d'accueil
	/// </summary>
	public void Pageaccueil()
    {
		UIManager.Instance.ShowScreen("");
		SceneManager.LoadScene(0);
	}


	/// <summary>
	/// Mettre a jour les statistiques de vitesse et de hauteur.
	/// </summary>
	/// <param name="speed">Vitesse verticale actuelle de la fusee.</param>
	/// <param name="height">Hauteur actuelle de la fusee.</param>
	public void UpdateRocketInfo(float speed, float height)
	{
		maxHeight = Mathf.Max(height, maxHeight);
		maxSpeed = Mathf.Max(speed, maxSpeed);
		UIManager.Instance.UpdateHUD(maxSpeed, maxHeight);
	}
	
}