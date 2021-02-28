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
            Debug.Log("the player hit the thing");
            if (this.gameObject.name.Equals("Cake(clone)"))
            {
                collision.gameObject.GetComponentInParent<HealthSystem>().Heal(healthAmount);
            }

            if (this.gameObject.name.Equals("Pie(Clone)"))
            {
                collision.gameObject.GetComponentInParent<HealthSystem>().Heal(healthAmount);
            }

            Destroy(gameObject);
        }
   }


}
