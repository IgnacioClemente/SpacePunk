using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;

    private int damage;
    private Vector3 dir;

   private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = dir * speed;
    }

    public void SetBullet(int damage, Vector3 dir)
    {
        this.damage = damage;
        this.dir = dir;
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
        //PoolManager.GetInstance().TurnOffByName("PlayerBullet",this);
    }
}
