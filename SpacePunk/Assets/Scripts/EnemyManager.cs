using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] naves;
    EnemyController enemy;

    public void Update()
    {
        for(int i = 0; i < naves.Length;i++)
        {
            if(naves[0])
            {
                enemy.Bandera();
            }
        }
        for(int i = 0; i < naves.Length-1;i++)
        {
            enemy.Attack();
        }
    }

}
