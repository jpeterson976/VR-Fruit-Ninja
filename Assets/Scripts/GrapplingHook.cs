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
        else if (attached)
        {
            transform.position = fruit.transform.position;
            fruit.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(left.transform.position - transform.position) * pullForce, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag.Equals("Fruit"))
        {
            extending = false;
            attached = true;
            fruit = other.gameObject;
        }
        else
            Reset();
    }

    public void Fire()
    {
        extending = true;
    }

    public void Reset()
    {
        rb.velocity = Vector3.zero;
        transform.position = left.transform.position;
        transform.rotation = left.transform.rotation;
        fruit = null;
        extending = false;
        attached = false;
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
