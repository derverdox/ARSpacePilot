using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachToARHand : MonoBehaviour
{

    public GameObject handTrackingPoint;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = handTrackingPoint.transform.position;
    }
}
