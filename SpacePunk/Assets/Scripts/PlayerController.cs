using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public Transform shotSpawn_1;
    public Transform shotSpawn_2;
    public float fireRateSingle;
    public float fireRateDouble;
    public float health = 15;
    private float nextFireSingle;
    private float nextFireDouble;
    public bool isAlive = true;

    float accelerationForce;

    void Update()
    {
        accelerationForce = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
        var rotationForce = Input.GetAxis("Horizontal") * 3.0f;

        transform.eulerAngles += new Vector3(0, rotationForce, 0);
        transform.Translate(-accelerationForce, 0, 0);

        if (Input.GetMouseButtonDown(0) && Time.time > nextFireSingle)
        {
            nextFireSingle = Time.time + fireRateSingle;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Ray ray = new Ray(shotSpawn.position, shotSpawn.right*10);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitobject = hit.transform.gameObject;
                if (hitobject.GetComponent<AIController>())
                {
                    hitobject.GetComponent<AIController>().ActualHealth -= 5;
                    Debug.Log("Nave enemiga toma daño");
                }
            }
        }
        if(Input.GetMouseButton(1) && Time.time > nextFireDouble)
        {
            nextFireDouble = Time.time + fireRateDouble;
            Instantiate(shot, shotSpawn_1.position, shotSpawn_1.rotation);
            Instantiate(shot, shotSpawn_2.position, shotSpawn_2.rotation);
            Ray ray = new Ray(shotSpawn_1.position, shotSpawn_1.right*10);
            Ray ray_2 = new Ray(shotSpawn_2.position, shotSpawn_2.right * 10);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitobject = hit.transform.gameObject;
                if (hitobject.GetComponent<AIController>())
                {
                    hitobject.GetComponent<AIController>().ActualHealth -= 5;
                    Debug.Log("Nave enemiga toma daño");
                }
            }
            if (Physics.SphereCast(ray_2, 0.75f, out hit))
            {
                GameObject hitobject = hit.transform.gameObject;
                if (hitobject.GetComponent<AIController>())
                {
                    hitobject.GetComponent<AIController>().ActualHealth -= 5;
                    Debug.Log("Nave enemiga toma daño");
                }
            }
        }

        if(health < 0)
        {
            isAlive = false;
            Destroy(this.gameObject);
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
