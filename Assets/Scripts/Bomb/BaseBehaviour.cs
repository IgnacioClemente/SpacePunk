using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    [SerializeField] Team myTeam;
    [SerializeField] int maxHealth = 100;

    public int actualHealth;
    public int damage = 50;

    private Vector3 initialPosition;
    private bool planted = false;
    private bool defused = false;
    private float time = 3f;
    public bool isAlive = true;
    private bool bombExploted;

    public bool Planted { get { return planted; } set { planted = value; } }
    public bool Defused { get { return defused; } set { defused = value; } }
    public int ActualHealth { get { return actualHealth; } set { actualHealth = value; } }
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

    public void PlantBomb()
    {
        BombGameManager.Instance.PlantBomb(myTeam);
        //la nave se queda quieta y no podra disparar,aparecera un texto arriba de la base, y cuando llegue a 0 le restara vida 
    }

    public void DefuseBomb()
    {
        BombGameManager.Instance.DefuseBomb(myTeam);
        //la nave se queda quieta y no podra disparar hasta que el texto que aparesca arriba llegue a 0
    }

    public void TakeDamage()
    {
        bombExploted = true;
        actualHealth -= damage;

        if (actualHealth <= 0)
        {
            isAlive = false;
            gameObject.SetActive(false);
        }
        bombExploted = false;
    }
}
