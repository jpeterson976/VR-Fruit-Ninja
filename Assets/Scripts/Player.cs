using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float walkSpeed;
    public float flySpeed;
    public float flyLimit;

    private bool isGrounded;
    private bool isFlying;
    private float flyTimer = 0f;

    private Rigidbody rb;
    private GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        head = GameObject.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
            flyTimer += Time.deltaTime;
        if (flyTimer >= flyLimit)
        {
            isFlying = false;
            rb.useGravity = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            else if (flyTimer < flyLimit)
            {
                isFlying = true;
                rb.useGravity = false;

                Vector3 vel = rb.velocity;
                vel.y = 0;
                rb.velocity = vel;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (isFlying)
            {
                Vector3 pos = transform.position;
                Vector3 delta = head.transform.forward;

                pos += delta * flySpeed * Time.deltaTime;

                transform.position = pos;
            }
            else
            {
                Vector3 pos = transform.position;
                Vector3 delta = head.transform.forward;

                delta.y = 0;
                pos += delta * walkSpeed * Time.deltaTime;

                transform.position = pos;
            }
        }
    }

    private void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.tag.Equals("Ground"))
        {
            Vector3 vel = rb.velocity;
            vel.y = 0f;
            rb.velocity = vel;

            flyTimer = 0f;
            isFlying = false;
            isGrounded = true;
            rb.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider Other)
    {
        if (Other.gameObject.tag.Equals("Ground"))
        {
            isGrounded = false;
            rb.useGravity = true;
        }
    }

}
