using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bulletPrefab;
    float energyCost;
    public GameController gameController;

    void Update()
    {
        shootLoop();
        getVariables();
    }

    void getVariables()
    {
        energyCost =
            gameController.playerMaxEnergy * 0.01f *
            gameController.energyCostInPercent; //Gettet die Variable aus dem GameController
    }

    public void shoot()
    {
        float playerEnergy = gameController.playerEnergy; //Gettet die Variable aus dem GameController
        float playerMaxEnergy = gameController.playerMaxEnergy; //Gettet die Variable aus dem GameController
        if (playerEnergy >= energyCost) //Hat der Spieler genug Energy?
        {
            if (gameController.quadroshot)
            {
                GetComponent<Player>().takeEnergy(energyCost); //Nimmt Energy über Player -> Über gameController
                spawnBullet(0.6f, 1f);
                spawnBullet(0.3f, 1f);
                spawnBullet(-0.3f, 1f);
                spawnBullet(-0.6f, 1f);
            }
            else if (gameController.doubleshot)
            {
                GetComponent<Player>().takeEnergy(energyCost); //Nimmt Energy über Player -> Über gameController
                spawnBullet(0.3f, 1f);
                spawnBullet(-0.3f, 1f);
            }
            else
            {
                GetComponent<Player>().takeEnergy(energyCost); //Nimmt Energy über Player -> Über gameController
                spawnBullet(0f, 1f);
            }

            GameObject.FindGameObjectWithTag("GameController").GetComponent<ParticleController>()
                .summonLaserSound(transform.position, transform.rotation);
        }
    }

    public void shootLoop()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Wenn der Input eine Spacebar ist
        {
            shoot();
        }
    }

    private void spawnBullet(float distX, float distY)
    {
        Vector3 spawnPos =
            new Vector3(transform.position.x + distX, transform.position.y + distY,
                transform.position.z); //Erstellt die Spawnposition der Bullet
        GameObject newBullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity); //Instanziere eine Bullet
        newBullet.GetComponent<Bullet>().setOrigin(this.gameObject);
    }
}