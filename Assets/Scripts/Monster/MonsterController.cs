using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : AIController
{
    [SerializeField] Transform shotSpawn_1;
    [SerializeField] Transform shotSpawn_2;

    protected override void Update()
    {
        //timer -= Time.deltaTime;
        if (!isActiveAndEnabled) return;

        targetDistance = Vector3.Distance(target.transform.position, transform.position);
        LookAtTarget();
        if (target != null)
        {
            if (attackDistance < targetDistance)
                Attack_Monster();
        }
        base.Update();
    }

    public void TakeDamage(int damage, Team damagingTeam)
    {
        actualHealth -= damage;
        if (actualHealth <= 0)
        {
            gameObject.SetActive(false);
            MonsterGameManager.Instance.ScoreUp(damagingTeam);
        }
        Invoke("Respawn", 5f);
    }

    private void Attack_Monster()
    {
        if (!canShoot) return;
        if (possibleTargets.Count > 0)
        {
            var bullet1 = Instantiate(bulletPrefab, shotSpawn_1.position, shotSpawn_1.rotation);
            bullet1.SetBullet(actualDamage, (possibleTargets[0].position - transform.position).normalized, Team.Monster);

            if (possibleTargets.Count > 1)
            {
                var bullet2 = Instantiate(bulletPrefab, shotSpawn_2.position, shotSpawn_2.rotation);
                bullet2.SetBullet(actualDamage, (possibleTargets[1].position - transform.position).normalized, Team.Monster);
            }

            canShoot = false;
            remainingCooldown = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!possibleTargets.Contains(collision.transform)) possibleTargets.Add(collision.transform);
        }
        if (collision.CompareTag("Allied"))
        {
            if (!possibleTargets.Contains(collision.transform)) possibleTargets.Add(collision.transform);
        }
        if (collision.CompareTag("Enemy"))
        {
            if (!possibleTargets.Contains(collision.transform)) possibleTargets.Add(collision.transform);
        }
        
        //Aca añade los que entran a su trigger a la lista de posibles targets
        //Los objetivos de ataque van a ser los 2 primeros en la lista
        //Reemplazar target por los primeros 2 de la lista (de haber 2, en ese caso uno solo)
        //(transform.position - target.transform.position).normalized
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            possibleTargets.Remove(collision.transform);
        }
        if (collision.CompareTag("Allied"))
        {
            possibleTargets.Remove(collision.transform);
        }
        if (collision.CompareTag("Enemy"))
        {
            possibleTargets.Remove(collision.transform);
        }
        //Aca quita a los que salen de su trigger de la lista de posibles targets
    }
}
