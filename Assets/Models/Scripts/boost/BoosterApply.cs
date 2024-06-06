using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterApply : MonoBehaviour
{
    public float invulnerableTime;
    private float appliedInvulnerableEffectStartTime = 0f;
    private float invulnerableEffectTime = 0f;
    private GameObject invulnerableObject;

    public float doubleShotTime;
    private float appliedDoubleShotEffectStartTime = 0f;
    private float doubleShotEffectTime = 0f;

    public float quadroShotTime;
    private float appliedquadroShotEffectStartTime = 0f;
    private float quadroShotEffectTime = 0f;

    GameController gameController;

    public GameObject preFab_invulnerableEffect;

    private void Start()
    {
        this.gameController = gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

    }

    private void Update()
    {
        checkInvulnerability();
        checkDoubleShot();
        checkQuadroShot();
    }

    public void applyQuadroShot()
    {
        gameController.quadroshot = true;

        quadroShotEffectTime = quadroShotTime;
        appliedquadroShotEffectStartTime = Time.time;
    }

    public void applyDoubleShot()
    {
        gameController.doubleshot = true;

        doubleShotEffectTime = doubleShotTime;
        appliedDoubleShotEffectStartTime = Time.time;
    }


    public void applyInvulnerability()
    {
        gameController.invulnerable = true;

        if(invulnerableEffectTime == 0) //Das erste Mal, dass der Boost gesammelt wird!
        {
            invulnerableEffectTime = invulnerableTime;
            appliedInvulnerableEffectStartTime = Time.time;
        }

        if(invulnerableObject == null) //Wenn das Object null ist, ist der Boost einmal komplett ausgelaufen -> Es kann von neuem begonnen werden!
        {
            invulnerableObject = Instantiate(preFab_invulnerableEffect, transform.position, transform.rotation);
            appliedInvulnerableEffectStartTime = Time.time;
        }
        else
        {
            Destroy(invulnerableObject.gameObject); //Ansonsten wird der alte Ring zerstört
            invulnerableObject = Instantiate(preFab_invulnerableEffect, transform.position, transform.rotation); //Ein neuer Ring wird erschaffen

            if((Time.time - (appliedInvulnerableEffectStartTime + invulnerableEffectTime)) < 0) //Wenn die errechnete Zahl unter 0 ist, bedeutet dies, dass noch ein Timer läuft!
            {
                invulnerableEffectTime = (Mathf.Abs(Time.time - (appliedInvulnerableEffectStartTime + invulnerableEffectTime)) + invulnerableTime); //Die übrige Zeit wird draufgerechnet, jedoch keine neue Startzeit gesetzt
            }
        }
    }

    private void checkQuadroShot()
    {
        if (gameController.quadroshot == true)
        {
            if ((Time.time - (appliedquadroShotEffectStartTime + quadroShotEffectTime)) >= 0)
            {
                appliedquadroShotEffectStartTime = Time.time;
                quadroShotEffectTime = 0;
                gameController.quadroshot = false;
            }
        }
    }

    private void checkDoubleShot()
    {
        if(gameController.doubleshot == true)
        {
            if((Time.time - (appliedDoubleShotEffectStartTime + doubleShotEffectTime)) >= 0)
            {
                appliedDoubleShotEffectStartTime = Time.time;
                doubleShotEffectTime = 0;
                gameController.doubleshot = false;
            }
        }
    }

    private void checkInvulnerability()
    {
        if(gameController.invulnerable == true)
        {
            if(Time.time > appliedInvulnerableEffectStartTime + invulnerableEffectTime)
            {
                appliedInvulnerableEffectStartTime = Time.time;
                invulnerableEffectTime = invulnerableTime;
                Destroy(invulnerableObject.gameObject);
                invulnerableObject = null;
                gameController.invulnerable = false;
            }
        }
    }
}

public enum EffectTypes
{
    Invulnerability,
};
