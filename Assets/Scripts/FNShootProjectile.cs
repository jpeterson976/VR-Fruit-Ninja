using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FNShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public int fireRate = 100;

    private GameObject target;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;

        if (counter == fireRate)
        {
            GameObject.Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation);
            // this.gameObject.GetComponent<AudioSource>().Play();
            counter = 0;
        }
    }
}
