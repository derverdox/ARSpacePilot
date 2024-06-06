using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public float playerHealth;
    public float playermaxHealth;
    public float playerEnergy;
    public float playerMaxEnergy;
    public float cooldown;
    public float lastEnergyFill;
    GameController gameController;

    bool tiltedLeft = false;
    bool tiltedRight = false;

    public GameObject healSound;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        updateVariables();
        playerMoveSide();
        playerMoveVertical();
        screenWrap();
    }

    void updateVariables()
    {
        playerSpeed = gameController.playerSpeed;
        playerHealth = gameController.playerHealth;
        playermaxHealth = gameController.playerMaxHealth;
        playerEnergy = gameController.playerEnergy;
        playerMaxEnergy = gameController.playerMaxEnergy;
        cooldown = gameController.energyRefillCooldown;
    }

    private void LateUpdate()
    {
        healthCheck();
    }

    void playerMoveSide()
    {
        float movement = Input.GetAxis("Horizontal")*playerSpeed*Time.deltaTime;
        transform.Translate(Vector3.right * movement,Space.World);

        if (movement > 0)
        {
            if (!tiltedLeft)
            {
                tiltedLeft = true;
                gameObject.transform.Rotate(0f, -30f, 0f, Space.World);
            }
        }
        else if (movement < 0)
        {
            if (!tiltedRight)
            {
                tiltedRight = true;
                gameObject.transform.Rotate(0f, 30f, 0f, Space.World);
            }
        }
        else
        {
            tiltedRight = false;
            tiltedLeft = false;
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    void playerMoveVertical()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); //Position in Relation zur MainCamera

        float movement = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        if (pos.y > 1.0)
        {
            if(movement > 0) {
                Debug.Log("Border oben erreicht!");
                return;
            }
        }
        else if(pos.y < 0)
        {
            if(movement < 0)
            {
                Debug.Log("Border unten erreicht!");
                return;
            }
        }
        transform.Translate(Vector3.up * movement);
    }

    void screenWrap() //Spieler kann Spielfeld nach rechts verlassen und kommt links raus!
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); //Position in Relation zur MainCamera

        if (pos.x < 0.0) //Links vom Kamerarand
        {
            float y = transform.position.y;
            transform.position = Camera.main.ViewportToWorldPoint(new Vector2(1f, 0.06f));
            transform.position = new Vector3(transform.position.x, y, 0f);
        }
        else if (pos.x > 1.0) //Rechts vom Kamerarand
        {
            float y = transform.position.y;
            transform.position = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0.06f));
            transform.position = new Vector3(transform.position.x, y, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Kollisionsabfrage
    {
        if (collision.gameObject.CompareTag("Enemy")) //Wenn das colliderObject ein GameObject mit Tag "Enemy" ist -> Asteroid
        {
            if(gameController.invulnerable == false)
            {
                takeDamage(1f);
            }
            else
            {
                switch (collision.GetComponent<Enemy>().type)
                {
                    case EnemyTypes.asteroidBig: gameController.GetComponent<GameController>().playerPoints += 5; break;
                    case EnemyTypes.asteroidNormal: gameController.GetComponent<GameController>().playerPoints += 10; break;
                    case EnemyTypes.asteroidSmall: gameController.GetComponent<GameController>().playerPoints += 20; break;
                    default: break;
                }
            }
            gameController.GetComponent<ParticleController>().summonExplosion(transform.position, transform.rotation);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("col_repair"))
        {
            //Effekt
            gameController.GetComponent<ParticleController>().summonHealEffect(transform.position, transform.rotation);
            Instantiate(healSound, transform.position, transform.rotation);
            Destroy(collision.gameObject);

            //Wirkung
            float healthGain = playermaxHealth * 0.2f;
            gameController.playerHealth += healthGain;
        }
        else if (collision.gameObject.CompareTag("col_invulnerable"))
        {
            //Effekt
            gameController.GetComponent<ParticleController>().summonHealEffect(transform.position, transform.rotation);
            Instantiate(healSound, transform.position, transform.rotation);
            Destroy(collision.gameObject);

            //Wirkung
            gameController.gameObject.GetComponent<BoosterApply>().applyInvulnerability();
        }
        else if (collision.gameObject.CompareTag("col_doubleshot"))
        {
            //Effekt
            gameController.GetComponent<ParticleController>().summonHealEffect(transform.position, transform.rotation);
            Instantiate(healSound, transform.position, transform.rotation);
            Destroy(collision.gameObject);

            //Wirkung
            gameController.gameObject.GetComponent<BoosterApply>().applyDoubleShot();
        }
        else if (collision.gameObject.CompareTag("col_quadroshot"))
        {
            //Effekt
            gameController.GetComponent<ParticleController>().summonHealEffect(transform.position, transform.rotation);
            Instantiate(healSound, transform.position, transform.rotation);
            Destroy(collision.gameObject);

            //Wirkung
            gameController.gameObject.GetComponent<BoosterApply>().applyQuadroShot();
        }
    }

    private void takeDamage(float damage) //Apply Damage to Player - > gameController
    {
        gameController.playerTakeDamage(damage);
    }

    public void takeEnergy(float energy) //Take Energy from Player -> GameController
    {
        gameController.playerTakeEnergy(energy);
    }

    void healthCheck() //Checkt, ob der Spieler weniger oder = 0 Leben hat -> Wenn ja zerstört ihn!
    {
        if(playerHealth <= 0)
        {
            if(gameController.gameRunning == true)
            {
                gameController.gameRunning = false;
                gameController.stopGame();
                Destroy(gameObject);
            }
        }
    }
    }
