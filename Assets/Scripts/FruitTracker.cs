using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitTracker : MonoBehaviour
{
    public float spawned = 0.0f;
    public float killed = 0.0f;

    public Text score;

    public float numberActive()
    {
        return spawned - killed;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + (killed * 100);
    }
}
