using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageAmount;
    public float projectileSpeed;

    void FixedUpdate()
    {
        transform.position = transform.position + (transform.forward * projectileSpeed * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // projectile hits the player
        if (collision.gameObject.tag.Equals("Player"))
        {
            // watermelon spits seeds
            if (this.gameObject.name.Equals("Seed(Clone"))
            {
                collision.gameObject.GetComponent<HealthSystem>().Damage(damageAmount);
            }

            // apple spits poison (damage over time)
            if (this.gameObject.name.Equals("Poison(Clone)"))
            {
                collision.gameObject.GetComponent<HealthSystem>().Damage(2 * Time.deltaTime);
            }
        }

        // SHOULD anything happen here? maybe a sound effect?
        if (collision.gameObject.name.Equals("Katana_LODA"))
        {
            Debug.Log("sword collision");
        }
   }
}
