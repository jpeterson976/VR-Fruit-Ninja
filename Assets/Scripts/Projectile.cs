using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageAmount;
    public float projectileSpeed;

    private float lifetime = 0.0f;

    void FixedUpdate()
    {
        transform.position = transform.position + (transform.forward * projectileSpeed * Time.smoothDeltaTime);
        lifetime += Time.deltaTime;

        if (lifetime > 10)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // projectile hits the player
        if (collision.gameObject.name.Equals("Camera (eye)"))
        {
            // watermelon spits seeds
            if (this.gameObject.name.Equals("Seed(Clone)"))
            {
                collision.gameObject.GetComponentInParent<HealthSystem>().Damage(damageAmount);
                collision.gameObject.GetComponentInParent<HealthSystem>().ShowDamage();
            }

            // apple spits poison (damage over time)
            if (this.gameObject.name.Equals("Poison(Clone)"))
            {
                collision.gameObject.GetComponentInParent<HealthSystem>().Damage(damageAmount * Time.deltaTime);
                collision.gameObject.GetComponentInParent<HealthSystem>().ShowDamage();
            }

            // avocado slimes the screen
            if (this.gameObject.name.Equals("Slime(Clone)"))
            {
                collision.gameObject.GetComponentInParent<Player>().slimed = true;
            }

        }

        if (collision.gameObject.tag.Equals("Fruit"))
        {
            collision.gameObject.GetComponent<HealthSystem>().Damage(damageAmount);
        }

        // SHOULD anything happen here? maybe a sound effect?
        if (collision.gameObject.name.Equals("Katana_LODA"))
        {
            Debug.Log("sword collision");
        }

        Destroy(gameObject);
   }
}
