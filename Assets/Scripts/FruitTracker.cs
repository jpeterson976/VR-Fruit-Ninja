using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTracker : MonoBehaviour
{
    private int spawned = 0;
    private int killed = 0;

    public int numberSpawned()
    {
        return spawned;
    }

    public int numberKilled()
    {
        return killed;
    }

    public int numberActive()
    {
        return spawned - killed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
