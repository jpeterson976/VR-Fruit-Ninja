using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Fruit"))
        {
            other.gameObject.GetComponent<HealthSystem>().Damage(damage);
            other.gameObject.GetComponent<Rigidbody>().AddForce(other.gameObject.transform.forward * -1f * 2f, ForceMode.Impulse);
        }
    }
}
