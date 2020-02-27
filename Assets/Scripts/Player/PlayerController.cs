using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Unit
{
    [SerializeField] float maxHealth = 15;
    [SerializeField] int damage = 5;
    [SerializeField] float speed = 2;

    public Image HealthBar;
    public float HealthBarYOffset = 2;
    public Bullet shot;
    public Transform shotSpawn;
    public Transform shotSpawn_1;
    public Transform shotSpawn_2;
    public float fireRateSingle;
    public float fireRateDouble;
    public bool isAlive = true;
    public float actualHealth;
    public float actualSpeed;

    protected bool hasFlag;

    private float nextFireSingle;
    private float nextFireDouble;
    private float accelerationForce;
    private int actualDamage;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool paused;
    //private bool paused;
    //private bool resume;

    public float ActualHealth { get { return actualHealth; } set { actualHealth = value; } }
    public float ActualSpeed { get { return actualSpeed; } }
    public bool HasFlag { get { return hasFlag; } }


    private void Awake()
    {
        actualHealth = maxHealth;
        actualSpeed = speed;
        actualDamage = damage;
    }

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        UpdateHealthBar();
    }

   protected virtual void Update()
   {
        if (!isActiveAndEnabled || paused) return;

        accelerationForce = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
        var rotationForce = Input.GetAxis("Horizontal") * 3.0f;

        transform.eulerAngles += new Vector3(0, 0, -rotationForce);
        transform.Translate(-accelerationForce, 0, 0);

        if (Input.GetMouseButton(0) && Time.time > nextFireSingle)
        {
            nextFireSingle = Time.time + fireRateSingle;
            Bullet bullet = Instantiate(shot, shotSpawn.transform.position, shot.transform.rotation);
            bullet.SetBullet(actualDamage, -transform.right,Team.Ally);
        }
        if (Input.GetMouseButton(1) && Time.time > nextFireDouble)
        {
            nextFireDouble = Time.time + fireRateDouble;
            Bullet bullet1 = Instantiate(shot, shotSpawn_1.transform.position, shot.transform.rotation);
            Bullet bullet2 = Instantiate(shot, shotSpawn_2.transform.position, shot.transform.rotation);

            bullet1.SetBullet(actualDamage, -transform.right, Team.Ally);
            bullet2.SetBullet(actualDamage, -transform.right, Team.Ally);
        }
    }

    public void TakeDamage(int damage)
    {
        actualHealth -= damage;
        actualHealth = Mathf.Clamp(actualHealth, 0, maxHealth);

        UpdateHealthBar();

        if (actualHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        isAlive = false;
        gameObject.SetActive(false);
        Invoke("Respawn", 5f);
    }

    private void UpdateHealthBar()
    {
        HealthBar.fillAmount = actualHealth / maxHealth;
    }

    public void RestoreHealth()
    {
        actualHealth = maxHealth;
        UpdateHealthBar();
    }

    public void Respawn()
    {
        RestoreHealth();
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        gameObject.SetActive(true);
    }

    public void BuffAttack(int amount, float duration)
    {
        actualDamage += amount;
        Invoke("ResetAttack", duration);
    }

    public void ResetAttack()
    {
        actualDamage = damage;
    }

    public void Heal(int amount)
    {
        actualHealth += amount;

        if (actualHealth >= maxHealth)
            actualHealth = maxHealth;

        UpdateHealthBar();
    }

    public override void Pause()
    {
        paused = true;
    }

    public override void Resume()
    {
        paused = false;
    }
}
