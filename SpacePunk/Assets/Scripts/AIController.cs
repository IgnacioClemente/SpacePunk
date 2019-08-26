using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] Transform shotSpawn;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] protected float attackDistance = 18;
    [SerializeField] protected float chaseDistance = 18;
    [SerializeField] protected float lookDistance = 18;
    [SerializeField] int maxHealth = 15;
    [SerializeField] float speed;
    [SerializeField] float fireRate;
    [SerializeField] int damage;

    protected Transform target;

    protected GameObject player;
    protected Rigidbody rb;
    protected float playerDistance;
    protected int actualHealth;
    protected float actualSpeed;
    protected int actualDamage;
    protected AIManager enemy;
    protected GameObject[] tagPlayer;

    public int ActualHealth { get { return actualHealth; } set { actualHealth = value; } }
    public float ActualSpeed { get { return actualSpeed; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        actualHealth = maxHealth;
        actualSpeed = speed;
        actualDamage = damage;
    }

    //Cosas que va a hacer esta clase padre:
    /* Disparar
     * Perseguir
     * Morir
     * Moverse
     * Agarrar PowerUps
     * */

    void Start()
    {
        tagPlayer = GameObject.FindGameObjectsWithTag("Player");
        enemy = GetComponentInParent<AIManager>();

        for(int i = 0; i < tagPlayer.Length;i++)
        {
            tagPlayer[i].SetActive(true);
            player = tagPlayer[i];
            target = player.transform;
        }
    }

    protected virtual void Update()
    {

        transform.Translate((target.position - transform.position).normalized * actualSpeed * Time.deltaTime);
        if (actualHealth < 0)
        {
            enemy.TheGuyWithTheFlagDied(this.player);
            Destroy(this.gameObject);
        }
    }

   public void LookAtPlayer()
    {
        float angle = Vector3.SignedAngle(transform.right, (player.transform.position - transform.position), transform.forward);

        if (angle > 0.1f || angle < -0.1f)
        {
            Quaternion qAux = Quaternion.Euler(0, 0, angle);
            rb.rotation *= qAux;
        }
    }

   public void ChasePlayer()
    {
        rb.MovePosition(transform.position + (player.transform.position - transform.position).normalized * speed * Time.deltaTime);
    }

   public void Attack()
    {
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitobject = hit.transform.gameObject;
            if (hitobject.GetComponent<PlayerController>())
            {
                if (bulletPrefab != null)
                {
                    GameObject bulletAux = Instantiate(bulletPrefab) as GameObject;
                    bulletAux.transform.position = shotSpawn.position;
                    hitobject.GetComponent<PlayerController>().health -= 5;
                }
            }
        }
    }

    
}