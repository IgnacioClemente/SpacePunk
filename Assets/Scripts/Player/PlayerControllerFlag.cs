using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFlag : PlayerController
{
    [SerializeField] FlagBehavior enemyFlag;
    [SerializeField] FlagBehavior allyFlag;

    protected override void Die()
    {
        base.Die();
        if (hasFlag)
        {
            enemyFlag.DropFlag(transform.position);
            hasFlag = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == allyFlag.transform)
        {
            if (hasFlag && !allyFlag.Captured)
            {
                FlagGameManager.Instance.ScoreUp(Team.Ally);
                hasFlag = false;
                enemyFlag.ResetFlag();
            }
            else
                allyFlag.ResetFlag();
        }

        if (other.transform == enemyFlag.transform)
        {
            enemyFlag.CaptureFlag();
            hasFlag = true;
        }
    }

}
