using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public List<Collectible> collectibles = new List<Collectible>();
    public GameObject preFab_col_repair;
    public GameObject preFab_col_invulnerable;
    public GameObject preFab_col_doubleshot;
    public GameObject preFab_col_quadrashoot;
    public GameObject preFab_col_lasershoot;
    public GameObject preFab_col_slowmotion;

    float lastSpawned1;
    public float cooldown1;
    float lastSpawned2;
    public float cooldown2;
    float lastSpawned3;
    public float cooldown3;

    private void Start()
    {
        collectibles.Add(new Collectible("Reperatur",0f,20,preFab_col_repair));
        collectibles.Add(new Collectible("Unverwundbarkeit", 20, 40, preFab_col_invulnerable));
        collectibles.Add(new Collectible("Doppelschuss", 40, 60f, preFab_col_doubleshot));
        collectibles.Add(new Collectible("VierfachSchuss", 60f, 80f, preFab_col_quadrashoot));
      //  collectibles.Add(new Collectible("Reperatur", 0f, 2f, preFab_col_lasershoot));
      //  collectibles.Add(new Collectible("Reperatur", 0f, 1f, preFab_col_slowmotion));
    }

    private void Update()
    {
        if (Time.time > (lastSpawned1 + cooldown1))
        {
            lastSpawned1 = Time.time;
            SpawnCollectible();
        }
        if (Time.time > (lastSpawned2 + cooldown2))
        {
            lastSpawned2 = Time.time;
            SpawnCollectible();
        }
        if (Time.time > (lastSpawned3 + cooldown3))
        {
            lastSpawned3 = Time.time;
            SpawnCollectible();
        }
    }

    void SpawnCollectible()
    {
        
        int random = Random.Range(0, 100);
        Debug.Log(random);

        for (int j = 0; j < collectibles.Count; j++)
        {
            if(random >= collectibles[j].minChance && random <= collectibles[j].maxChance)
            {
                var collectible = Instantiate(collectibles[j].preFab,transform.position,transform.rotation);
                collectible.GetComponent<Enemy>().type = EnemyTypes.collectible;
                break;
            }
        }
    }


}

[System.Serializable]
public class Collectible
{
    public string name;
    public float minChance;
    public float maxChance;
    public GameObject preFab;

    public Collectible(string name,float minChance,float maxChance,GameObject preFab)
    {
        this.name = name;
        this.minChance = minChance;
        this.maxChance = maxChance;
        this.preFab = preFab;
    }
}
