using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesGameBar : MonoBehaviour
{
    public GameObject[] barAreas;

    private bool canBePressed = false;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        barAreas[0].SetActive(true);
    }

    void Update()
    {
        if (transform.localPosition.x >= 580)
        {
            GlassesGame.instance.Miss();
        }
        if (Input.GetKeyDown("space"))
        {
            //Debug.Log(transform.localPosition);
            if (count == 0 & transform.localPosition.x > 60 & transform.localPosition.x < 260)
            {
                count++;
                barAreas[0].SetActive(false);
                barAreas[1].SetActive(true);
                GlassesGame.instance.Hit();
            }
            if (count == 1 & transform.localPosition.x > 300 & transform.localPosition.x < 500)
            {
                count++;
                barAreas[1].SetActive(false);
                barAreas[2].SetActive(true);
                GlassesGame.instance.Hit();
            }
            if (count == 2 & transform.localPosition.x > -433 & transform.localPosition.x < -233)
            {
                count++;
                barAreas[2].SetActive(false);
                GlassesGame.instance.Hit();
            }
        }
        if(count == 3)
        {
            gameObject.SetActive(false);
        }
    }
}
