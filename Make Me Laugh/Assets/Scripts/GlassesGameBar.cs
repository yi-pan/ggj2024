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
        if (Input.GetKeyDown("space"))
        {
            if (canBePressed)
            {
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!");
                gameObject.SetActive(false);
                GlassesGame.instance.Hit();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        canBePressed = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("out collision");
        canBePressed = false;
        GlassesGame.instance.Miss();
    }
}
