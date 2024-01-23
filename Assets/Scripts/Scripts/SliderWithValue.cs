using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

/// <summary>
/// Un script qui controle un curseur avec une etiquette de texte.
/// Ce script modifie egalement l'etiquette de texte en mode edition.
/// </summary>
public class SliderWithValue : MonoBehaviour
{
	[SerializeField]
	Slider slider = null;
	[SerializeField]
	GameObject text = null;
	[SerializeField]
	int decimals = 0;
	TextMeshProUGUI actualText;

	void Start()
	{	
		actualText= text.GetComponent<TextMeshProUGUI>();
		ChangeValue(slider.value);
	}

	void Update()
	{
		decimals = Mathf.Max(decimals, 0);
	}

	void OnEnable()
	{
		// Ajoute un ecouteur d'evenement au curseur
		slider.onValueChanged.AddListener(ChangeValue);
	}

	void OnDisable()
	{
		// Supprime l'ecouteur d'evenement du curseur
		slider.onValueChanged.RemoveListener(ChangeValue);
	}

	/// <summary>
	/// Met a jour l'etiquette de texte avec la valeur du curseur.
	/// </summary>
	/// <param name="value">La valeur a mettre a jour.</param>
	void ChangeValue(float value)
	{
		actualText.text = value.ToString("n" + decimals);
	}
}
