using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiController : MonoBehaviour
{
    public bool showEmoji = false;
    public bool showDialogue = false;
    public bool showThought = false;

    public GameObject dialogue;
    public GameObject emoji;
    public GameObject thought;

    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(false);
        emoji.SetActive(false);
        thought.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (showDialogue)
        {
            dialogue.SetActive(true);
        }
        else
        {
            dialogue.SetActive(false);
        }

        if (showEmoji)
        {
            emoji.SetActive(true);
        }
        else
        {
            emoji.SetActive(false);
        }

        if (showThought)
        {
            thought.SetActive(true);
        }
        else
        {
            thought.SetActive(false);
        }
    }
}
