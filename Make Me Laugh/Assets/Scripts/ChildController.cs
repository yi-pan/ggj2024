using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childController : MonoBehaviour
{
    Animator childAnim;
    AudioSource childAudio;

    // Start is called before the first frame update
    void Start()
    {
        childAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
