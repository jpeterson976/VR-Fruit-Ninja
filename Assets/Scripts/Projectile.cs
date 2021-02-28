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
                collision.gameObject.GetComponent<HealthSystem>().ShowDamage();
            }

            // apple spits poison (damage over time)
            if (this.gameObject.name.Equals("Poison(Clone)"))
            {
                collision.gameObject.GetComponent<HealthSystem>().Damage(damageAmount * Time.deltaTime);
                collision.gameObject.GetComponent<HealthSystem>().ShowDamage();
            }

            // avocado slimes the screen
            if (this.gameObject.name.Equals("Slime(Clone)"))
            {
                collision.gameObject.GetComponent<Player>().slimed = true;
            }
        }

        // SHOULD anything happen here? maybe a sound effect?
        if (collision.gameObject.name.Equals("Katana_LODA"))
        {
            Debug.Log("sword collision");
        }
   }
}
