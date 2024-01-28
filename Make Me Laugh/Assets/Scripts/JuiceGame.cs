using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JuiceGame : MonoBehaviour
{
    public GameObject juice;
    public GameObject cup;
    public GameObject battery;
    public GameObject robot;

    private int clickCount = 0;
    //private int totalCount = 20;
    private Vector3 juiceScale;
    private RaycastHit raycastHit;
    private bool isPlaying;

    public Camera game_cam;

    public Material orange;
    public Material green;

    public void gameStart()
    {
        robot.SetActive(true);
        isPlaying = true;
        juiceScale = juice.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
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
                    cup.transform.localScale = new Vector3(18f, 18f, 18f);
                    if (Input.GetMouseButtonDown(0))
                    {
                        clickCount++;
                        juice.transform.localScale = juice.transform.localScale - new Vector3(0, 0.05f, 0);
                        battery.transform.localScale = battery.transform.localScale + new Vector3(0.008f, 0, 0);
                        battery.transform.localPosition = battery.transform.localPosition + new Vector3(-0.005f, 0, 0);
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
                    cup.transform.localScale = new Vector3(17f, 17f, 17f);
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
        robot.SetActive(false);
        cup.SetActive(false);
        planeGame.instance.MiniGameEnd("juice");
    }
}