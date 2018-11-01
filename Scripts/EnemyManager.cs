using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<GameObject> naves;
    EnemyController enemy;

    public void Start()
    {
        naves = new List<GameObject>();
        enemy = GetComponentInChildren<EnemyController>();

        foreach (Transform t in transform)
        {
            naves.Add(t.gameObject);
        }
        naves[0].GetComponent<EnemyController>().isInChargeOfTakingFlag = true;
        
    }
   /* public void Update()
    {
        naves[0].GetComponent<EnemyController>().isGoingToFlag = true;
           
                int rdm = Random.Range(0, naves.Count);
                naves[rdm].GetComponent<EnemyController>().CapturarBandera();
    }*/

    }
