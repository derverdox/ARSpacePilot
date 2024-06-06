using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    float originalScaleX;
    Vector3 originalPosition;

    float playerHealth;
    float playerMaxHealth;
    float lastPercent;

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        originalScaleX = transform.localScale.x;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        updatePlayerHealth();
        updateHealthbar();
    }

    void updateHealthbar()
    {
        changeSize();
    }


    void updatePlayerHealth()
    {
        playerHealth = player.playerHealth;
        playerMaxHealth = player.playermaxHealth;

    }


    void changeSize()
    {
        float originalValue = GetComponent<SpriteRenderer>().bounds.min.x;
        float percentage = playerHealth / playerMaxHealth;

        if (lastPercent != percentage)
        {
            lastPercent = percentage;

            transform.localScale = new Vector3(originalScaleX * percentage, transform.localScale.y, transform.localScale.z);

            float newValue = GetComponent<SpriteRenderer>().bounds.min.x;
            
            float differencebetween = newValue - originalValue;

            transform.Translate(new Vector3(-differencebetween, 0f, 0f));
        }
    }
}
