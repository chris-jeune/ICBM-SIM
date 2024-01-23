using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script permet de faire suivre l'image associee au missile en lui donnant la meme rotation que celui-ci.
/// </summary>
public class ImageFollow : MonoBehaviour
{
    public Transform missileTransform; // reference vers le transform du missile
    void Start()
    {
        transform.rotation = missileTransform.rotation; // l'image a la meme rotation que le missile
    }

    void FixedUpdate()
    {
        transform.rotation = missileTransform.rotation;// a chaque mise a jour l'image suit la meme rotation que celle du missile
    }
}
