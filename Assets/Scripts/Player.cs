using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SteamVR_TrackedController left;
    public SteamVR_TrackedController right;
    public GameObject shurikenPreview;
    public GameObject shuriken;
    public Text shurikenCounter;
    public GrapplingHook hook;
    public SugarRush sugar;

    public float jumpForce;
    public float walkSpeed;
    public float flySpeed;
    public float flyLimit;

    public bool slimed = false;
    public GameObject slimedDisplay;
    private float slimeCounter = 0;

    public int shurikenCount = 5;
    private bool isGrounded;
    private bool isFlying;
    private float flyTimer = 0f;
    private float sugarCD = 0f;
    private bool sugarRush = false;

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
        if (sugarRush)
        {
            sugarCD -= Time.deltaTime;
            sugar.SetTime(sugarCD);

            if (sugarCD <= 0)
            {
                sugar.gameObject.SetActive(false);
                sugarRush = false;
            }
        }

        if (slimed)
        {
            slimeCounter += Time.deltaTime;
            slimedDisplay.SetActive(true);
        }
        
        if (slimeCounter >= 5)
        {
            slimeCounter = 0;
            slimed = false;
            slimedDisplay.SetActive(false);
        }

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

        if (left.gripped && !hook.IsActive())
            hook.SetActive(true);
        else if (!left.gripped && hook.IsActive())
            hook.SetActive(false);

        if (left.triggerPressed)
        {
            if (right.triggerPressed)
            {
                GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");

                foreach (GameObject fruit in fruits)
                {
                    FNShootProjectile sp = fruit.GetComponent<FNShootProjectile>();

                    if (sp != null)
                        sp.Moldy();

                    sugar.gameObject.SetActive(true);
                    sugarRush = true;
                    sugarCD = 5f;
                    fruit.GetComponent<EnemyMovement>().Moldy();
                }
            }
            else if (left.gripped)
                hook.Fire();
            else if (shurikenCount > 0)
                shurikenPreview.SetActive(true);
        }
        else
        {
            if (shurikenPreview.activeInHierarchy)
            {
                shurikenPreview.SetActive(false);
                GameObject.Instantiate(shuriken, left.gameObject.transform.position, left.gameObject.transform.rotation);
                shurikenCount--;
                shurikenCounter.text = "Shurikens: " + shurikenCount;
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
