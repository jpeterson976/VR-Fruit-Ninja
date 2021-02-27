using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTracker : MonoBehaviour
{
    public float spawned = 0.0f;
    public float killed = 0.0f;

    public float numberActive()
    {
        return spawned - killed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
