using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int healthAmount;

    private void OnTriggerEnter(Collider collision)
    {
        // projectile hits the player
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (this.gameObject.name.Equals("Cake(clone)"))
            {
                collision.gameObject.GetComponent<HealthSystem>().Heal(healthAmount);
            }

            if (this.gameObject.name.Equals("Pie(Clone)"))
            {
                collision.gameObject.GetComponent<HealthSystem>().Heal(healthAmount);
            }

            if (this.gameObject.name.Equals("Shuriken(Clone)"))
            {
                collision.gameObject.GetComponent<Player>().shurikenCount++;
            }

            Destroy(gameObject);
        }
   }


}
