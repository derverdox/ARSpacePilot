using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public GameObject warpParticle;
    public GameObject explosion;
    public GameObject explosionSound;
    public GameObject healOrbEffect;
    public GameObject healSound;
    public GameObject laserSound;

    public void summonExplosion(Vector3 position,Quaternion rotation)
    {
        var particleSystem = Instantiate(explosion, position, rotation);
        var particleSound = Instantiate(explosionSound, position, rotation);
        StartCoroutine(deleteParticle(particleSystem, particleSound));
    }

    public void summonHealEffect(Vector3 position,Quaternion rotation)
    {
        var particleSystem = Instantiate(healOrbEffect, position, rotation);
        var particleSound = Instantiate(healSound, position, rotation);
        StartCoroutine(deleteParticle(particleSystem, particleSound));
    }

    public void summonLaserSound(Vector3 position,Quaternion rotation)
    {
        var particleSound = Instantiate(laserSound, position, rotation);
        StartCoroutine(deleteParticle(null, particleSound));
    }


    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(warpParticle != null)
        {
            warpParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1);
        }
    }

    IEnumerator deleteParticle(GameObject particleSystem,GameObject particleSound)
    {
        yield return new WaitForSeconds(5);
        if(particleSystem != null)
        {
            Destroy(particleSystem);
        }
        if(particleSound != null)
        {
            Destroy(particleSound);
        }
    }

}
