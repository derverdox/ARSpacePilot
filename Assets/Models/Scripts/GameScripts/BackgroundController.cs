using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    float scrollSpeed;
    Vector2 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        resetPosition();
        moveBackground();

        if (CompareTag("Foreground")) //Wenn das GameObject des Scripts den Tag Foreground hat
        {
            scrollSpeed = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().foregroundScrollSpeed; //-> ForegroundSpeed Variable wird gesetzt
        }
        if (CompareTag("Background"))
        {
            scrollSpeed = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().backgroundScrollSpeed; //-> BackgroundSpeed Variable wird gesetzt
        }

    }

    void resetPosition() //Reset Position lässt das Background Sprite wieder über der Kamera auftauchen, um wieder nach unten laufen zu können.
    {
        if (transform.position.y < (Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f)).y)*2) //Abhängig von der Kameraposition
        {
            transform.position = new Vector3(transform.position.x, Camera.main.ViewportToWorldPoint(new Vector2(0f, 1f)).y*2, transform.position.z); //Zurücksetzen
        }
    }

    void moveBackground() //Lässt das Sprite nach unten laufen
    {
        transform.Translate(Vector3.down*scrollSpeed*Time.deltaTime);
    }
}
