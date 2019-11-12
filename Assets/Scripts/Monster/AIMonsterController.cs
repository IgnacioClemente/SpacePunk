using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonsterController : AIController
{
    private MonsterController monster;
    public bool isInChargeOfShootingMonster;
    private bool stay;
    private float minMonsterDistance = 18;
    private float shootDistance;
    protected override void Update()
    {
        shootDistance = Vector2.Distance(monster.transform.position, transform.position);
        //timer -= Time.deltaTime;
        if (!isActiveAndEnabled) return;

        targetDistance = Vector3.Distance(target.transform.position, transform.position);
        LookAtTarget();

        if(isInChargeOfShootingMonster)
        {
            SetMonsterAsTarget();
        }
        else if (target != null)
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
   
    public void SetMonster(MonsterController monster)
    {
        this.monster = monster;
    }

    public void SetMonsterAsTarget()
    {
        target = monster.transform;
        if (shootDistance <= minMonsterDistance)
        {
            actualSpeed = 0;
            Attack();
        }
            
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

