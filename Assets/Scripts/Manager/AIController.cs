using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{
    [SerializeField] protected Team team;
    [SerializeField] protected PlayerController player;
    [SerializeField] Transform shotSpawn;
    [SerializeField] protected Mover bulletPrefab;
    [SerializeField] protected float attackDistance = 18;
    [SerializeField] protected float chaseDistance = 18;
    [SerializeField] protected float lookDistance = 18;
    [SerializeField] float attackSpeed;
    [SerializeField] protected float maxHealth = 15;
    [SerializeField] float speed = 2;
    [SerializeField] int damage = 5;
    [Header("Canvas Settings")]
    [SerializeField] protected Image healthBar;

    protected Transform target;
    protected Vector3 initialPosition;
    protected List<Transform> possibleTargets;
    
    protected float targetDistance;
    protected float actualHealth;
    protected float actualSpeed;
    protected int actualDamage;
    protected GameObject[] tagPlayer;

    protected float remainingCooldown;
    protected bool canShoot = true;
    protected bool wait;

    public float ActualHealth { get { return actualHealth; } set { actualHealth = value; } }
    public float ActualSpeed { get { return actualSpeed; } }
    public Team Team { get { return team; } }

    protected virtual void Awake()
    {
        actualHealth = maxHealth;
        actualSpeed = speed;
        actualDamage = damage;
        remainingCooldown = attackSpeed;
    }

    protected virtual void Start()
    {
        initialPosition = transform.position;
        healthBar.fillAmount = actualHealth / maxHealth;
    }

    protected virtual void Update()
    {
        if(wait)
        {
            CheckAvailableTargets();
            return; 
        }

        if (target != null && target.gameObject.activeSelf)
            transform.position += (target.position - transform.position).normalized * actualSpeed * Time.deltaTime;
        else
            CheckAvailableTargets();

        if (canShoot) return;

        remainingCooldown += Time.deltaTime;

        if (remainingCooldown >= attackSpeed)
            canShoot = true;
    }

    public virtual void SetShip(List<Transform> targets)
    {
        possibleTargets = new List<Transform>();
        for (int i = 0; i < targets.Count; i++)
        {
            possibleTargets.Add(targets[i]);
        }

        if (team == Team.Enemy)
        {
            possibleTargets.Add(player.transform);
        }

        CheckAvailableTargets();
    }

    public void CheckAvailableTargets()
    {
        bool targetAvailable = false;
        for (int i = 0; i < possibleTargets.Count; i++)
        {
            if (possibleTargets[i].gameObject.activeSelf)
            {
                targetAvailable = true;
                wait = false;
                break;
            }
        }

        if (targetAvailable)
            ChooseTarget();
        else
            wait = true;    
    }

    public void ChooseTarget()
    {
        target = possibleTargets[Random.Range(0, possibleTargets.Count)];
        if (target == null || !target.gameObject.activeInHierarchy)
            ChooseTarget();
    }

    public virtual void TakeDamage(int damage)
    {
        actualHealth -= damage;
        healthBar.fillAmount = actualHealth / maxHealth;
        if (actualHealth <= 0)
        {
            gameObject.SetActive(false);
            Invoke("Respawn", 5f);
        }
    }

    public void Respawn()
    {
        transform.position = initialPosition;
        actualHealth = maxHealth;
        healthBar.fillAmount = actualHealth / maxHealth;
        gameObject.SetActive(true);
    }

    public void LookAtTarget()
    {
        transform.right = (target.position - transform.position).normalized;
    }

    public void ChaseTarget()
    {
        //rb.MovePosition(transform.position + (target.transform.position - transform.position).normalized * speed * Time.deltaTime);
    }

    public void Attack()
    {
        if (!canShoot) return;
        var bullet1 = Instantiate(bulletPrefab, shotSpawn.position, shotSpawn.rotation);
        bullet1.SetBullet(actualDamage, (target.position - transform.position).normalized,team);
        canShoot = false;
        remainingCooldown = 0;
    }
}

public enum Team { Ally, Enemy, Monster}