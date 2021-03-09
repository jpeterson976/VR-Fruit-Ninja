using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FNShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public int fireRate = 100;

    private GameObject target;
    private int counter = 0;
    private bool isMoldy = false;
    private float moldTime = 5.0f;
    private float moldTimer = 0.0f;
    private bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrabbed)
            return;

        counter++;

        if (isMoldy)
        {
            moldTimer += Time.fixedDeltaTime;

            if (moldTimer >= moldTime)
            {
                isMoldy = false;
                moldTimer = 0f;
            }

            return;
        }

        if (counter == fireRate)
        {
            GameObject.Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation);
            if (this.gameObject.GetComponent<AudioSource>() != null)
                this.gameObject.GetComponent<AudioSource>().Play();
    
            counter = 0;
        }
    }

    public void Moldy()
    {
        isMoldy = true;
        moldTimer = 0f;
    }

    public void Grab()
    {
        isGrabbed = true;
    }

    public void Ungrab()
    {
        isGrabbed = false;
    }
}
