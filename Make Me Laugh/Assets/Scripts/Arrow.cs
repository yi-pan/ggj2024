using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    //public bool inverseKeyCode = false;


    // Start is called before the first frame update
    void Start()
    {
        if(this.name == "arrowL")
        {
            this.keyToPress = KeyCode.LeftArrow;
        }
        if (this.name == "arrowR")
        {
            this.keyToPress = KeyCode.RightArrow;
        }
        if (this.name == "arrowUp")
        {
            this.keyToPress = KeyCode.UpArrow;
        }
        if (this.name == "arrowDown")
        {
            this.keyToPress = KeyCode.DownArrow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RhythmGame.instance.inverseKeyCode)
        {
            if (this.name == "arrowL")
            {
                this.keyToPress = KeyCode.RightArrow;
            }
            if (this.name == "arrowR")
            {
                this.keyToPress = KeyCode.LeftArrow;
            }
            if (this.name == "arrowUp")
            {
                this.keyToPress = KeyCode.DownArrow;
            }
            if (this.name == "arrowDown")
            {
                this.keyToPress = KeyCode.UpArrow;
            }
        }
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                canBePressed = false;
                gameObject.SetActive(false);
                RhythmGame.instance.ArrowHit();
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "RhythmBtn")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "RhythmBtn" & canBePressed)
        {
            canBePressed = false;
            RhythmGame.instance.ArrowMiss();
            
        }
    }
}
