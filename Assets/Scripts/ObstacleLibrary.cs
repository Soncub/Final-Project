using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLibrary
{
    public GameObject CreateStoneObstacles(GameObject tileBase)
    {
        GameObject newObstacle = GameObject.Instantiate(tileBase);
        newObstacle.name = "Stone";
        newObstacle.AddComponent<BoxCollider2D>();
        return newObstacle;
    }
    // Generates "stone" obstacles. Thin and grey. Meant for Grass.

    /*public GameObject CreateCactusObstacles(GameObject tileBase)
    {
        GameObject newObstacle = GameObject.Instantiate(tileBase);
        newObstacle.name = "Cactus";
        newObstacle.GetComponent<SpriteRenderer>().color = Color.green;
        newObstacle.transform.localScale = new Vector2(0.4f, 0.8f);
        newObstacle.AddComponent<BoxCollider2D>();
        return newObstacle;
    }
    // Generates "cactus" obstacles. Tall and green. Meant for Sand.

    public GameObject CreateTreeObstacles(GameObject tileBase)
    {
        GameObject newObstacle = GameObject.Instantiate(tileBase);
        newObstacle.name = "Cactus";
        newObstacle.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.5f, 0.25f);
        newObstacle.transform.localScale = new Vector2(0.3f, 1.0f);
        newObstacle.AddComponent<BoxCollider2D>();
        return newObstacle;
    }
    // Generates "tree" obstacles. Meant for Mountain and Snowy.*/
    // [OBSOLETE], We only use stones with the one available level theme.
}
