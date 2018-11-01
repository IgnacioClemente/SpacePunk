using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedManager : MonoBehaviour
{
    List<GameObject> naves;

    void Start()
    {
        naves = new List<GameObject>();
        foreach (Transform t in transform)
        {
            naves.Add(t.gameObject);
        }
        naves[0].GetComponent<AlliedController>().isInChargeOfTakingFlag = true;

    }

    public void TheGuyWithTheFlagDied(GameObject nave)
    {
        naves.Remove(nave);
        LookForNewFlagCarrier();
    }

    public void LookForNewFlagCarrier()
    {
        int rdm = Random.Range(0, naves.Count);
        naves[rdm].GetComponent<AlliedController>().isInChargeOfTakingFlag = true;
    }
}
