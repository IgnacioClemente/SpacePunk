using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBombManager : MonoBehaviour
{
    List<AIBombController> enemyShips;
    List<AIBombController> alliedShips;

    public static AIBombManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        Instance = this;

        enemyShips = new List<AIBombController>();
        alliedShips = new List<AIBombController>();

        foreach (Transform child in transform)
        {
            var ship = child.GetComponent<AIBombController>();
            if (ship.Team == Team.Enemy)
                enemyShips.Add(ship);
            else
                alliedShips.Add(ship);
        }

        var tempAlliedShips = new List<Transform>();
        var tempEnemyShips = new List<Transform>();

        for (int i = 0; i < alliedShips.Count; i++)
        {
            tempAlliedShips.Add(alliedShips[i].transform);
        }

        for (int i = 0; i < enemyShips.Count; i++)
        {
            tempEnemyShips.Add(enemyShips[i].transform);
        }

        for (int i = 0; i < enemyShips.Count; i++)
        {
            enemyShips[i].SetShip(tempAlliedShips);
        }

        for (int i = 0; i < alliedShips.Count; i++)
        {
            alliedShips[i].SetShip(tempEnemyShips);
        }
        for (int i = 0; i < enemyShips.Count; i++)
        {
            if (enemyShips[i].gameObject.activeInHierarchy)
            {
                enemyShips[i].isInChargeOfPlantingBomb = true;
                break;
            }
        }
        for (int i = 0; i < enemyShips.Count; i++)
        {
            if (enemyShips[i].gameObject.activeInHierarchy)
            {
                enemyShips[i].isInChargeOfDefusingBomb = true;
                break;
            }
        }

        for (int i = 0; i < alliedShips.Count; i++)
        {
            if (alliedShips[i].gameObject.activeInHierarchy)
            {
                alliedShips[i].isInChargeOfPlantingBomb = true;
                break;
            }
        }
        for (int i = 0; i < alliedShips.Count; i++)
        {
            if (alliedShips[i].gameObject.activeInHierarchy)
            {
                alliedShips[i].isInChargeOfDefusingBomb = true;
                break;
            }
        }
    }

    public void BombCarrierDied(AIController ship)
    {
        //enemyShips.Remove(ship);
        if (ship.Team == Team.Enemy)
        {
            for (int i = 0; i < enemyShips.Count; i++)
            {
                if (enemyShips[i].gameObject.activeInHierarchy)
                {
                    enemyShips[i].isInChargeOfPlantingBomb = true;
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < alliedShips.Count; i++)
            {
                if (alliedShips[i].gameObject.activeInHierarchy)
                {
                    alliedShips[i].isInChargeOfPlantingBomb = true;
                    return;
                }
            }
        }
        //si no encontro nave, ya sea enemigo o aliado, espera unos segundos y vuelve a intentar
        StartCoroutine(SetNewShip(0.5f, ship));
    }

    public void DefuseCarrierDied(AIController ship)
    {
       //enemyShips.Remove(ship);
        if (ship.Team == Team.Enemy)
        {
            int rdm = Random.Range(0, enemyShips.Count);
            for (int i = 0; i < enemyShips.Count; i++)
            {
                if (enemyShips[i].gameObject.activeInHierarchy)
                {
                    enemyShips[i].isInChargeOfDefusingBomb = true;
                    return;
                }
            }
        }
        else
        {
            int rdm = Random.Range(0, alliedShips.Count);
            for (int i = 0; i < alliedShips.Count; i++)
            {
                if (alliedShips[i].gameObject.activeInHierarchy)
                {
                    alliedShips[i].isInChargeOfDefusingBomb = true;
                    return;
                }
            }
        }
        //si no encontro nave, ya sea enemigo o aliado, espera unos segundos y vuelve a intentar
        StartCoroutine(SetNewShip_(0.5f, ship));
    }

    IEnumerator SetNewShip(float delay, AIController ship)
    {
        yield return new WaitForSeconds(delay);
        BombCarrierDied(ship);

    }

    IEnumerator SetNewShip_(float delay, AIController ship)
    {
        yield return new WaitForSeconds(delay);
        DefuseCarrierDied(ship);

    }
}
