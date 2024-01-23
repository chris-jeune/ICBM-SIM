using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script est attache a un gameObject qui represente la trajectoire du missile sur une minimap. Il suit la position et
/// la rotation du missile principal et ajoute un point a chaque frame pour visualiser la trajectoire. 
/// </summary>
public class miniMapFollow : MonoBehaviour
{
    public Transform missile; // reference vers le transform du missile
    public GameObject realmissile;// reference vers le gameObject "Vrai missile"
    public GameObject pointPrefab;// prefab pour un point de la trajectoire du missile (le gameObject est "Triangle")
    private RectTransform rt;// position du Triangle dans le Canva

    // Ajustement de la position du point au debut et a chaque mise a jour en fonction
    // de la position du missile de sorte a ce que ca rentre dans le canvas
    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2((missile.transform.position.x)/13, (missile.transform.position.y)/7f)  + new Vector2(20, 35f);
    }

    void FixedUpdate()
    {

        rt.anchoredPosition = new Vector2((missile.transform.position.x)/13, (missile.transform.position.y)/7f)  + new Vector2(20, 35f);

    }

    private void Update()
    {
        // ajout d'un point a chaque frame
        if (realmissile.GetComponent<Mouvements>().launch)
        {
            GameObject point = Instantiate(pointPrefab);
            point.transform.position = this.transform.position;
            point.transform.localScale = new Vector3(154, 154, 154);
            point.GetComponent<SpriteRenderer>().sortingOrder = -5;

        }
    }
}
