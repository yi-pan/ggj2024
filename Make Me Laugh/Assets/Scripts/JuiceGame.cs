using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JuiceGame : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject juice;
    public GameObject cup;
    public GameObject battery;
    public GameObject batteryBK;
    public GameObject straw;
    public GameObject robot;

    private int clickCount = 0;
    //private int totalCount = 20;
    private Vector3 juiceScale;
    private RaycastHit raycastHit;
    private bool isPlaying;

    public Camera game_cam;

    public Material orange;
    public Material green;

    public AudioSource slurp;

    public void gameStart()
    {
        uiCanvas.SetActive(true);
        robot.SetActive(true);
        //isPlaying = true;
        straw.GetComponent<Animation>().Play("juice_straw");
        juiceScale = juice.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (straw.GetComponent<AnimationEvent>().animFinished)
        {
            straw.GetComponent<AnimationEvent>().animFinished = false;
            isPlaying = true;
        }

        // click on juice
        if (isPlaying)
        {
            // raycast
            Ray ray = game_cam.ScreenPointToRay(Input.mousePosition);
            // hover
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.transform.name == "cup" & juice.transform.localScale.y > 0)
                {
                    cup.transform.localScale = new Vector3(24f, 24f, 24f);
                    if (Input.GetMouseButtonDown(0))
                    {
                        // play slurp sound effect
                        slurp.Play();

                        clickCount++;
                        juice.transform.localScale = juice.transform.localScale - new Vector3(0, 0.05f, 0);
                        battery.transform.localScale = battery.transform.localScale + new Vector3(0.0085f, 0, 0);
                        battery.transform.localPosition = battery.transform.localPosition + new Vector3(-0.0047f, 0, 0);
                    }
                    if (clickCount == 6)
                    {
                        battery.GetComponent<Renderer>().material = orange;
                    }
                    if (clickCount == 12)
                    {
                        battery.GetComponent<Renderer>().material = green;
                    }
                }
                else
                {
                    cup.transform.localScale = new Vector3(23f, 23f, 23f);
                }
            }
            if (juice.transform.localScale.y < 0)
            {
                isPlaying = false;
                gameEnd();
            }
        }
    }

    void gameEnd()
    {
        uiCanvas.SetActive(false);
        robot.GetComponent<ChangeMaterial>().changeBody = true;
        robot.SetActive(false);
        cup.SetActive(false);
        straw.SetActive(false);
        battery.SetActive(false);
        batteryBK.SetActive(false);
        planeGame.instance.MiniGameEnd("juice");
    }
}