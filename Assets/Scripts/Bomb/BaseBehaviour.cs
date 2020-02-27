using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBehaviour : MonoBehaviour
{
    [SerializeField] Team myTeam;
    [SerializeField] float maxHealth = 90;
    [SerializeField] Image healthBar;
    [SerializeField] BombBehaviour _myBomb;

    public float actualHealth;
    public int damage = 30;

    private Vector3 initialPosition;
    private bool planted = false;
    private bool defused = false;
    private float time = 3f;
    public bool isAlive = true;
    private bool bombExploted;

    public bool Planted { get { return planted; } set { planted = value; } }
    public bool Defused { get { return defused; } set { defused = value; } }
    public float ActualHealth { get { return actualHealth; } set { actualHealth = value; } }
    public bool Exploted { get { return bombExploted; } }

    public Team GetTeam()
    {
        return myTeam;
    }

    private void Awake()
    {
        actualHealth = maxHealth;
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void PlantBomb(Team team)
    {
        planted = true;
        _myBomb.Plant(this,team);
        BombGameManager.Instance.PlantBomb(myTeam);
        //la nave se queda quieta y no podra disparar,aparecera un texto arriba de la base, y cuando llegue a 0 le restara vida 
    }

    public void DefuseBomb(Team team)
    {
        defused = true;
        AIBombManager.Instance.AssingBombCarrier(team);
        planted = false;
        _myBomb.Defuse(this,team);
        BombGameManager.Instance.DefuseBomb(myTeam);
        //la nave se queda quieta y no podra disparar hasta que el texto que aparesca arriba llegue a 0
    }

    public void TakeDamage(Team team)
    {
        bombExploted = true;
        AIBombManager.Instance.AssingBombCarrier(team);
        AIBombManager.Instance.BombExploted(team);
        actualHealth -= damage;
        healthBar.fillAmount = actualHealth / maxHealth;

        if (actualHealth <= 0)
        {
            isAlive = false;
            gameObject.SetActive(false);
            BombGameManager.Instance.Win(team);
        }
        bombExploted = false;
        planted = false;
    }
}
