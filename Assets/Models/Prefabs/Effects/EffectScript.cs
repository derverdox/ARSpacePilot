using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    GameObject player;
    float rotationSpeed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        updatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        updatePosition();
        rotate();
    }

    void updatePosition()
    {
        transform.position = player.transform.position;
    }

    void rotate()
    {
        transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
    }
}
