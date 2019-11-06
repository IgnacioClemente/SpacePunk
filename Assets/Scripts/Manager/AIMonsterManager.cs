using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonsterManager : MonoBehaviour
{
    List<AIMonsterController> enemyShips;
    List<AIMonsterController> alliedShips;
    List<MonsterController> monsterList;

    public static AIMonsterManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        Instance = this;

        enemyShips = new List<AIMonsterController>();
        alliedShips = new List<AIMonsterController>();
        monsterList = new List<MonsterController>();

        foreach (Transform child in transform)
        {
            var ship = child.GetComponent<AIMonsterController>();
            var monster = child.GetComponent<MonsterController>();
            if (ship.Team == Team.Enemy)
                enemyShips.Add(ship);
            else
                alliedShips.Add(ship);
            if (monster.Team == Team.Monster)
                monsterList.Add(monster);
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

        for (int i = 0; i < monsterList.Count; i++)
        {
            tempMonsterList.Add(monsterList[i].transform);
        }

        for (int i = 0; i < enemyShips.Count; i++)
        {
            enemyShips[i].SetShip(tempAlliedShips);
        }

        for (int i = 0; i < alliedShips.Count; i++)
        {
            alliedShips[i].SetShip(tempEnemyShips);
        }

        for (int i = 0; i < monsterList.Count; i++)
        {
            monsterList[i].SetShip(tempMonsterList);
        }


        //Test
        Time.timeScale = 5;
    }

}
