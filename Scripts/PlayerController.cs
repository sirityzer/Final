using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public GameObject shield;
    private GameObject shieldInstance;
    public Transform shieldSpawn;
    public float fireRate;
    

    private float nextFire;
    private float slowTimer;
    private bool slow;

    private Rigidbody rb;

    private AudioSource audioSource;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        slow = false;
        slowTimer =  0.0F;
        
    }
    public void Shield()
    {
        if(shieldInstance == null)
        {
            shieldInstance = Instantiate(shield, shieldSpawn.position, shieldSpawn.rotation);
        }
        
        
    }
    public void Slow()
    {
        slow = true;
    }
    void Update()
    {
        if (Input.GetButton("Fire1")&& Time.time > nextFire)
        {
            if (slow)
            {
                nextFire = Time.time + fireRate * 3;
            }
            else
            {
                nextFire = Time.time + fireRate;
            }

            //GameObject clone = 
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //as GameObject;
            audioSource.Play();
        }
        if (slow)
        {
            slowTimer += Time.deltaTime;
            if (slowTimer>=5F)
            {
                slow = false;
                slowTimer = 0f;
            }
        }
    }

    void FixedUpdate()
    { 
            
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
            //Debug.Log(moveHorizontal);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

        // if has shield move it with player
        if (shieldInstance != null)
        {
            shieldInstance.GetComponent<Rigidbody>().position=rb.position;
        }
        
    }
}

