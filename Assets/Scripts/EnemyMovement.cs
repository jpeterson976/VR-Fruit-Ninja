using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.0f;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(target.transform);
        transform.position = transform.position + (transform.forward * speed * Time.smoothDeltaTime);
    }
}
