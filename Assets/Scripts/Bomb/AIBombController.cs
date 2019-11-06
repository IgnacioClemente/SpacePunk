using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBombController : AIController
{
    [SerializeField] BaseBehaviour enemyBase;
    [SerializeField] BaseBehaviour allyBase;
    [SerializeField] float minBombDistance = 15;
    [SerializeField] float timeToPlant = 3f;

    public bool isInChargeOfPlantingBomb;
    public bool isInChargeOfDefusingBomb;

    private PlayerControllerBomb player_bomb;

    private bool isAlliedBombPlanted;
    private bool isEnemyBombPlanted;
    private bool canPlantBomb;
    private bool canDefuseBomb;
    private bool isPlanting;

    private float plantingTimer;
    // private bool paused;
    //private bool resume;
    //private float timer = 3f;

    protected override void Awake()
    {
        player_bomb = player.GetComponent<PlayerControllerBomb>();
        base.Awake();
    }

    protected override void Update()
    {
        //timer -= Time.deltaTime;
        if (!isActiveAndEnabled) return;

        if (isPlanting)
        {
            plantingTimer += Time.deltaTime;
            if(plantingTimer > timeToPlant)
                PlantBomb();
        }
        else
        {
            targetDistance = Vector3.Distance(target.transform.position, transform.position);
            LookAtTarget();

            var distancePlant = Vector2.Distance(enemyBase.transform.position, transform.position);
            var distanceDefuse = Vector2.Distance(enemyBase.transform.position, transform.position);

            if (isInChargeOfPlantingBomb)
            {
                if (team == Team.Ally && player_bomb.PlantingBomb == true)
                {
                    isInChargeOfPlantingBomb = false;
                    return;
                }//Seteo la base enemiga como target
                else if (!isAlliedBombPlanted)
                {
                    GoToPlantBomb();
                }
                //Si estoy cerca de la base enemiga, planto
                if(targetDistance <= minBombDistance)
                {
                    StartPlantingBomb();
                }
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
            if (isAlliedBombPlanted)
                if (isInChargeOfDefusingBomb)
                {
                    if (team == Team.Ally && player_bomb.DefusingBomb == true)
                    {
                        isInChargeOfDefusingBomb = false;
                        return;
                    }
                    else if (isAlliedBombPlanted)
                    {
                        DefuseBomb();
                    }
                }
            base.Update();
        }
    }

    public void GoToPlantBomb()
    {
        target = enemyBase.transform;
    }

    public void StartPlantingBomb()
    {
        isPlanting = true;
        plantingTimer = 0;
    }

    public void PlantBomb()
    {
        enemyBase.PlantBomb();
        isPlanting = false;
        plantingTimer = 0;
        isAlliedBombPlanted = true;
        isInChargeOfPlantingBomb = false;
        CheckAvailableTargets();
    }

    public void DefuseBomb()
    {
        target = allyBase.transform;
        if(canDefuseBomb)
        {
            wait = true;
            allyBase.DefuseBomb();
            wait = false;
        }
    }

    public override void TakeDamage(int damage)
    {
        actualHealth -= damage;
        if (actualHealth <= 0)
        {
            gameObject.SetActive(false);
            if (isInChargeOfPlantingBomb)
            {
                isInChargeOfPlantingBomb = false;

                gameObject.SetActive(false);
                AIBombManager.Instance.BombCarrierDied(this);
            }
            if (isInChargeOfDefusingBomb)
            {
                isInChargeOfDefusingBomb = false;

                gameObject.SetActive(false);
                AIBombManager.Instance.BombCarrierDied(this);
                Invoke("Respawn", 5f);
            }
        }
    }
}
