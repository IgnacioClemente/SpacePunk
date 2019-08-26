using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlagController : AIController
{
    [SerializeField] Transform banderaEnemiga;
    [SerializeField] Transform banderaAliada;
    [SerializeField] float pickUpDistance = 50;

    private float banderaDistance;
    private bool hasFlag;
    private bool isGoingToFlag;
    private bool flagIsSecured;
    private bool isInChargeOfTakingFlag;

    //Cosas que va a hacer esta clase hija:
    /*Agarrar bandera
     * Entregar bandera
     * Recuperar bandera caida
     * */

    protected override void Update()
    {/*
        if (isInChargeOfTakingFlag)
        {
            if (hasFlag == false)
            {
                CapturarBandera();
            }
            else
                ReturnFlag();
        }
        else
        {
            if (player != null && player.activeSelf)
            {
                if ((player.transform.position - this.transform.position).sqrMagnitude < playerDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, actualSpeed);
                }
                playerDistance = Vector3.Distance(player.transform.position, transform.position);

                if (playerDistance < chaseDistance)
                {
                    if (playerDistance > attackDistance)
                    {
                        ChasePlayer();
                    }
                    else
                    {
                        Attack();
                    }
                }
                if (playerDistance < lookDistance)
                {
                    LookAtPlayer();
                }
            }
        }*/
        base.Update();
    }

    public void CapturarBandera()
    {
        if (flagIsSecured == false)
        {
            isGoingToFlag = true;
            rb.MovePosition(transform.position + (banderaEnemiga.position - transform.position).normalized * actualSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerFlag"))
        {
            other.gameObject.SetActive(false);
            hasFlag = true;
            isGoingToFlag = false;
            flagIsSecured = false;
            ReturnFlag();
        }

        if (other.gameObject.CompareTag("EnemyFlag"))
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
        }
    }

    void ReturnFlag()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(90, 0, 180);

        if (transform.rotation == Quaternion.Euler(90, 0, 0))
        {
            transform.rotation = Quaternion.Euler(rot);
        }
        // rb.MovePosition(transform.position + (banderaAliada.position - transform.position).normalized * speed * Time.deltaTime);
        transform.Translate(Vector3.right * actualSpeed * Time.deltaTime);
    }
}
