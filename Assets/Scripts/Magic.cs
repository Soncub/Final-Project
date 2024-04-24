using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public float pointIncreasePerSecond;
    // Start is called before the first frame update
    void Start()
    {
        pointIncreasePerSecond = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Protect()
    {

    }
    void Regen()
    {
        //currentHealth +=pointIncreasePerSecond * Time.deltaTime;
    }
    void Saber()
    {

    }
}
