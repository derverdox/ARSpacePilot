using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject bigAsteroid;
    public GameObject smallAsteroid;

    float lastSpawnedAsteroid = 0;
    float lastSpawnedAsteroidBig = 0;
    float lastSpawnedAsteroidSmall = 0;

    GameController gameController;

    float cdAsteroid; //Cooldown für AsteroidenSpawn
    float cdAsteroidBig; //Cooldown für großen AsteroidenSpawn
    float cdAsteroidSmall; //Cooldown für kleinen Asteroidenspawn


    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Instantiate(asteroid); //Instanziere einen ersten Asteroiden unabhängig von den Cooldownds
    }

    // Update is called once per frame
    void Update()
    {
        updateVariables(); //Aktualisiert die Variablen
        if (Time.time > (lastSpawnedAsteroid + cdAsteroid)) //Wenn Cooldown abgelaufen ist kann wieder gespawned werden!
        {
            lastSpawnedAsteroid = Time.time;
            var enemy = Instantiate(asteroid);
            enemy.GetComponent<Enemy>().type = EnemyTypes.asteroidNormal;
        }
        if (Time.time > (lastSpawnedAsteroidBig + cdAsteroidBig)) //Wenn Cooldown abgelaufen ist kann wieder gespawned werden!
        {
            lastSpawnedAsteroidBig = Time.time;
            var enemy = Instantiate(bigAsteroid);
            enemy.GetComponent<Enemy>().type = EnemyTypes.asteroidBig;
        }
        if (Time.time > (lastSpawnedAsteroidSmall + cdAsteroidSmall)) //Wenn Cooldown abgelaufen ist kann wieder gespawned werden!
        {
            lastSpawnedAsteroidSmall = Time.time;
            var enemy = Instantiate(smallAsteroid);
            enemy.GetComponent<Enemy>().type = EnemyTypes.asteroidSmall;
        }
    }

    private void updateVariables() //Gettet Variablen aus dem GameController
    {
        cdAsteroid = gameController.cdAsteroid;
        cdAsteroidBig = gameController.cdAsteroidBig;
        cdAsteroidSmall = gameController.cdAsteroidSmall;
    }
}
