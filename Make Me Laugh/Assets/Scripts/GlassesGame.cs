using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesGame : MonoBehaviour
{
    private bool isPlaying;
    public static GlassesGame instance;
    public GameObject bar_area;
    public GameObject key;
    public float speed = 4f;

    public GameObject robot;
    public GameObject glasses;

    private bool animPlayed = false;
    private int count = 0;

    public void gameStart()
    {
        robot.SetActive(true);
        instance = this;
        isPlaying = true;
        robot.GetComponent<AnimationEvent>().animFinished = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            key.transform.localPosition = key.transform.localPosition + new Vector3(speed, 0, 0);
        }
        //if (animPlayed)
        //{
        //    if (!robot.GetComponent<Animation>().isPlaying)
        //    {
        //        gameEnd();
        //        animPlayed = false;
        //    }
        //}

        if(count == 3)
        {
            glasses.GetComponent<Animation>().Play("glasses_glasses_after");
            robot.GetComponent<Animation>().Play("robot_glasses_after");
            if (robot.GetComponent<AnimationEvent>().animFinished)
            {
                gameEnd();
                isPlaying = false;
                //animPlayed = false;
            }
        }
    }

    public void Hit()
    {
        glasses.GetComponent<Animation>().Play("glasses_glasses_shake");
        key.transform.localPosition = new Vector3(-600, -319, 0);
        count++;
        //animPlayed = true;
        // play animation
    }

    public void Miss()
    {
        key.transform.localPosition = new Vector3(-600, -319, 0);
    }

    void gameEnd()
    {
        key.SetActive(false);
        robot.SetActive(false);
        robot.GetComponent<ChangeMaterial>().changeArm = true;
        glasses.SetActive(false);
        planeGame.instance.MiniGameEnd("glasses");
    }
}