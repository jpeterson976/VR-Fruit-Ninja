using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePickups : MonoBehaviour
{
    public GameObject[] pickup;
    public int xPos;
    public int zPos;

    int index;

    void Start()
    {
        StartCoroutine (PickupDrop());
    }

    IEnumerator PickupDrop()
    {
        while (true)
        {
            xPos = Random.Range(-300, -230);
            zPos = Random.Range(110, 180);
            index = Random.Range(0, 5);

            Vector3 spawn = new Vector3(xPos, 40, zPos);

            Instantiate(pickup[index], spawn, Quaternion.identity);

            yield return new WaitForSeconds(30);
        }
    }
}
