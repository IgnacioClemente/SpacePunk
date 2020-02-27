using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] List<PowerUp> powerUps;
    [SerializeField] float spawnRate = 10f;
    [SerializeField] float maxWidth = 40;
    [SerializeField] float maxHeight = 20;

    public static PowerUpManager Instance { get; private set; }

    private Camera mainCamera;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnPowerUp", 0, spawnRate);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
            SpawnPowerUp();*/
    }

    public void SpawnPowerUp()
    {
        var powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Count)], transform);
        powerUp.transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), Random.Range(-maxHeight, maxHeight));
    }
}
