﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<GameObject> naves;

    void Start()
    {
        naves = new List<GameObject>();
        foreach (Transform t in transform)
        {
            naves.Add(t.gameObject);
        }
        naves[0].GetComponent<EnemyController>().isInChargeOfTakingFlag = true;
        
    }

    public void TheGuyWithTheFlagDied(GameObject nave)
    {
       naves.Remove(nave);
       LookForNewFlagCarrier();;
    }

   public void LookForNewFlagCarrier()
    {
        int rdm = Random.Range(0, naves.Count);
        naves[rdm].GetComponent<EnemyController>().isInChargeOfTakingFlag = true;
    }
}
