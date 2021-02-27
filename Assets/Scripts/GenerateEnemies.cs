using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject[] enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;

    public FruitTracker ft;

    int index;


    void Start()
    {
        StartCoroutine (EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (ft.numberActive() < 10)
        {
            xPos = Random.Range(-300, -230);
            zPos = Random.Range(110, 180);
            index = Random.Range(0, 5);

            Vector3 spawn = new Vector3(xPos, 40, zPos);

            Instantiate(enemy[index], spawn, Quaternion.identity);
            if (index == 1)
            {
                for (int i = 0; i < 9; i++)
                {
                    Instantiate(enemy[index], spawn, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                }
            }

            yield return new WaitForSeconds(5);
            ft.spawned += 1.0f;
        }
    }
    
}
