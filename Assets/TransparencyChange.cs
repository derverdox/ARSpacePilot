using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyChange : MonoBehaviour
{
    private float transparency = 0.5f;
    void Start()
    {
        var transparentMaterial = GetComponent<MeshRenderer>().material;
        // Setze die Transparenz (Alpha-Wert)
        Color color = transparentMaterial.color;
        color.a = transparency; // 50% transparent
        transparentMaterial.color = color;
    }
}
