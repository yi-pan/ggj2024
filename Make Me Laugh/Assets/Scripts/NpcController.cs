using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    Animator npcAnim;
    AudioSource crying;
    public bool sitBool = false;

    private void Awake()
    {
        npcAnim = GetComponent<Animator>();
        crying = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        npcAnim.SetBool("isCrying", false);
    }

    public void Cry()
    {
        if (!crying.isPlaying)
        {
            crying.Play();
        }
        npcAnim.SetBool("isCrying", true);
    }

    public void StopCry()
    {
        if (crying.isPlaying)
        {
            crying.Stop();
        }
        npcAnim.SetBool("isCrying", false);
    }


    public void Sit()
    {
        npcAnim.SetBool("isSitting", true);
    }

    public void Stand()
    {
        npcAnim.SetBool("isSitting", false);
    }

    // Update is called once per frame
    void Update()
    {
        npcAnim.SetBool("isSitting", sitBool);
    }
}
