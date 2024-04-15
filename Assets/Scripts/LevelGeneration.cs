using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Vector2 stageSize = new Vector2(30, 30);
    // Total size of stage in X by Y format.
    public Transform tileParent;
    // GameObject that is the parent of the tiles.
    public GameObject tile;
    // GameObject prefab used as a base for the tiles.
    public GameObject wall;
    // GameObject prefab used for the wall.
    public GameObject stone;
    // GameObject prefab used for the stones.
    public string stageType = "Grass";
    // String for Stage Type. Can be Grass, Mountain, Sand, or Snowy.
    public int obstacleDensity = 7;
    // Int for how many obstacles are created. 
    //Color tileColor = new Color();
    // Color variable for holding the tile color. [OBSOLETE]

    StageLibrary stageLibrary = new StageLibrary();
    // Reference to the stage library class. Holds the dictionary with stage colors.

    ObstacleLibrary obstacleLibrary = new ObstacleLibrary();
    // Reference to the obstacle library class. Holds the information regarding the creation of new obstacles.

    public void Start()
    {
        //GetStageColor();
        CreateGround();
        GenerateWalls();
        GenerateObstacles();
    }
    // Runs on startup, runs all methods in order.

    /*public void GetStageColor()
    {
        if(!this.stageLibrary.stageColors.ContainsKey(stageType))
        {
            Debug.Log("Stage Type invalid - setting to grass.");
            this.stageType = "Grass";
            this.tileColor = Color.green;
            return;
        }
        this.tileColor = stageLibrary.stageColors[stageType];
    }*/
    // First, checks if the stage type is in the stage library. If not, defaults the value to grass.
    // Otherwise, it will set the stage color according to the stage type.
    // [OBSOLETE], color is not needed.

    public void CreateGround()
    {
        /*for(int i = 0; i < stageSize.y; i++)
        {
            for(int j = 0; j < stageSize.x; j++)
            {
                GameObject newTile = Instantiate(this.tile, this.tileParent);
                newTile.name = "FloorTile";
                newTile.transform.localPosition = new Vector2(j, i);
            }
        }
        this.tileParent.position = new Vector2(((stageSize.x / 2) - 0.5f) * -1, ((stageSize.y / 2) - 0.5f) * -1);*/

        GameObject floorTile = Instantiate(this.tile, this.tileParent);
        floorTile.name = "FloorTile";
        floorTile.GetComponent<SpriteRenderer>().size = new Vector2(stageSize.x, stageSize.y);
    }
    // First, runs the for loops that tile out the stage based on the stage size vector2. [OBSOLETE]
    // It creates a tile under a parent, names it, moves the local position to be based on j and i in the loops, then sets the color. [OBSOLETE]
    // After all tiles are created, it centers the stage around (0, 0) by moving the parent. [OBSOLETE]
    // New Method simply makes a big tile instead of a bajillion. Problem solved!

    public void GenerateWalls()
    {
        GameObject wallTileWest = Instantiate(this.wall, this.tileParent);
        wallTileWest.name = "wallTileWest";
        wallTileWest.transform.position = new Vector2(((stageSize.x / 2) + 9f) * -1, 0);
        // moves to the left side.
        wallTileWest.GetComponent<SpriteRenderer>().size = new Vector2 (18, stageSize.y);
        // scales up the y.
        wallTileWest.AddComponent<BoxCollider2D>();
        wallTileWest.GetComponent<BoxCollider2D>().size = new Vector2 (18, stageSize.y);

        GameObject wallTileEast = Instantiate(this.wall, this.tileParent);
        wallTileEast.name = "wallTileEast";
        wallTileEast.transform.position = new Vector2(((stageSize.x / 2) + 9f), 0);
        // moves to the rght.
        wallTileEast.GetComponent<SpriteRenderer>().size = new Vector2 (18, stageSize.y);
        // scales up the y.
        wallTileEast.AddComponent<BoxCollider2D>();
        wallTileEast.GetComponent<BoxCollider2D>().size = new Vector2 (18, stageSize.y);

        GameObject wallTileNorth = Instantiate(this.wall, this.tileParent);
        wallTileNorth.name = "wallTileNorth";
        wallTileNorth.transform.position = new Vector2(0, ((stageSize.y / 2) + 5f));
        // moves to the top.
        wallTileNorth.GetComponent<SpriteRenderer>().size = new Vector2 (stageSize.x + 36, 10);
        // scales up the x, adds 2 to create a perfect box.
        wallTileNorth.AddComponent<BoxCollider2D>();
        wallTileNorth.GetComponent<BoxCollider2D>().size = new Vector2 (stageSize.x + 36, 10);

        GameObject wallTileSouth = Instantiate(this.wall, this.tileParent);
        wallTileSouth.name = "wallTileSouth";
        wallTileSouth.transform.position = new Vector2(0, ((stageSize.y / 2) + 5f) * -1);
        // moves to the bottom.
        wallTileSouth.GetComponent<SpriteRenderer>().size = new Vector2 (stageSize.x + 36, 10);
        // scales up the x, adds 2 to create a perfect box.
        wallTileSouth.AddComponent<BoxCollider2D>();
        wallTileSouth.GetComponent<BoxCollider2D>().size = new Vector2 (stageSize.x + 36, 10);
    }
    // Creates walls in each cardinal direction.
    // First, the game object is created, then named, moved based on stage size, scaled based on stage size, has it's color changed to black, and adds a box colider 2D.

    public void GenerateObstacles()
    {
        if(stageType == "Grass")
        {
            for(int i = 0; i < obstacleDensity; i++)
            {
               GameObject obstacle = this.obstacleLibrary.CreateStoneObstacles(this.stone);
               obstacle.transform.position = new Vector2 (Random.Range(((stageSize.x / 2) - 1) * -1, (stageSize.x / 2) -1), Random.Range(((stageSize.y / 2) - 1) * -1, (stageSize.y / 2) - 1));
            }
        }

        /*if(stageType == "Sand")
        {
            for(int i = 0; i < obstacleDensity; i++)
            {
               GameObject obstacle = this.obstacleLibrary.CreateCactusObstacles(this.tile);
               obstacle.transform.position = new Vector2 (Random.Range(((stageSize.x / 2) - 1) * -1, (stageSize.x / 2) -1), Random.Range(((stageSize.y / 2) - 1) * -1, (stageSize.y / 2) - 1));
            }
        }

        if(stageType == "Mountain" || stageType == "Snowy")
        {
            for(int i = 0; i < obstacleDensity; i++)
            {
               GameObject obstacle = this.obstacleLibrary.CreateTreeObstacles(this.tile);
               obstacle.transform.position = new Vector2 (Random.Range(((stageSize.x / 2) - 1) * -1, (stageSize.x / 2) -1), Random.Range(((stageSize.y / 2) - 1) * -1, (stageSize.y / 2) - 1));
            }
        }*/
    }
    // Checks for the stage type, and generates the appropriate obstacles based on that. Moves them to random locations within the bounds of the stage.
}