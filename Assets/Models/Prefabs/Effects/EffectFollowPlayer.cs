using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFollowPlayer : MonoBehaviour
{
    void Update()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
