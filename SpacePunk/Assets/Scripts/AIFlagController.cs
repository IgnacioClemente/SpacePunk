using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlagController : AIController
{
    [SerializeField] Transform enemyFlag;
    [SerializeField] Transform allyFlag;
    [SerializeField] float pickUpDistance = 50;

    public bool isInChargeOfTakingFlag;
    public bool flagIsDropped;

    private float flagDistance;
    private bool hasFlag;
    private bool isGoingToFlag;
    private bool flagIsSecured;
    private Vector3 enemyFlagPosition;
    private Vector3 alliedFlagPosition;
    private float timer = 4f;

    protected override void Start()
    {
        enemyFlagPosition = enemyFlag.position;
        alliedFlagPosition = allyFlag.position;
        base.Start();
    }

    protected override void Update()
    {
        if (!isActiveAndEnabled) return;

        targetDistance = Vector3.Distance(target.transform.position, transform.position);
        LookAtTarget();
        if (isInChargeOfTakingFlag)
        {
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
        isGoingToFlag = true;
        target = enemyFlag;
    }

   private void OnTriggerEnter(Collider other)
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
           FlagGameManager.Instance.ResetFlagAfterCapture(enemyFlagPosition, alliedFlagPosition, enemyFlag, allyFlag);
        }

        if (other.transform == enemyFlag)
        {
            other.gameObject.SetActive(false);
            hasFlag = true;
            flagIsDropped = false;
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
            if (isInChargeOfTakingFlag)
            {
                isInChargeOfTakingFlag = false;
                flagIsDropped = true;
                AIManager.Instance.FlagCarrierDied(this);
                if (hasFlag)
                {
                    enemyFlagPosition = transform.position;
                    enemyFlag.transform.position = enemyFlagPosition;
                    enemyFlag.gameObject.SetActive(true);
                    hasFlag = false;
                }
            }

            gameObject.SetActive(false);
            Invoke("Respawn", 5f);
        }
    }

    public void ReturnFlag()
    {
        //si quiero capturar la bandera enemiga y la mia no esta disponible voy a pelear
        if (allyFlag == null || !allyFlag.gameObject.activeInHierarchy)
        {
            target = FindEnemyFlagCarrier();
        }
        else
            target = allyFlag;
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

    public void ResetFlag()
    {
        if(flagIsDropped)
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                FlagGameManager.Instance.ResetFlagAfterTimer(enemyFlagPosition, enemyFlag);
            }
            else
            {
                FlagGameManager.Instance.ResetFlagAfterTimer(alliedFlagPosition, allyFlag);
            }
        }
    }
