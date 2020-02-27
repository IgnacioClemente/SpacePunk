using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPowerUp : PowerUp
{
    [SerializeField] private float duration = 5;

    public override void ActivatePowerUp()
    {
        if (player != null)
        {
            //Aplico buff al player
            player.BuffAttack(amount, duration);
            player = null;
        }
        else
        {
            unit.BuffAttack(amount, duration);
            unit = null;
        }
        Destroy(gameObject);
    }
}
