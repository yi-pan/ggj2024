using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesGameBar : MonoBehaviour
{
    private bool canBePressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.localPosition.x >= 580)
        {
            Debug.Log("miss");
            GlassesGame.instance.Miss();
        }
        if (Input.GetKeyDown("space"))
        {
            //Debug.Log(transform.localPosition);
            if (transform.localPosition.x > 60 & transform.localPosition.x < 260)
            {
                gameObject.SetActive(false);
                GlassesGame.instance.Hit();
            }
        }
    }
}
