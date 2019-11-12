using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonsterManager : MonoBehaviour
{
    [SerializeField] MonsterController monster;

    List<AIMonsterController> enemyShips;
    List<AIMonsterController> alliedShips;

    public static AIMonsterManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        Instance = this;

        enemyShips = new List<AIMonsterController>();
        alliedShips = new List<AIMonsterController>();

        foreach (Transform child in transform)
        {
            var ship = child.GetComponent<AIMonsterController>();

            if (ship.Team == Team.Enemy)
                enemyShips.Add(ship);
            else
                alliedShips.Add(ship);
        }

        var tempAlliedShips = new List<Transform>();
        var tempEnemyShips = new List<Transform>();
        var tempMonsterList = new List<Transform>();

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
            enemyShips[i].SetMonster(monster);
        }

        for (int i = 0; i < alliedShips.Count; i++)
        {
            alliedShips[i].SetShip(tempEnemyShips);
            alliedShips[i].SetMonster(monster);
        }

        for (int i = 0; i < enemyShips.Count; i++)
        {
            if (enemyShips[i].gameObject.activeInHierarchy)
            {
                enemyShips[i].isInChargeOfShootingMonster = true;
                break;
            }
        }

        for (int i = 0; i < alliedShips.Count; i++)
        {
            if (alliedShips[i].gameObject.activeInHierarchy)
            {
                alliedShips[i].isInChargeOfShootingMonster = true;
                break;
            }
        }
    }

}
