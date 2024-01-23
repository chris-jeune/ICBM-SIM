using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Ce script permet de gerer la selection d'un pays a partir des boutons correspondants, en modifiant
/// les valeurs booleennes correspondantes pour indiquer le pays selectionne. Les valeurs booleennes
/// sont utilisees par le script SceneLoader pour charger la scene correspondante.
/// </summary>
public class SelectionPays : MonoBehaviour
{
    // valeur booleenne de chaque pays pour indiquer leur statut de selection
    public bool us = false;
    public bool russ = false;
    public bool chine = false;
    public bool france = false;
    public bool gb = false;

    // les boutons de chaques pays (se fier a l'ordre des valeurs booleennes pour l'ordre des boutons
    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;
    public GameObject btn4;
    public GameObject btn5;
    public void OnSelect()
    {
        // Si un pays est selectionne son etat de selection est true et l'etat 
        // de selection de tous les autres est false
        if (EventSystem.current.currentSelectedGameObject == btn1)
        {
            us = true;
            russ = false;
            chine = false;
            france = false;
            gb = false;
        }

        if (EventSystem.current.currentSelectedGameObject == btn2)
        {
            us = false;
            russ = true;
            chine = false;
            france = false;
            gb = false;
        }

        if (EventSystem.current.currentSelectedGameObject == btn3)
        {
            us = false;
            russ = false;
            chine = true;
            france = false;
            gb = false;
        }

        if (EventSystem.current.currentSelectedGameObject == btn4)
        {
            us = false;
            russ = false;
            chine = false;
            france = true;
            gb = false;
        }

        if (EventSystem.current.currentSelectedGameObject == btn5)
        {
            us = false;
            russ = false;
            chine = false;
            france = false;
            gb = true;
        }
    }
}
