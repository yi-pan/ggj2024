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
        if (transform.localPosition.x == 580)
        {
            GlassesGame.instance.Miss();
        }
        if (Input.GetKeyDown("space"))
        {
            //Debug.Log(transform.localPosition);
            if (transform.localPosition.x > -170 & transform.localPosition.x < -30)
            {
                gameObject.SetActive(false);
                GlassesGame.instance.Hit();
            }
        }
    }
}
