using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmButtonController : MonoBehaviour
{
    private Image image;
    private float pressedScale = 0.8f;
    public Color defaultColor;
    public Color pressedColor;
    public KeyCode keyToPress;


    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.color = defaultColor;
        if (this.name == "btn01")
        {
            this.keyToPress = KeyCode.LeftArrow;
        }
        if (this.name == "btn02")
        {
            this.keyToPress = KeyCode.UpArrow;
        }
        if (this.name == "btn03")
        {
            this.keyToPress = KeyCode.DownArrow;
        }
        if (this.name == "btn04")
        {
            this.keyToPress = KeyCode.RightArrow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RhythmGame.instance.inverseKeyCode)
        {
            if (this.name == "btn01")
            {
                this.keyToPress = KeyCode.RightArrow;
            }
            if (this.name == "btn02")
            {
                this.keyToPress = KeyCode.DownArrow;
            }
            if (this.name == "btn03")
            {
                this.keyToPress = KeyCode.UpArrow;
            }
            if (this.name == "btn04")
            {
                this.keyToPress = KeyCode.LeftArrow;
            }
        }
               
        if (Input.GetKeyDown(keyToPress))
        {
            image.color = pressedColor;
            image.transform.localScale = new Vector3(pressedScale, pressedScale, pressedScale);
        }
        if(Input.GetKeyUp(keyToPress)) 
        {
            image.color = defaultColor;
            image.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
