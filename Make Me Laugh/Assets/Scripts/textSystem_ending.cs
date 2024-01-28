using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textSystem_ending : MonoBehaviour
{
    public TextAsset textFile;     // drop your file here in inspector
    string[] text_dialogue;
    public GameObject friend_bubble;
    public GameObject main_bubble;
    GameObject[] text_history;
    public float spawn_friend_x;
    public float spawn_friend_y;

    public float spawn_main_x;
    public float spawn_main_y;

    int current_text_index;

    //public GameObject GameManager;
    //public string nextScene;

    void Start()
    {
        //find the dialogue
        //textDisplay = GameObject.Find("text");
        //textContent = textDisplay.GetComponent<TMP_Text>();
        //Debug.Log(textDisplay);
        //Debug.Log(textContent);

        string text = textFile.text;  //this is the content as string
        text_dialogue = text.Split("\n");
        text_history = new GameObject[text_dialogue.Length];

        nextText();
        //Debug.Log(text_history);

        /*
        for (int i = 0; i < text_dialogue.Length; i++)
        {
            GameObject new_bubble = SpawnBubble(text_dialogue[i].Split(":")[0], text_dialogue[i].Split(":")[1]);
            text_history[i] = new_bubble;
            if (i > 0) text_history[i - 1].transform.position = new Vector3(text_history[i - 1].transform.position.x, text_history[i - 1].transform.position.y, 0);
        }
        */

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Mouse0) && current_text_index < text_dialogue.Length)
        {
            nextText();
        } 

    }

    GameObject SpawnBubble(string character, string thought)
    {
        GameObject newSpawn;
        
        if (string.Equals(character, "character"))
        {
            newSpawn = Instantiate(main_bubble, new Vector3(0, 0, 0), Quaternion.identity);
            newSpawn.transform.SetParent(gameObject.transform);
            newSpawn.transform.localPosition = new Vector3(spawn_main_x, spawn_main_y, 0);

            //newSpawn.transform.Rotate(0, 0, 20);
            Debug.Log(newSpawn.transform.position);
        }
        else
        {
            newSpawn = Instantiate(friend_bubble, new Vector3(0, 0, 0), Quaternion.identity);
            newSpawn.transform.SetParent(gameObject.transform);
            newSpawn.transform.localPosition = new Vector3(spawn_friend_x, spawn_friend_y, 0);
            //newSpawn.transform.Rotate(0, 0, 20);

        }
        //newSpawn.transform.SetParent(gameObject.transform);

        newSpawn.transform.localScale = new Vector3(2, 2, 2);

        TMP_Text text = newSpawn.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        text.text = thought;
        return newSpawn;
    }

    void nextText()
    {
        GameObject new_bubble = SpawnBubble(text_dialogue[current_text_index].Split(":")[0], text_dialogue[current_text_index].Split(":")[1]);
        text_history[current_text_index] = new_bubble;
        Debug.Log(new_bubble.transform.position);
        if (current_text_index > 0)
        {
            for (int j = current_text_index; j > 0; j--)
            {
                text_history[j - 1].transform.localPosition = new Vector3(text_history[j - 1].transform.localPosition.x, text_history[j - 1].transform.localPosition.y + 300, 0);
                
            }
            if (current_text_index > 3)
            {
                text_history[current_text_index - 4].SetActive(false);
            }
        }
        current_text_index++;
    }
}
