using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedController: MonoBehaviour
{
    public Rigidbody rb;
    public GameObject nave;
    public float speed;
    public Transform shotSpawn;
    public float fireRate;
    public GameObject bulletPrefab;
    public float enemyDistance;
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
    public float health = 15;
    AlliedManager allied;

    public bool isInChargeOfTakingFlag = false;

    void Start()
    {
        allied = GetComponentInParent<AlliedManager>();
    }
    void Update()
    {
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
            if (nave != null && nave.activeSelf)
            {
                if ((nave.transform.position - this.transform.position).sqrMagnitude < enemyDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, nave.transform.position, speed);
                }
                enemyDistance = Vector3.Distance(nave.transform.position, transform.position);

                if (enemyDistance < chaseDistance)
                {
                    if (enemyDistance > attackDistance)
                    {
                        ChaseEnemy();
                    }
                    else
                    {
                        Attack();
                    }
                }
                if (enemyDistance < lookDistance)
                {
                    LookAtEnemy();
                }
            }
        }
        if (health < 0)
        {
            allied.TheGuyWithTheFlagDied(this.nave);
            Destroy(this.gameObject);
        }
    }

    public void LookAtEnemy()
    {
        float angle = Vector3.SignedAngle(transform.right, (nave.transform.position - transform.position), transform.forward);

        if (angle > 0.1f || angle < -0.1f)
        {
            Quaternion qAux = Quaternion.Euler(0, 0, angle);
            rb.rotation *= qAux;
        }
    }

    public void ChaseEnemy()
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
            if (hitobject.GetComponent<EnemyController>())
            {
                if (bulletPrefab != null)
                {
                    GameObject bulletAux = Instantiate(bulletPrefab) as GameObject;
                    bulletAux.transform.position = shotSpawn.position;
                    hitobject.GetComponent<EnemyController>().health -= 5;
                }
            }
        }
    }

    public void CapturarBandera()
    {
        if (flagIsSecured == false)
        {
            isGoingToFlag = true;
            rb.MovePosition(transform.position + (banderaEnemiga.position - transform.position).normalized * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyFlag"))
        {
            other.gameObject.SetActive(false);
            hasFlag = true;
            isGoingToFlag = false;
            flagIsSecured = false;
            ReturnFlag();
        }

        if (other.gameObject.CompareTag("PlayerFlag"))
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
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
