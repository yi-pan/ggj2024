using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    Animator npcAnim;
    AudioSource crying;
    public GameObject elevPanel;

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

    // Update is called once per frame
    void Update()
    {
        if (elevPanel.GetComponent<AnimationEvent>().animFinished)
        {
            if (!crying.isPlaying)
            {
                crying.Play();
            }
            npcAnim.SetBool("isCrying", true);
        }
    }
}
