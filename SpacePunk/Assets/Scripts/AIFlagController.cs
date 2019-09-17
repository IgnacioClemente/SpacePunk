using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlagController : AIController
{
    [SerializeField] Transform enemyFlag;
    [SerializeField] Transform allyFlag;
    [SerializeField] float pickUpDistance = 50;

    public bool isInChargeOfTakingFlag;

    private float flagDistance;
    private bool hasFlag;
    private bool isGoingToFlag;
    private bool flagIsSecured;
    private Vector3 enemyFlagPosition;
    private Vector3 alliedFlagPosition;

  /* protected override void Start()
    {
        enemyFlagPosition = enemyFlag.transform.position;
        alliedFlagPosition = allyFlag.transform.position;
    }*/

    protected override void Update()
    {
        if (!isActiveAndEnabled) return;

        targetDistance = Vector3.Distance(target.transform.position, transform.position);
        LookAtTarget();
        if (isInChargeOfTakingFlag)
        {
            if (hasFlag == false)
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
        if (flagIsSecured == false)
        {
            isGoingToFlag = true;
            target = enemyFlag;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == allyFlag)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(90, 0, 0);
            hasFlag = false;

            if (transform.rotation == Quaternion.Euler(90, 0, 180))
            {
                transform.rotation = Quaternion.Euler(rot);
            }
            isGoingToFlag = false;
            flagIsSecured = true;
           //GameManager.Instance.ResetFlag(enemyFlagPosition, alliedFlagPosition, enemyFlag, allyFlag);
        }

        if (other.transform == enemyFlag)
        {
            other.gameObject.SetActive(false);
            hasFlag = true;
            isGoingToFlag = false;
            flagIsSecured = false;
            ReturnFlag();
        }
    }

    public override void TakeDamage(int damage)
    {
        actualHealth -= damage;
        if (actualHealth <= 0)
        {
            if(isInChargeOfTakingFlag)
                AIManager.Instance.FlagCarrierDied(this);

            gameObject.SetActive(false);
            Invoke("Respawn", 5f);
        }
    }

    void ReturnFlag()
    {
       //if (allyFlag == null || !allyFlag.gameObject.activeInHierarchy) return;
        target = allyFlag;
    }
}
