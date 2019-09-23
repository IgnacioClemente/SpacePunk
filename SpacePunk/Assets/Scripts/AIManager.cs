using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    List<AIController> enemyShips;
    List<AIController> alliedShips;

    public static AIManager Instance { get; private set; }

   private void Awake()
    {
        if (Instance != null) Destroy(this);

        Instance = this;

        enemyShips = new List<AIController>();
        alliedShips = new List<AIController>();

        foreach (Transform child in transform)
        {
            var ship = child.GetComponent<AIController>();
            if(ship.Team == Team.Enemy)
                enemyShips.Add(ship);
            else
                alliedShips.Add(ship);
        }
        for (int i = 0; i < enemyShips.Count; i++)
        {
            enemyShips[i].SetShip(alliedShips);
        }

        for (int i = 0; i < alliedShips.Count; i++)
        {
            alliedShips[i].SetShip(enemyShips);
        }
        
        int rdm = Random.Range(0, enemyShips.Count);
        enemyShips[rdm].GetComponent<AIFlagController>().isInChargeOfTakingFlag = true;

        int alliedRdm = Random.Range(0, alliedShips.Count);
        alliedShips[alliedRdm].GetComponent<AIFlagController>().isInChargeOfTakingFlag = true;

        //Test
        Time.timeScale = 4;
    }

    public void FlagCarrierDied(AIController ship)
    {
        //enemyShips.Remove(ship);
        if (ship.Team == Team.Enemy)
        {
            int rdm = Random.Range(0, enemyShips.Count);
            if (enemyShips[rdm] == ship || !enemyShips[rdm].gameObject.activeSelf)
                FlagCarrierDied(ship);

            enemyShips[rdm].GetComponent<AIFlagController>().isInChargeOfTakingFlag = true;
        }
        else
        {
            int rdm = Random.Range(0, alliedShips.Count);
            if (alliedShips[rdm] == ship)
                FlagCarrierDied(ship);

            alliedShips[rdm].GetComponent<AIFlagController>().isInChargeOfTakingFlag = true;

        }
    }
}
