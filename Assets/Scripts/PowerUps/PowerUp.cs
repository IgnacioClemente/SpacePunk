using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected int amount = 5;

    public abstract void ActivatePowerUp();

    protected PlayerController player;
    protected AIController unit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            ActivatePowerUp();
        }
        else
        {
            unit = other.GetComponent<AIController>();
            ActivatePowerUp();
        }
    }
}
