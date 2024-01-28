using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesGame : MonoBehaviour
{
    private bool isPlaying;
    public static GlassesGame instance;
    public GameObject bar_area;
    public GameObject key;

    public void gameStart()
    {
        isPlaying = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            key.transform.localPosition = key.transform.localPosition + new Vector3(1, 0, 0);
        }
    }

    public void Hit()
    {
        // play animation
        gameEnd();
    }

    public void Miss()
    {
        key.transform.localPosition = new Vector3(-600, -319, 0);
    }

    void gameEnd()
    {
        key.SetActive(false);
        planeGame.instance.MiniGameEnd();
    }
}
