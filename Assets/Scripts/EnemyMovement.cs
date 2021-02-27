using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.0f;

    // peaches can jump
    public float jumpForce;
    private int counter;
    private int jumpFrequency = 50;
    private Rigidbody rb;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Camera");

        counter = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        counter++; 

        this.gameObject.transform.LookAt(target.transform);    
        transform.position = transform.position + (transform.forward * speed * Time.smoothDeltaTime);

        if (this.gameObject.name.Equals("peach(Clone)"))
            if (counter == jumpFrequency)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                counter = 0;
            }
    }
}
