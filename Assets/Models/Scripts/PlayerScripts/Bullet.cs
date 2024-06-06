using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 1f;
	GameObject origin = null;
    GameObject GameController;

    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController");
    }


    void Update()
    {
        bulletMove();
        checkForScreenEdge();
    }

    void bulletMove() //BulletMove
    {
        float movement = bulletSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * movement);
    }

    void checkForScreenEdge() //Checkt, ob die Bullet den Screen Rand verlassen hat.
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.y > 1.0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Checkt schon wieder nach Kollisionen :o
    {
        if (collision.gameObject.CompareTag("Enemy")) //Checkt auch diesmal ob es ein GameOBject mit dem Enemy Tag ist -> Asteroid 
        {
            destroyAsteroid(collision.gameObject);
        }
    }

    void destroyAsteroid(GameObject asteroid) //Extra Methode zum Zerstören eines Asteroiden
    {
        if (origin.CompareTag("Player"))
        {
            if (asteroid.gameObject.CompareTag("Enemy"))
            {
                Destroy(asteroid); //Zerstört den Asteroiden
                GameController.GetComponent<ParticleController>().summonExplosion(this.transform.position, this.transform.rotation);
                Destroy(gameObject); //Zerstört die Bullet

                switch (asteroid.GetComponent<Enemy>().type)
                {
                    case EnemyTypes.asteroidBig: GameController.GetComponent<GameController>().playerPoints += 5; break;
                    case EnemyTypes.asteroidNormal: GameController.GetComponent<GameController>().playerPoints += 10; break;
                    case EnemyTypes.asteroidSmall: GameController.GetComponent<GameController>().playerPoints += 20; break;
                    default: break;
                }

                 //Gibt dem Spieler 10 Punkte
            }
        }
    }

	public void setOrigin(GameObject gameObject){
		origin = gameObject;
	}
}
