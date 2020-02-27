using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : PowerUp
{
    public override void ActivatePowerUp()
    {
        if (player != null)
        {
            //Aplico buff al player
            player.Heal(amount);
        }
        else
        {
            unit.Heal(amount);
        }
        Destroy(gameObject);
    }
}
