using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;

    private int damage;
    private Vector3 dir;
    private Team myTeam;

   private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = dir * speed;
    }
    
    public void SetBullet(int damage, Vector3 dir, Team team)
    {
        this.damage = damage;
        this.dir = dir;
        this.myTeam = team;

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if(player != null)
                player.TakeDamage(damage);
        }
        else
        {
            var aiToDamage = other.GetComponent<AIController>();
            if(aiToDamage != null)
                aiToDamage.TakeDamage(damage);
        }

        if(other.CompareTag("Monster"))
        {
            Debug.Log(other.name);
            var monster = other.GetComponent<MonsterController>();
            if (monster != null)
                monster.TakeDamage(damage, myTeam);
        }

        Destroy(gameObject);
    }
}
