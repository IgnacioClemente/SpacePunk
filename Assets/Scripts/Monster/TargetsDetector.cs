using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsDetector : MonoBehaviour
{
    [SerializeField] MonsterController myMonster;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Allied") || collision.CompareTag("Enemy"))
            myMonster.AddTarget(collision.transform);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Allied") || collision.CompareTag("Enemy"))
            myMonster.RemoveTarget(collision.transform);
    }
}
