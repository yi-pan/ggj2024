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
    }

    // Update is called once per frame
    void Update()
    {
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
