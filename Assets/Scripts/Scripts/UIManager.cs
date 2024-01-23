using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Le gestionnaire d'interface utilisateur (UI) est responsable de controler quel ecran afficher
/// ainsi que de mettre a jour le HUD. Il est un singleton
/// et peut etre accede dans n'importe quel script en utilisant la syntaxe UIManager.Instance.
/// </summary>
public class UIManager : MonoBehaviour
{
	// L'instance singleton statique du gestionnaire d'interface utilisateur..
	public static UIManager Instance { get; private set; }

	// Panneau de controle


	[SerializeField]
	TextMeshProUGUI maxSpeedText = null;


	[SerializeField]
	TextMeshProUGUI maxHeightText = null;

	[SerializeField]
	GameObject[] screens = {};  // Tableau GameObject pour tous les ecrans.

	void Awake()
	{
		// Enregistre ce script en tant qu'instance singleton.
		Instance = this;
	}

	/// <summary>
	/// Affiche l'ecran avec le nom donne et masque tout le reste.
	/// </summary>
	/// <param name="name">Nom de l'ecran a afficher.</param>
	public void ShowScreen(string name)
	{
		// Parcourt tous les ecrans du tableau.
		foreach (GameObject screen in screens)
		{
			// Active l'ecran avec le nom correspondant et desactive
			// tout ecran qui ne correspond pas.
			screen.SetActive(screen.name == name);
		}
	}


	/// <summary>
	/// Met a jour les informations de la vitesse et hauteur maximale
	/// </summary>
	/// <param name="maxSpeed">Vitesse maximale.</param>
	/// /// <param name="maxHeight">Hauteur maximale.</param>
	public void UpdateHUD(float maxSpeed, float maxHeight)
	{
		ShowMaxSpeed(maxSpeed);
		ShowMaxHeight(maxHeight);
	}


	/// <summary>
	/// Met a jour le texte de la vitesse maximale dans l'interface utilisateur.
	/// </summary>
	/// <param name="maxSpeed">Vitesse maximale.</param>
	void ShowMaxSpeed(float maxSpeed)
	{
		// 1 decimal
		maxSpeedText.text = "Vitesse maximale:  " + maxSpeed.ToString("n1") + " km/s ";
	}



	/// <summary>
	/// Met a jour le texte de la hauteur maximale dans l'interface utilisateur.
	/// </summary>
	/// <param name="maxHeight">Hauteur maximale.</param>
	void ShowMaxHeight(float maxHeight)
	{
		// 1 decimal 
		maxHeightText.text = "Hauteur maximale:  " + maxHeight.ToString("n1") + " km";
	}
	
}