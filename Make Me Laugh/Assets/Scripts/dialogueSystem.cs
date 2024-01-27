using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class dialogueSystem : MonoBehaviour
{
    public TextAsset textFile;     // drop your file here in inspector
    string[] dialogue;
    GameObject main_character;
    GameObject cry_kid;
    GameObject happy_kid;
    GameObject speaking_character;
    TMP_Text textContent;
    public GameObject textDisplay;
    //public handGame handGame;
    int dialogue_index = 0;
    string[] speaker_and_line = new string[0];
    public GameObject handGame;

    //public PlayableDirector timeline;


    void Start()
    {
        //looking for sprites
        main_character = GameObject.Find("character");
        main_character.SetActive(false);
        //Debug.Log(mainCharacter);
        cry_kid = GameObject.Find("cry_kid");
        cry_kid.SetActive(false);
        happy_kid = GameObject.Find("happy_kid");
        happy_kid.SetActive(false);
        speaking_character = main_character;

        //find the dialogue
        //textDisplay = GameObject.Find("text");
        textContent = textDisplay.GetComponent<TMP_Text>();
        //Debug.Log(textDisplay);
        Debug.Log(textContent);

        string text = textFile.text;  //this is the content as string
        dialogue = text.Split("\n");

        general_speak(dialogue[dialogue_index]);

    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(dialogue[dialogue_index]);
        if (Input.GetKeyUp(KeyCode.A) && (dialogue_index < dialogue.Length)) {
            general_speak(dialogue[dialogue_index]);
        } else if (Input.GetKeyUp(KeyCode.A) && dialogue_index >= dialogue.Length)
        {
            handGame.GetComponent<ObjectSelection>().gameStart();
            //timeline.Play();
            Debug.Log("dialogue finish");

            gameObject.SetActive(false);
        }
        

    }

    //function for when person talking
    void person_speak(string name, string dialogue)
    {
        if (string.Equals(name, "character")) speaking_character = main_character;
        else if (string.Equals(name, "happy_kid")) speaking_character = happy_kid;
        else if (string.Equals(name, "cry_kid")) speaking_character = cry_kid;
        else speaking_character = main_character;

        speaking_character.SetActive(true);
        textContent.text = speaker_and_line[1];
        Debug.Log(speaker_and_line[1]);

    }

    void general_speak(string current_sentence)
    {
        speaking_character.SetActive(false);
        speaker_and_line = current_sentence.Split(":");
        if (speaker_and_line.Length > 1) person_speak(speaker_and_line[0], speaker_and_line[1]);
        else textContent.text = speaker_and_line[0];
        dialogue_index++;
    }
}
