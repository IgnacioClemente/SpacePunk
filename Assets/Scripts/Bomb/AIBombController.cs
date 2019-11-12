using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIBombController : AIController
{
    [SerializeField] BaseBehaviour enemyBase;
    [SerializeField] BaseBehaviour allyBase;
    [SerializeField] float minBombDistance = 15;
    [SerializeField] float timeToPlant = 3f;
    [SerializeField] float timeToDefuse = 3f;
    [SerializeField] Text plantText;
    [SerializeField] Text defuseText;

    public bool isInChargeOfPlantingBomb;
    public bool isInChargeOfDefusingBomb;

    private PlayerControllerBomb player_bomb;

    private bool isDefusing;
    private bool isPlanting;

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
        plantText.text = ((int)timeToPlant).ToString();
        defuseText.text = ((int)timeToDefuse).ToString();
        //timer -= Time.deltaTime;
        if (!isActiveAndEnabled || target == null) return;

            targetDistance = Vector3.Distance(target.transform.position, transform.position);
            LookAtTarget();

            var distancePlant = Vector2.Distance(enemyBase.transform.position, transform.position);
            var distanceDefuse = Vector2.Distance(allyBase.transform.position, transform.position);

        if (isInChargeOfPlantingBomb)
        {
            if (team == Team.Ally && player_bomb.PlantingBomb == true)
            {
                isInChargeOfPlantingBomb = false;
                return;
            }//Seteo la base enemiga como target
            if (!enemyBase.Planted && enemyBase.actualHealth > 0)
            {
                GoToPlantBomb();
            }
            //Si estoy cerca de la base enemiga, planto
            if (distancePlant <= minBombDistance && !enemyBase.Planted)
            {
                PlantBomb();
            }
            if (isPlanting == true)
            {
                wait = true;
                plantText.gameObject.SetActive(true);
                timeToPlant -= Time.deltaTime;
                if (timeToPlant <= 0)
                {
                    enemyBase.PlantBomb(team);
                    plantText.gameObject.SetActive(false);
                    wait = false;
                    isInChargeOfPlantingBomb = false;
                    isPlanting = false;
                    timeToPlant = 3f;
                }
                target = FindBombPlanter();
                AIBombManager.Instance.BombCarrierDied(this);
            }
        }
        else if (isInChargeOfDefusingBomb)
        {
            if (team == Team.Ally && player_bomb.DefusingBomb == true)
            {
                isInChargeOfDefusingBomb = false;
                return;
            }
            if (allyBase.Planted)
            {
                GoDefuseBomb();
            }
            if (distanceDefuse <= minBombDistance && allyBase.Planted) 
            {
                DefuseBomb();
            }
            if (isDefusing == true)
            {
                wait = true;
                defuseText.gameObject.SetActive(true);
                timeToDefuse -= Time.deltaTime;
                if (timeToDefuse <= 0)
                {
                    allyBase.DefuseBomb(team);
                    defuseText.gameObject.SetActive(false);
                    wait = false;
                    isInChargeOfDefusingBomb = false;
                    isDefusing = false;
                    timeToDefuse = 3f;
                }
                target = FindDefuser();
                AIBombManager.Instance.DefuseCarrierDied(this);
            }
        }
        else
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

    public void GoToPlantBomb()
    {
        target = enemyBase.transform;
    }

    public void PlantBomb()
    {
        isPlanting = true;
    }

    public void GoDefuseBomb()
    {
        target = allyBase.transform;
    }

    public void DefuseBomb()
    {
        isDefusing = true;    
    }

    public override void TakeDamage(int damage)
    {
        actualHealth -= damage;
        healthBar.fillAmount = actualHealth / maxHealth;
        if (actualHealth <= 0)
        {
            gameObject.SetActive(false);
            if (isPlanting == true)
            {
                isPlanting = false;
                plantText.gameObject.SetActive(false);
            }
            if (isDefusing == true)
            {
                isDefusing = false;
                defuseText.gameObject.SetActive(false);
            }
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
                AIBombManager.Instance.DefuseCarrierDied(this);
            }
            Invoke("Respawn", 5f);
        }
    }

    public Transform FindBombPlanter()
    {
        for (int i = 0; i < possibleTargets.Count; i++)
        {
            var auxAI = possibleTargets[i].GetComponent<AIBombController>();

            if (auxAI != null)
            {
                if (auxAI.isInChargeOfPlantingBomb)
                    return auxAI.transform;
            }
            else
            {

                var auxPlayer = possibleTargets[i].GetComponent<PlayerControllerBomb>();
                if (auxPlayer != null && auxPlayer.PlantingBomb)
                    return auxPlayer.transform;
            }
        }
        return null;
    }

    public Transform FindDefuser()
    {
        for (int i = 0; i < possibleTargets.Count; i++)
        {
            var auxAI = possibleTargets[i].GetComponent<AIBombController>();

            if (auxAI != null)
            {
                if (auxAI.isInChargeOfDefusingBomb)
                    return auxAI.transform;
            }
            else
            {

                var auxPlayer = possibleTargets[i].GetComponent<PlayerControllerBomb>();
                if (auxPlayer != null && auxPlayer.DefusingBomb)
                    return auxPlayer.transform;
            }
        }
        return null;
    }
}
