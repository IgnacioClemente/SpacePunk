using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlagController : AIController
{
    [SerializeField] FlagBehavior enemyFlag;
    [SerializeField] FlagBehavior allyFlag;
    [SerializeField] float pickUpDistance = 50;

    public bool isInChargeOfTakingFlag;

    private float flagDistance;
    private bool hasFlag;
   // private bool paused;
   //private bool resume;
   //private float timer = 3f;

    protected override void Update()
    {
        //timer -= Time.deltaTime;
        if (!isActiveAndEnabled || target == null) return;

            targetDistance = Vector3.Distance(target.transform.position, transform.position);
            LookAtTarget();
            if (isInChargeOfTakingFlag)
            {
                if (team == Team.Ally && player.HasFlag == true)
                {
                    isInChargeOfTakingFlag = false;
                    return;
                }
                if (!hasFlag)
                {
                    CaptureFlag();
                }
                else
                    ReturnFlag();
            }
            else
            {
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
            }
            base.Update();
    }

    public void CaptureFlag()
    {
        target = enemyFlag.transform;
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.transform == allyFlag.transform)
        {
            if (hasFlag && !allyFlag.Captured)
            {
                FlagGameManager.Instance.ScoreUp(this.team);
                hasFlag = false;
                enemyFlag.ResetFlag();
            }
            else
                allyFlag.ResetFlag();
        }

        if (other.transform == enemyFlag.transform)
        {
            enemyFlag.CaptureFlag();
            hasFlag = true;
        }
    }

    public override void TakeDamage(int damage)
    {
        actualHealth -= damage;
        healthBar.fillAmount = actualHealth / maxHealth;
        if (actualHealth <= 0)
        {
            gameObject.SetActive(false);
            if (isInChargeOfTakingFlag)
            {
                isInChargeOfTakingFlag = false;

                if (hasFlag)
                {
                    enemyFlag.DropFlag(transform.position);
                    hasFlag = false;
                }

                gameObject.SetActive(false);
                AIFlagManager.Instance.FlagCarrierDied(this);
            }
            Invoke("Respawn", 5f);
        }
    }

    public void ReturnFlag()
    {
        //si quiero capturar la bandera enemiga y la mia no esta disponible voy a pelear
        if (allyFlag == null || !allyFlag.gameObject.activeInHierarchy)
        {
            target = FindEnemyFlagCarrier();
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
        else
            target = allyFlag.transform;
    }

    public Transform FindEnemyFlagCarrier()
    {
        for (int i = 0; i < possibleTargets.Count; i++)
        {
            var auxAI = possibleTargets[i].GetComponent<AIFlagController>();

            if (auxAI != null)
            {
                if (auxAI.isInChargeOfTakingFlag)
                    return auxAI.transform;
            }
            else
            {
                
                var auxPlayer = possibleTargets[i].GetComponent<PlayerController>();
                if (auxPlayer != null && auxPlayer.HasFlag)
                    return auxPlayer.transform;
            }
        }
        return null;
    }

    /*public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        resume = true;
    }*/
}
