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
    float offset_x = 0;
    float offset_y = 0;
    float offset_increment_x;
    float offset_increment_y;


    void Start()
    {
        //find the dialogue
        //textDisplay = GameObject.Find("text");
        //textContent = textDisplay.GetComponent<TMP_Text>();
        //Debug.Log(textDisplay);
        //Debug.Log(textContent);

        string text = textFile.text;  //this is the content as string
        mind_dialogue = text.Split("\n");
        offset_increment_x = (MaxX - MinX) / (mind_dialogue.Length);
        Debug.Log(offset_increment_x);
        offset_increment_y = (MaxY - MinY) / mind_dialogue.Length;

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
        float x = Random.Range(MinX + offset_x, MinX + offset_x + offset_increment_x);
        float y = Random.Range(MinY, MaxY);

        GameObject newSpawn = Instantiate(mind_bubble, new Vector3(x, y, 0), Quaternion.identity);
        newSpawn.transform.SetParent(gameObject.transform);
        TMP_Text text = newSpawn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        text.text = thought;
        offset_x += offset_increment_x;
        offset_y += offset_increment_y;
    }
}
