using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] private bool playerAllies;
    [SerializeField] private AIManager enemyTeam;

    List<GameObject> naves;

    void Awake()
    {
        naves = new List<GameObject>();
        foreach (Transform t in transform)
        {
            naves.Add(t.gameObject);
        }
        //naves[0].GetComponent<EnemyController>().isInChargeOfTakingFlag = true;
        
    }

    public void TheGuyWithTheFlagDied(GameObject nave)
    {
       naves.Remove(nave);
       LookForNewFlagCarrier();
    }

   public void LookForNewFlagCarrier()
    {
        int rdm = Random.Range(0, naves.Count);
        //naves[rdm].GetComponent<EnemyController>().isInChargeOfTakingFlag = true;
    }
}
