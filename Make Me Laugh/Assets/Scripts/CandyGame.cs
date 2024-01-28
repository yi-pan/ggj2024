using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CandyGame : MonoBehaviour
{
    public Camera game_cam;

    private int count = 0;
    private bool isPlaying;
    private RaycastHit raycastHit;

    public GameObject[] candies;
    
    public void gameStart()
    {
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            Check();
            if(count == 10)
            {
                // play animation
                // game end
                isPlaying = false;
                gameEnd();
            }
        }
    }

    void Check()
    {
        count = 0;
        foreach (GameObject candy in candies)
        {
            if (candy.transform.localPosition.y < -4)
            {
                count++;
            }
        }
    }

    void gameEnd()
    {
        foreach (GameObject candy in candies)
        {
            candy.SetActive(false);
        }
        planeGame.instance.MiniGameEnd("candy");
    }
}
