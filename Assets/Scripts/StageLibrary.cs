using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLibrary
{
    public Dictionary<string, Color> stageColors = new Dictionary<string, Color>()
    {
        { "Grass", Color.green },
        { "Sand", Color.yellow },
        { "Mountain", Color.grey },
        { "Snowy", Color.white }
    };
    // This dictionary houses colors to use based on stage type.
}
