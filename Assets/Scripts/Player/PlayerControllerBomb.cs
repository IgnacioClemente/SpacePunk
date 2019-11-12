using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerBomb : PlayerController
{
    [SerializeField] BaseBehaviour enemyBase;
    [SerializeField] BaseBehaviour allyBase;
    [SerializeField] float minBombDistance = 15;
    [SerializeField] float minDefuseDistance = 15;
    [SerializeField] Text plantText;
    [SerializeField] Text defuseText;

    private bool isPlantingBomb;
    private bool defusingBomb;
    private float plantTimer = 3f;
    private float defuseTimer = 3f;

    public bool PlantingBomb { get { return isPlantingBomb; } }
    public bool DefusingBomb { get { return defusingBomb; } }

    protected override void Update()
    {
        base.Update();

        var distancePlant = Vector2.Distance(enemyBase.transform.position, transform.position);
        var distanceDefuse = Vector2.Distance(allyBase.transform.position, transform.position);
        plantText.text = ((int)plantTimer).ToString();
        defuseText.text = ((int)defuseTimer).ToString();

        if (!enemyBase.Planted)
        {
            if(distancePlant <= minBombDistance)
            {
                if(Input.GetKey(KeyCode.E))
                {
                    isPlantingBomb = true;
                    plantText.gameObject.SetActive(true);
                    plantTimer -= Time.deltaTime;
                    if(plantTimer <= 0)
                    {
                        enemyBase.PlantBomb(Team.Ally);
                        plantText.gameObject.SetActive(false);
                        plantTimer = 3f;
                        isPlantingBomb = false;
                    }
                }
                else if(Input.GetKeyUp(KeyCode.E))
                {
                    plantText.gameObject.SetActive(false);
                    plantTimer = 3f;
                    isPlantingBomb = false;
                }
            }
            else
            {
                plantText.gameObject.SetActive(false);
                isPlantingBomb = false;
            }
        }

        if(allyBase.Planted)
        {
            if(distanceDefuse <= minDefuseDistance)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    defusingBomb = true;
                    defuseText.gameObject.SetActive(true);
                    defuseTimer -= Time.deltaTime;
                    if (defuseTimer <= 0)
                    {
                        allyBase.DefuseBomb(Team.Ally);
                        defuseText.gameObject.SetActive(false);
                        defuseTimer = 3f;
                        defusingBomb = false;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    defuseText.gameObject.SetActive(false);
                    defuseTimer = 3f;
                    defusingBomb = false;
                }
            }
            else
            {
                defuseText.gameObject.SetActive(false);
                defusingBomb = false;
            }
        }
    }
}

