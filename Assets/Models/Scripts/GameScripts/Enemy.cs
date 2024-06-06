using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyTypes type;
    public float minRotSpeed;
    public float maxRotSpeed;
    public float minSpeed;
    public float maxSpeed;
    float speed;
    float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        setStartPointAndSpeed();
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("EnemyList").GetComponent<Transform>());
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
        checkEdgePosition();
    }

    void setStartPointAndSpeed() //Generiert Startposition des Asteroiden und die Geschwindigkeit, mit der er nach unten fällt
    {
        
        speed = Random.Range(minSpeed, maxSpeed); //Random Zahl zwischen minSpeed und maxSpeed
        rotationSpeed = Random.Range(minRotSpeed, maxRotSpeed); //Random Zahl zwischen minRotSpeed und maxRotSpeed

        float x = Random.Range(Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f)).x, Camera.main.ViewportToWorldPoint(new Vector2(0f, 1f)).x); //Random X Koordinate abhängig von der Kamera -> zw. 1 und 0
        float y = Camera.main.ViewportToWorldPoint(new Vector2(0f, 1f)).y; //Abhängig von der Kamera am oberen Rand der Kamera
        float z = 0; //z=0 -> 2D

        transform.position = new Vector3(x, y, z);
    }

    void moveEnemy() //Bewegt den Enemy mit dem speed nach unten
    {
        transform.Translate(Vector3.down* speed * Time.deltaTime,Space.World);
        rotateEnemy();
    }

    void rotateEnemy()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            transform.Rotate(0f * rotationSpeed, 0f * rotationSpeed, 1f * rotationSpeed, Space.World);
        }
    }

    void checkEdgePosition() //Wenn der Asteroid den Kameraedge unten erreicht hat despawned er
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.y < 0)
        {
            Destroy(gameObject);
        }
    }
}

public enum EnemyTypes
{
    asteroidSmall,
    asteroidNormal,
    asteroidBig,
    collectible
}
