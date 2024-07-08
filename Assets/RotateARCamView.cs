using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateARCamView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount >= 1)
        {
            //gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(180, 0, 0);
        }
    }
}