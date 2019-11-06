using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBomb : PlayerController
{
    [SerializeField] FlagBehavior enemyBase;
    [SerializeField] FlagBehavior allyBase;
    [SerializeField] float minBombDistance = 15;
    private bool canPlantBomb;
    private bool isPlantingBomb;
    private bool defusingBomb;

    public bool PlantingBomb { get { return isPlantingBomb; } }
    public bool DefusingBomb { get { return defusingBomb; } }


    protected override void Update()
    {
        base.Update();
    }
}

