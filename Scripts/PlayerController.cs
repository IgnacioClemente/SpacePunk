using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    public GameObject shot;
    public Transform shotSpawn;
    public Transform shotSpawn_1;
    public Transform shotSpawn_2;
    public float fireRateSingle;
    public float fireRateDouble;
    public float health = 15;
    public float rotationSpeed = 15;
    private float nextFireSingle;
    private float nextFireDouble;
    public bool isAlive = true;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFireSingle)
        {
            nextFireSingle = Time.time + fireRateSingle;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
        if(Input.GetMouseButton(1) && Time.time > nextFireDouble)
        {
            nextFireDouble = Time.time + fireRateDouble;
            Instantiate(shot, shotSpawn_1.position, shotSpawn_1.rotation);
            Instantiate(shot, shotSpawn_2.position, shotSpawn_2.rotation);
        }

        if(health < 0)
        {
            isAlive = false;
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.right * -speed * Time.deltaTime);
            //transform.position -= Vector3.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
            //transform.position += Vector3.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, 1);
            // transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -1);
            // transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }

    }

    /* void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.CompareTag("EnemyFlag"))
         {
             other.gameObject.SetActive(false);
             Debug.Log("You have the enemy flag");
         }
     }*/
}
