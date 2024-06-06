using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	//GameController Object beherbergt alle wichtigen Spielevariablen, um sie an einem Ort zu sammeln und anpassen zu können. Variablen die Objectspezifisch sind sind hier nicht enthalten.

    #region BackgroundVariables
    public float foregroundScrollSpeed = 1f; //Vordergrundsterne Scrollspeed
    public float backgroundScrollSpeed = 1f; //Hintergrundsterne Scrollspeed
    #endregion
    #region PlayerVariables
    public float playerSpeed = 15; //Spielerspeed
    public float playerHealth = 1f; //Spielerleben
    public float playerMaxHealth = 1f; //SpielermaxLeben
    public float playerEnergy = 1f; //Spieler Energy
    public float playerMaxEnergy = 1f; //Spieler max Energy
    public float playerPoints = 0f;
    public float energyCostInPercent = 1f; //Wieviel % kostet eine Basisattacke
    public float energyRefillCooldown = 1f; //Wie lange dauert ein EnergyRefillTick
    public float energyRefillInPercent = 1f; //Wie viel Energy soll aufgeladen werden
	public float multiplier = 1f;
    float lastEnergyFill; //Wann wurde das letzte mal Energy aufgefüllt.
    #endregion
    #region AsteroidSpawnerValues
    public float cdAsteroid = 1f; //Wie lange dauert es zwischen Asteroidenspawns
    public float cdAsteroidBig = 1f; //'' für große Asteroiden
    public float cdAsteroidSmall = 1f; //'' für kleine Asteroiden
    float cdAsteroidStart;
    float cdAsteroidBigStart;
    float cdAsteroidSmallStart;
    #endregion
    #region SystemVariables
    public float timer; //Timervariable
    float lastTimertick; //Wann der Timer das letzte mal getickt hat
    public Text timerText; //Timertext am oberen Spielrand
    public Text pointText;
    public bool gameRunning;
    #endregion
    #region boostBools
    public bool invulnerable = false;
    public bool doubleshot = false;
    public bool quadroshot = false;
    public bool lasershot = false;
    public bool slowmotion = false;
    #endregion

    private void Start()
    {
        gameRunning = true;
        cdAsteroidStart = cdAsteroid;
        cdAsteroidBigStart = cdAsteroidBig;
        cdAsteroidSmallStart = cdAsteroidSmall;
    }

    private void Update()
    {
        fillEnergy();
        checkHealth();
        difficultyRaise();
        
    }

    private void FixedUpdate()
    {
        setTimerText();
        setPointText();
    }

    #region PlayerMethods
    public void playerTakeDamage(float damage)
    {
        playerHealth -= damage;
    }

    public void playerTakeEnergy(float energy)
    {
        playerEnergy -= energy;
    }

    private void fillEnergy()
    {
        if (Time.time > (lastEnergyFill + energyRefillCooldown))
        {
            lastEnergyFill = Time.time;
            float newValue = playerEnergy + playerMaxEnergy * 0.01f * energyRefillInPercent;
            if (playerEnergy < playerMaxEnergy)
            {
                if (newValue <= playerMaxEnergy)
                {
                    playerEnergy = playerEnergy + playerMaxEnergy * 0.01f * energyRefillInPercent;
                }
                else
                {
                    playerEnergy = playerMaxEnergy;
                }
            }
        }
    }

    private void checkHealth()
    {
        if(playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }
    #endregion

    private void difficultyRaise()
    {
        cdAsteroid = cdAsteroidStart - timer*0.001f;
        cdAsteroidBig = cdAsteroidBigStart - timer * 0.002f;
        cdAsteroidSmall = cdAsteroidSmallStart - timer * 0.001f;

    }

    #region Timer
    public void addToTimer()
    {
        timer = timer + 1;
    }

    void setTimerText()
    {
        if (Time.time > lastTimertick + 1)
        {
            lastTimertick = Time.time;
            addToTimer();
            timerText.text = "Überlebenszeit: " + (timer).ToString();
        }
    }
    #endregion

    void setPointText()
    {
        pointText.text = "Punkte: " + playerPoints;
    }

    public void stopGame()
    {
        StartCoroutine(LoosingScreen());
    }



    public IEnumerator LoosingScreen()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("LooseMenu");
        SceneManager.UnloadSceneAsync("Level1");
    }
}
