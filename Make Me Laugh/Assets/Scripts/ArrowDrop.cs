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

    void Start()
    {
        speed = bpm / 60f;
    }

    void Update()
    {
        if (!gameOver) {
            // move arrows down
            transform.position -= new Vector3(0f, speed * Time.deltaTime * unit, 0f);
        }
    }
}
