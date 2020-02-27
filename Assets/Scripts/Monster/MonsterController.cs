using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : Unit
{
    [Header("Canvas Settings")]
    [SerializeField] Image healthBar;
    [SerializeField] Text healthText;
    [Header("Monster Settings")]
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float speed = 2;
    [SerializeField] float attackCooldown = 3f;
    [SerializeField] int damage = 5;
    [SerializeField] Transform shotSpawn_1;
    [SerializeField] Transform shotSpawn_2;

    float targetDistance;
    float actualHealth;
    bool isAlive = true;
    bool canShoot = true;
    int direction = 1;
    private bool paused;

    List<Transform> possibleTargets = new List<Transform>();
    public bool IsAlive { get { return isAlive; } }

    protected virtual void Awake()
    {
        actualHealth = maxHealth;
    }
    
    void Start()
    {
        healthText.text = actualHealth + "/" + maxHealth;
        healthBar.fillAmount = actualHealth / maxHealth;
    }

    void Update()
    {
        if (paused) return;
        transform.position += Vector3.up * speed * direction * Time.deltaTime;

        if (Mathf.Abs(transform.position.y) >= 12)
            direction *= -1;
        //timer -= Time.deltaTime;
        if (!isActiveAndEnabled) return;

        if (possibleTargets.Count > 0)
            Attack_Monster();
    }

    public void TakeDamage(int damage, Team damagingTeam)
    {
        actualHealth -= damage;
        healthBar.fillAmount = actualHealth / maxHealth;
        healthText.text = actualHealth + "/" + maxHealth;
        if (actualHealth <= 0)
        {
            isAlive = false;
            actualHealth = 0;
            gameObject.SetActive(false);
            MonsterGameManager.Instance.ScoreUp(damagingTeam);
            MonsterGameManager.Instance.Win(damagingTeam);
        }
    }

    private void Attack_Monster()
    {
        if (!canShoot) return;
        if (possibleTargets.Count > 0)
        {
            var bullet1 = Instantiate(bulletPrefab, shotSpawn_1.position, shotSpawn_1.rotation);
            bullet1.SetBullet(damage, (possibleTargets[0].position - transform.position).normalized, Team.Monster);

            if (possibleTargets.Count > 1)
            {
                var bullet2 = Instantiate(bulletPrefab, shotSpawn_2.position, shotSpawn_2.rotation);
                bullet2.SetBullet(damage, (possibleTargets[1].position - transform.position).normalized, Team.Monster);
            }
            else
            {
                var bullet2 = Instantiate(bulletPrefab, shotSpawn_2.position, shotSpawn_2.rotation);
                bullet2.SetBullet(damage, (possibleTargets[0].position - transform.position).normalized, Team.Monster);
            }

            canShoot = false;
            Invoke("AllowAttack", attackCooldown);
        }
    }

    public void AllowAttack()
    {
        canShoot = true;
    }

    public void AddTarget(Transform target)
    {
        if (!possibleTargets.Contains(target))
            possibleTargets.Add(target);
    }

    public void RemoveTarget(Transform target)
    {
        if (possibleTargets.Contains(target))
            possibleTargets.Remove(target);
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
