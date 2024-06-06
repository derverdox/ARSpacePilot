using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundDeleter : MonoBehaviour
{
    float cooldown;

    private void Start()
    {
        cooldown = Time.time + 5;
        transform.SetParent(GameObject.FindGameObjectWithTag("EnemyList").GetComponent<Transform>());
    }

    private void Update()
    {
        if (Time.time > cooldown)
        {
            Destroy(this.gameObject);
        }
    }
}
