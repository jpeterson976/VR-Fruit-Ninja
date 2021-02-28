using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public SteamVR_TrackedController left;
    public float speed;
    public float pullForce;

    private Rigidbody rb;
    private GameObject player;
    public bool extending;
    public bool attached;
    private GameObject fruit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("[CameraRig]");
    }

    void Update()
    {
        if (extending)
        {
            transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
        }
        if (attached)
        {
            fruit.transform.position = left.transform.position;
            fruit.transform.rotation = left.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Fruit"))
        {
            extending = false;
            attached = true;
            fruit = other.gameObject;
            fruit.transform.SetParent(left.transform);
            fruit.transform.position = left.transform.position;
            fruit.GetComponent<EnemyMovement>().Grab();
            FNShootProjectile sp = fruit.GetComponent<FNShootProjectile>();

            if (sp != null)
                sp.Grab();

            Attach();
        }
        else
            Reset();

    }

    public void Fire()
    {
        extending = true;
    }

    public void Attach()
    {
        rb.velocity = Vector3.zero;
        transform.position = left.transform.position;
        transform.rotation = left.transform.rotation;
        extending = false;
    }

    public void Reset()
    {
        rb.velocity = Vector3.zero;
        transform.position = left.transform.position;
        transform.rotation = left.transform.rotation;
        extending = false;
        attached = false;

        if (fruit != null)
        {
            fruit.transform.SetParent(null);
            fruit.GetComponent<EnemyMovement>().Ungrab();
            FNShootProjectile sp = fruit.GetComponent<FNShootProjectile>();

            if (sp != null)
                sp.Ungrab();

            fruit = null;
        }
    }

    public void SetActive(bool active)
    {
        if (!active)
            Reset();

        gameObject.SetActive(active);
    }

    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }
}
