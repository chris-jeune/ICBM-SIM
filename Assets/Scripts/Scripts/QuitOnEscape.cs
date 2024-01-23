using UnityEngine;
using System.Collections;

/// <summary>
/// Script pour detecter si la touche Echap est pressee. Si c'est le cas, cela quittera le jeu.
/// </summary>
public class QuitOnEscape : MonoBehaviour
{
	void LateUpdate()
	{
		// Verifie si la touche ESC est pressee.
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// Quitte l'application.
			Application.Quit();
		}
	}
}
