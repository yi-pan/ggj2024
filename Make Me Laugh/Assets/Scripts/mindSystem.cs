using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mindSystem : MonoBehaviour
{
    public TextAsset textFile;     // drop your file here in inspector
    string[] mind_dialogue;
    public GameObject mind_bubble;
    //int dialogue_index = 0;
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;


    void Start()
    {
        //find the dialogue
        //textDisplay = GameObject.Find("text");
        //textContent = textDisplay.GetComponent<TMP_Text>();
        //Debug.Log(textDisplay);
        //Debug.Log(textContent);

        string text = textFile.text;  //this is the content as string
        mind_dialogue = text.Split("\n");

        for (int i = 0; i < mind_dialogue.Length; i++)
        {
            SpawnBubble(mind_dialogue[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        


    }

    void SpawnBubble(string thought)
    {
        float x = Random.Range(MinX, MaxX);
        float y = Random.Range(MinY, MaxY);

        GameObject newSpawn = Instantiate(mind_bubble, new Vector3(x, y, 0), Quaternion.identity);
        newSpawn.transform.parent = gameObject.transform;
        TMP_Text text = newSpawn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        text.text = thought;
    }
}
