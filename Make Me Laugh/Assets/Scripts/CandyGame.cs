using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CandyGame : MonoBehaviour
{
    public GameObject uiCanvas;
    public Camera game_cam;

    private int count = 0;
    private bool isPlaying;
    private RaycastHit raycastHit;

    public GameObject[] candies;
    private bool animPlayed = false;

    public GameObject green;
    public GameObject robot;

    public GameObject headlight;

    public GameObject coin_robot;
    public GameObject coin_robot_green;

    public void gameStart()
    {
        uiCanvas.SetActive(true);
        green.SetActive(true);
        robot.SetActive(true);
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            Check();
            if (count == 10)
            {
                green.GetComponent<Animation>().Play("candy_end");
                animPlayed = true;
                isPlaying = false;
            }
        }
        if (animPlayed)
        {
            if (green.GetComponent<AnimationEvent>().animFinished)
            {
                green.SetActive(false);
                headlight.GetComponent<Animation>().Play("green_on");
            }
            if (headlight.GetComponent<AnimationEvent>().animFinished)
            {
                robot.GetComponent<ChangeMaterial>().changeLight = true;
                animPlayed = false;
                gameEnd();
            }
            //if (!green.GetComponent<Animation>().isPlaying)
            //{
            //    gameEnd();
            //    animPlayed = false;
            //}
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
        uiCanvas.SetActive(false);
        coin_robot_green.SetActive(true);
        coin_robot.SetActive(false);
        robot.SetActive(false);
        foreach (GameObject candy in candies)
        {
            candy.SetActive(false);
        }
        planeGame.instance.MiniGameEnd("candy");
    }
}