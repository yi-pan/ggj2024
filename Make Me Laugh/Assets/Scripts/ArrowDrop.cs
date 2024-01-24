using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDrop : MonoBehaviour
{
    // bpm = 120; 2 beats per second
    public float bpm;
    public float unit; // distance arrow drop
    private float speed;
    public bool gameOver = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = bpm / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            // press space to start our game
            if (Input.GetKeyDown("space"))
            {
                gameOver = false;
            }
        }
        else
        {
            // move arrows down
            transform.position -= new Vector3(0f, speed * Time.deltaTime * unit, 0f);
        }
    }
}
