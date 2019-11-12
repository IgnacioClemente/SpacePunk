using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombBehaviour : MonoBehaviour {

    [SerializeField] float timeToExplode;
    [SerializeField] Text timerText;
    [SerializeField] GameObject explosion;

    private bool countingDown;
    private float timer;
    private BaseBehaviour myBase;
    private Team team;

    private void Update()
    {
        if(countingDown)
        {
            timer -= Time.deltaTime;
            timerText.text = ((int)timer).ToString();

            if (timer <= 0)
                Explode();
        }
    }

    public void Plant(BaseBehaviour b, Team Team)
    {
        countingDown = true;
        team = Team;
        timer = timeToExplode;
        myBase = b;
        gameObject.SetActive(true);
    }

    public void Defuse(BaseBehaviour b, Team Team)
    {
        countingDown = false;
        team = Team;
        timer = timeToExplode;
        gameObject.SetActive(false);
    }

    public void Explode()
    {
        myBase.TakeDamage(team);
        gameObject.SetActive(false);
        explosion.SetActive(true);
        Invoke("HideExplosion", 1f);
    }

    private void HideExplosion()
    {
        explosion.SetActive(false);
    }
}
