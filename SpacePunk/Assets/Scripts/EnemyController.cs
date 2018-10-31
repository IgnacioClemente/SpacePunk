using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject nave;
    public float speed;
    public Transform shotSpawn;
    public float fireRate;
    public GameObject bulletPrefab;
    public float playerDistance;
    public float attackDistance = 18;
    public float chaseDistance = 18;
    public float lookDistance = 18;
    public Transform banderaEnemiga;
    public Transform banderaAliada;
    public float banderaDistance;
    public float pickUpDistance = 50;
    public bool hasFlag = false;
    public bool isGoingToFlag = false;
    public bool flagIsSecured = false;
    private GameObject[] tagPlayer;

    public bool isInChargeOfTakingFlag = false;

    void Start()
    {
        tagPlayer = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < tagPlayer.Length;i++)
        {
            tagPlayer[i].SetActive(true);
            nave = tagPlayer[i];
        }
    }
    void Update()
    {
        if (isInChargeOfTakingFlag)
        {
            if (hasFlag == false)
            {
                Bandera();
            }
            else
                ReturnFlag();
        }
        else
        {
            if (nave != null && nave.activeSelf)
            {
                if ((nave.transform.position - this.transform.position).sqrMagnitude < playerDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, nave.transform.position, speed);
                }
                playerDistance = Vector3.Distance(nave.transform.position, transform.position);

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
        }
    }

   public void LookAtPlayer()
    {
        float angle = Vector3.SignedAngle(transform.right, (nave.transform.position - transform.position), transform.forward);

        if (angle > 0.1f || angle < -0.1f)
        {
            Quaternion qAux = Quaternion.Euler(0, 0, angle);
            rb.rotation *= qAux;
        }
    }

   public void ChasePlayer()
    {
        rb.MovePosition(transform.position + (nave.transform.position - transform.position).normalized * speed * Time.deltaTime);
    }

   public void Attack()
    {
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitobject = hit.transform.gameObject;
            if (hitobject.GetComponent<PlayerController>())
            {
                if (bulletPrefab != null)
                {
                    GameObject bulletAux = Instantiate(bulletPrefab) as GameObject;
                    bulletAux.transform.position = shotSpawn.position;
                    hitobject.GetComponent<PlayerController>().health -= 5;
                }
            }
        }
    }

    public void Bandera()
    {
        if(flagIsSecured == false)
        {
            isGoingToFlag = true;
            rb.MovePosition(transform.position + (banderaEnemiga.position - transform.position).normalized * speed * Time.deltaTime);
        }
        else
        {
           // Attack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerFlag"))
        {
            other.gameObject.SetActive(false);
            hasFlag = true;
            Debug.Log("The Enemy has your flag");
        //    isGoingToFlag = false;
          //  flagIsSecured = true;
            ReturnFlag();
        }
        if(other.gameObject.CompareTag("EnemyFlag"))
        {
            hasFlag = false;
            Debug.Log("The Enemy has capture your flag");
         //   isGoingToFlag = true; 
         //   flagIsSecured = false;
        }
    }
    void ReturnFlag()
    {
        rb.MovePosition(transform.position + (banderaAliada.position - transform.position).normalized * speed * Time.deltaTime);
    }
}