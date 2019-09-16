using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxHealth = 15;
    [SerializeField] int damage = 5;
    [SerializeField] float speed = 2;

    public Mover shot;
    public Transform shotSpawn;
    public Transform shotSpawn_1;
    public Transform shotSpawn_2;
    public float fireRateSingle;
    public float fireRateDouble;
    public bool isAlive = true;
    public int actualHealth;
    public float actualSpeed;

    private float nextFireSingle;
    private float nextFireDouble;
    private float accelerationForce;
    private int actualDamage;
    private Vector3 initialPosition;

    public int ActualHealth { get { return actualHealth; } set { actualHealth = value; } }
    public float ActualSpeed { get { return actualSpeed; } }


    private void Awake()
    {
        actualHealth = maxHealth;
        actualSpeed = speed;
        actualDamage = damage;
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!isActiveAndEnabled) return;

        accelerationForce = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
        var rotationForce = Input.GetAxis("Horizontal") * 3.0f;

        transform.eulerAngles += new Vector3(0, 0, -rotationForce);
        transform.Translate(-accelerationForce, 0, 0);

        if (Input.GetMouseButton(0) && Time.time > nextFireSingle)
        {
            nextFireSingle = Time.time + fireRateSingle;
            var bullet1 = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            bullet1.SetBullet(actualDamage, (shotSpawn.position - transform.position).normalized);
        }
        if(Input.GetMouseButton(1) && Time.time > nextFireDouble)
        {
            nextFireDouble = Time.time + fireRateDouble;
            var bullet1 = Instantiate(shot, shotSpawn_1.position, shotSpawn_1.rotation);
            var bullet2 = Instantiate(shot, shotSpawn_2.position, shotSpawn_2.rotation);
            bullet1.SetBullet(actualDamage, (shotSpawn_1.position - transform.position).normalized);
            bullet2.SetBullet(actualDamage, (shotSpawn_2.position - transform.position).normalized);
        }
    }

    public void TakeDamage(int damage)
    {
        actualHealth -= damage;

        if (actualHealth <= 0)
        {
            isAlive = false;
            gameObject.SetActive(false);
            Invoke("Respawn", 3f);
        }
    }

    public void Respawn()
    {
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.CompareTag("EnemyFlag"))
         {
             other.gameObject.SetActive(false);
             Debug.Log("You have the enemy flag");
         }
     }
}
