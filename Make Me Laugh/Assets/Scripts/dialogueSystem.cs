using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueSystem : MonoBehaviour
{
    public TextAsset textFile;     // drop your file here in inspector

    void Start()
    {
        string text = textFile.text;  //this is the content as string
        string[] dialogue = text.Split("\n");
        for (var i = 0; i < dialogue.Length; i++)
        {
            Debug.Log(dialogue[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
