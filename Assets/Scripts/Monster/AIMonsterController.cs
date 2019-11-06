using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonsterController : AIController
{
    protected override void Update()
    {
        //timer -= Time.deltaTime;
        if (!isActiveAndEnabled) return;

        targetDistance = Vector3.Distance(target.transform.position, transform.position);
        LookAtTarget();
        if (target != null)
        {
            if (targetDistance < chaseDistance)
            {
                if (targetDistance > attackDistance)
                {
                    ChaseTarget();
                }
                else
                {
                    Attack();
                }
            }
        }
        base.Update();
    }

    public override void TakeDamage(int damage)
    {
        actualHealth -= damage;
        if (actualHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        Invoke("Respawn", 5f);
    }
   
        // private bool paused;
        //private bool resume;
        //private float timer = 3f;
}

    /*public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        resume = true;
    }*/

