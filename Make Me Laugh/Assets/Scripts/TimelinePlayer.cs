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

    // Update is called once per frame
    void Update()
    {
        
    }
}
