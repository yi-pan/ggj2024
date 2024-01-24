using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeTest : MonoBehaviour
{
    public GameObject dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            dialogue.SetActive(true);
        }
    }
}
