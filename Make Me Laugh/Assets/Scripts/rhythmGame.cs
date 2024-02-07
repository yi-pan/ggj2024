using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RhythmGame : MonoBehaviour
{
    public GameObject game_hint;
    public GameObject gameUI;
    public GameObject ui_hint_icon;
    public Sprite ui2;

    public AudioSource music;
    public bool startPlaying;
    public static RhythmGame instance;

    public AudioSource[] sounds;
    public AudioSource bass;
    public GameObject arrows;
    public GameObject arrows2;
    public GameObject gameCanvas;
    public GameObject[] btns;
    public GameObject[] npcs;
    public GameObject timeline;
    public GameObject timeline2;
    public GameObject[] cryEmoji;

    [SerializeField]
    private Camera cutscene_cam;
    [SerializeField]
    private Camera game_cam;

    [SerializeField]
    private TMP_Text _hit;
    [SerializeField]
    private TMP_Text _miss;

    public bool inverseKeyCode = false;
    private int hit_count = 0;
    private int miss_count = 0;
    private ArrowDrop arrowDrop1;
    private ArrowDrop arrowDrop2;

    private int missHintCount = 0;
    private int hitHintCount = 0;


    public GameObject teach_dialogue;

    // Start is called before the first frame update
    void Start()
    {
        game_hint.SetActive(false);
        gameUI.SetActive(false);
        // PLAY CUTSCENE 01
        intoSubway();

        //cutscene_cam.enabled = true;
        //game_cam.enabled = false;
        instance = this;

        startPlaying = false;
        arrows.SetActive(false);
        gameCanvas.SetActive(false);
        // ArrowDrop
        arrowDrop1 = arrows.GetComponent<ArrowDrop>();
        arrowDrop2 = arrows2.GetComponent<ArrowDrop>();
        arrowDrop1.gameOver = true;
        arrowDrop2.gameOver = true;
    }

    void intoSubway()
    {
        // play the first cutscene
        timeline.GetComponent<TimelinePlayer>().StartTimeline();
    }

    void outSubway()
    {
        // play the second cutscene
        timeline.GetComponent<TimelinePlayer>().StopTimeline();
        timeline2.GetComponent<TimelinePlayer>().StartTimeline();
    }

    public void gameStart()
    {
        gameUI.SetActive(true);
        //cutscene_cam.enabled = false;
        //game_cam.enabled = true;
        inverseKeyCode = false;
        arrows.SetActive(true);
        arrows2.SetActive(false);
        gameCanvas.SetActive(true);
        arrowDrop1.gameOver = false;
        startPlaying = true;
        music.Play();
    }

    public void gameRestart()
    {
        // reset button size and color
        foreach(GameObject btn in btns)
        {
            btn.GetComponent<RhythmButtonController>().Reset();
        }
        // slow down the game
        arrowDrop2.unit = 100;
        arrowDrop2.gameOver = false;
        arrowDrop1.gameOver = true;
        if (music.isPlaying)
        {
            music.Stop();
        }
        if (bass.isPlaying)
        {
            bass.Stop();
        }
        arrows.SetActive(false);
        hit_count = 0;
        miss_count = 0;
        arrows2.SetActive(true);
        music.Play();
    }

    void gameEnd()
    {
        //foreach(GameObject npc in npcs)
        //{
        //    npc.GetComponent<NpcController>().StopCry();
        //}
        gameUI.SetActive(false);
        startPlaying = false;
        if (bass.isPlaying)
        {
            bass.Stop();
        }
        if (music.isPlaying)
        {
            music.Stop();
        }
        gameCanvas.SetActive(false);
        foreach(GameObject e in cryEmoji)
        {
            e.SetActive(false);
        }
        // CUTSCENE 2 START
        outSubway();
    }

    // Update is called once per frame
    void Update()
    {
        // Game Start
        //if (!startPlaying)
        //{
        //    if (Input.GetKeyDown("space"))
        //    {
        //        gameStart();
        //    }
        //}

        // track hit and miss
        _hit.text = "Good: " + hit_count;
        _miss.text = "Miss: " + miss_count;

        

        // change pace
        if (inverseKeyCode)
        {
            if(hit_count == 10)
            {
                arrowDrop2.unit = 140;
            }
            if(hit_count == 20)
            {
                arrowDrop2.unit = 160;
            }
            if(hit_count == 30)
            {
                arrowDrop2.unit = 180;
            }
            if(hit_count == 50)
            {
                arrowDrop2.unit = 200;
            }

            if (missHintCount > 5)
            {
                game_hint.SetActive(true);
            }

            if(hitHintCount > 5 & game_hint)
            {
                game_hint.SetActive(false);
            }
            // make more npc cry gradually
            if (hit_count + miss_count == 11)
            {
                npcs[0].GetComponent<NpcController>().Cry();
                cryEmoji[0].SetActive(true);
            }
            if (hit_count + miss_count == 20)
            {
                npcs[1].transform.localPosition = npcs[1].transform.localPosition - new Vector3(0.003f, 0, 0);
                npcs[1].GetComponent<NpcController>().Cry();
                cryEmoji[1].SetActive(true);
            }
            if (hit_count + miss_count == 30)
            {
                npcs[2].transform.localPosition = npcs[2].transform.localPosition - new Vector3(0.006f, 0, 0);
                npcs[3].transform.localPosition = npcs[3].transform.localPosition - new Vector3(0.004f, 0, 0);
                npcs[2].GetComponent<NpcController>().Cry();
                npcs[3].GetComponent<NpcController>().Cry();
                cryEmoji[2].SetActive(true);
                cryEmoji[3].SetActive(true);
            }
            if (hit_count + miss_count == 40)
            {
                npcs[4].transform.localPosition = npcs[4].transform.localPosition - new Vector3(0.002f, 0, 0);
                npcs[4].GetComponent<NpcController>().Cry();
                cryEmoji[4].SetActive(true);
                npcs[5].GetComponent<NpcController>().Cry();
            }
            if (hit_count + miss_count == 50)
            {
                npcs[6].GetComponent<NpcController>().Cry();
                npcs[7].GetComponent<NpcController>().Cry();
            }
            if (hit_count + miss_count == 60)
            {
                npcs[8].GetComponent<NpcController>().Cry();
                npcs[9].GetComponent<NpcController>().Cry();
            }
        }

        // if key code is not inversed. left is left, right is right
        if (!inverseKeyCode)
        {
            // now inverse the key code and restart the game
            // HERE IS WHERE CHILD DIALOGUE GO; DIALOGUE FINISH -> INVERSE KEY
            if (hit_count == 10 | (hit_count + miss_count == 60))
            {
                inverseKeyCode = true;
                // dialogue
                arrowDrop1.unit = 0;
                teach_dialogue.SetActive(true);
                // bass stop
                bass.Stop();
                // arrow ui change
                ui_hint_icon.GetComponent<Image>().overrideSprite = ui2;
            }
        }


        // Game Over
        if (hit_count + miss_count == 60 && startPlaying)
        {
            gameEnd();
            // PLAY NEXT CUTSCENE
        }
    }

    // arrow hit
    public void ArrowHit()
    {
        hitHintCount++;
        missHintCount = 0;
        hit_count++;
        // if key code is inversed, play a stupid sound effect
        if (inverseKeyCode)
        {
            int randNum = Random.Range(0, sounds.Length);
            sounds[randNum].Play();
        }
        else
        {
            // play bass sound
            if (!bass.isPlaying)
            {
                bass.Play();
            }
        }
    }

    // arrow miss
    public void ArrowMiss()
    {
        hitHintCount = 0;
        missHintCount++;
        miss_count++;
        // if key code is not inversed, pause the bass if miss
        if (!inverseKeyCode)
        {
            if (bass != null)
            {
                if (bass.isPlaying)
                {
                    bass.Pause();
                }
            }
        }
    }
}
