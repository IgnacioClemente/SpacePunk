using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGameManager : MonoBehaviour
{
    public static FlagGameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        Instance = this;
    }

    public void ResetFlag(Vector3 enemyFlagPos, Vector3 alliedFlagPos, Transform enemyFlag, Transform alliedFlag)
    {
        enemyFlag.position = enemyFlagPos;
        alliedFlag.position = alliedFlagPos;
        alliedFlag.gameObject.SetActive(true);
        enemyFlag.gameObject.SetActive(true);
    }
}
