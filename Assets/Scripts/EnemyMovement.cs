using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float damageAmount;

    // peaches can jump
    public float jumpForce;
    private int counter;
    private int jumpFrequency = 100;
    private bool isMoldy = false;
    private float moldTime = 5f;
    private float moldTimer = 0f;
    private bool isGrabbed = false;
    private Rigidbody rb;

    private GameObject player;
    private HealthSystem hs;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Camera (eye)");

        hs = GetComponent<HealthSystem>();

        counter = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
            return;

        counter++;

        if (isMoldy)
        {
            moldTimer += Time.deltaTime;

            if (moldTimer >= moldTime)
            {
                isMoldy = false;
                moldTimer = 0f;
            }

            return;
        }
        
        this.gameObject.transform.LookAt(player.transform);    
        transform.position = transform.position + (transform.forward * 0 * Time.smoothDeltaTime);

        if (this.gameObject.name.Equals("peach(Clone)"))
            if (counter == jumpFrequency)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                counter = 0;
            }
        
        if (hs.isDead())
        {
            if (this.gameObject.name.Equals("strawberry(Clone)"))
                player.GetComponent<FruitTracker>().killed += 0.1f;
            else
                player.GetComponent<FruitTracker>().killed += 1.0f;
            Destroy(this.gameObject);
        }
        
    }

    public void Moldy()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        isMoldy = true;
        moldTimer = 0f;
    }

    public void Grab()
    {
        isGrabbed = true;
        rb.useGravity = false;
        // transform.localScale = transform.localScale / 2f;
    }

    public void Ungrab()
    {
        isGrabbed = false;
        rb.useGravity = true;
        // transform.localScale = transform.localScale * 2f;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponentInParent<HealthSystem>().Damage(damageAmount);
        }
    }
}
