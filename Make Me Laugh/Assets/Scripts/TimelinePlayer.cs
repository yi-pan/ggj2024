using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector director;
    
    // Start is called before the first frame update
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void hide_cursor()
    {
        Cursor.visible = false;
    }

    public void show_cursor()
    {
        Cursor.visible = true;
    }
    // start timeline
    public void StartTimeline()
    {
        director.Play();
    }

    public void StopTimeline()
    {
        //Debug.Log("cutscene end");
        director.Stop();
    }

    public void PauseAndDialogue(GameObject dialogue)
    {
        //Debug.Log("cutscene end");
        director.Pause();
        dialogue.SetActive(true);
    }

    public void SpawnMindBubble(GameObject mind_bubble_spawn)
    {
        mind_bubble_spawn.SetActive(true);
    }

    public void DisableBubble(GameObject mind)
    {
        mind.SetActive(false);
    }

    public void muteAudio(GameObject audio)
    {
        audio.SetActive(false);
        audio.GetComponent<AudioSource>().Stop();
        Debug.Log(audio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
