using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetStageType();
        GenerateGround();
        GenerateObstacles();
        GenerateWalls();
    }
    void GetStageType()
    {

    }
    void GenerateGround()
    {
        //Creates a primitive plane that acts as the ground GameObject
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        
        //Sets name of GameObject to "Ground"
        ground.name = "Ground";

        //Sets position of GameObject to the origin
        ground.transform.position = new Vector3(0, 0, 0);

        //Multiplies local scale of ground by 5
        ground.transform.localScale = new Vector3(20,20,20);
        
        //Sets color of GameObject to a hue similar to professor's example
        var groundRenderer = ground.GetComponent<Renderer>();
        Color groundColor = new Color(0.8f, 0.4f, 0.4f);
        groundRenderer.material.SetColor("_Color", groundColor);
    }
    void GenerateObstacles()
    {

    }
    void GenerateWalls()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
