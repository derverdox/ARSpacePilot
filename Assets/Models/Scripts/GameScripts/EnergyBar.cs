using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    float originalScaleX;
    Vector3 originalPosition;

    float playerEnergy;
    float playerMaxEnergy;
    float lastPercent; //Speichert den letzten Prozentsatz, um mehrfache Größenanpassungen zu vermeiden

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        originalScaleX = transform.localScale.x; //Speichert die ursprüngliche X-Länge
        originalPosition = transform.position; //Speichert ursprüngliche Position
    }

    // Update is called once per frame
    void Update()
    {
        updateEnergyHealth();
        updateHealthbar();
    }

    void updateHealthbar()
    {
        changeSize();
    }


    void updateEnergyHealth() //Updatet PlayerVariablen für die Energybar
    {
        playerEnergy = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().playerEnergy;
        playerMaxEnergy = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().playerMaxEnergy;
    }


    void changeSize()
    {
        float originalValue = GetComponent<SpriteRenderer>().bounds.min.x; //Ursprüngliche Länge des Sprites
        float percentage = playerEnergy / playerMaxEnergy; //Wieviel Prozent Energy hat der Spieler?

        if (lastPercent != percentage)  //Vermeidet weitere Anpassungen beim gleichen Prozent
        {
            lastPercent = percentage; //Setzt die LastPercent Variable

            transform.localScale = new Vector3(originalScaleX * percentage, transform.localScale.y, transform.localScale.z); //Ändert die Größe des Sprites

            float newValue = GetComponent<SpriteRenderer>().bounds.min.x; //Länge des Sprites nach Anpassung

            float differencebetween = newValue - originalValue; //Differenz beider Längen -> X

            transform.Translate(new Vector3(-differencebetween, 0f, 0f)); //Setzt die Position des Sprites abzüglich der Differenz der beiden Längen, um auf die ursprüngliche Position zu kommem.
        }
    }
}
