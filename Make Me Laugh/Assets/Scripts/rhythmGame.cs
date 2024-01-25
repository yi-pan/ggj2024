using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RhythmGame : MonoBehaviour
{
    public AudioSource music;
    public bool startPlaying;
    public ArrowDrop arrowDrop;
    public static RhythmGame instance;
    public AudioSource[] sounds;
    public AudioSource bass;
    public GameObject arrows;

    [SerializeField]
    private TMP_Text _hit;
    [SerializeField]
    private TMP_Text _miss;

    private Vector3 arrowsPos;
    public bool inverseKeyCode = false;
    private int hit_count = 0;
    private int miss_count = 0;
    // Start is called before the first frame update
    void Start()
    {
        arrowsPos = arrows.transform.position;
        instance = this;
        arrowDrop.gameOver = true;
        startPlaying = false;
        arrows.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.GetKeyDown("space"))
            {
                arrows.SetActive(true);
                arrowDrop.gameOver = false;
                startPlaying = true;
                music.Play();
            }
        }
        _hit.text = "Good: " + hit_count;
        _miss.text = "Miss: " + miss_count;
        if (!inverseKeyCode)
        {
            // now inverse the key code, restart the game
            if (Input.GetKeyDown("1"))
            {
                inverseKeyCode = true;
                if (music.isPlaying)
                {
                    music.Stop();
                }
                if (bass.isPlaying)
                {
                    bass.Stop();
                }
                arrows.SetActive(false);
                foreach (Arrow arrow in arrows.GetComponentsInChildren<Arrow>())
                {
                    arrow.gameObject.SetActive(true);
                }
                
                // restart the game
                hit_count = 0;
                miss_count = 0;
                arrows.SetActive(true);

                arrows.transform.position = arrowsPos;
                music.Play();
            }
        }
        // game over
        if (hit_count + miss_count == 49)
        {
            startPlaying = false;
            if(bass.isPlaying)
            {
                bass.Stop();
            }
            if (music.isPlaying)
            {
                music.Stop();
            }
        }
    }

    public void ArrowHit()
    {
        hit_count++;
        if (inverseKeyCode)
        {
            int randNum = Random.Range(0, sounds.Length);
            sounds[randNum].Play();
        }
        else
        {
            if (!bass.isPlaying)
            {
                bass.Play();
            }
        }
        //Debug.Log("Hit");
    }

    public void ArrowMiss()
    {
        miss_count++;
        //Debug.Log("Miss");
        if (!inverseKeyCode)
        {
            if (bass.isPlaying)
            {
                bass.Pause();
            }
        }
    }
}
