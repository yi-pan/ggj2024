using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ObjectSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    private int success_count = 0;
    private int click_num = 0;
    private int miss_num = 0;
    private float scale1 = 0.8f;
    private float scale2 = 0.6f;
    private float scale3 = 0.4f;
    private float scale4 = 0.3f;
    private bool gameover = true;
    private float time = 0.0f;
    private bool door_open = false;
    private float changePerSecond;
    private float elevFloor = 1;

    public AudioSource audioSource;
    public AudioClip[] audioClipArray;
    public GameObject hand;
    public GameObject canvas;
    public GameObject briefcase;
    public GameObject ipad;
    public GameObject bottle;
    public GameObject bottle1;
    public GameObject[] needToHide;
    public GameObject itemsOnFloor;
    public GameObject firstFloor;
    public GameObject sixFloor;
    public GameObject door;
    public GameObject UICanvas;
    public GameObject elevPanel;
    public GameObject timeline;
    public GameObject timeline2;
    public GameObject child;

    public GameObject[] npcs;

    [SerializeField]
    private Camera cctv_cam;
    [SerializeField]
    private Camera game_cam;
    [SerializeField]
    private TMP_Text _click;
    [SerializeField]
    private TMP_Text _miss;
    [SerializeField]
    private TMP_Text _timer;
    [SerializeField]
    private TMP_Text _success;
    [SerializeField]
    private TMP_Text _elevScreen;

    AudioClip RandomClip()
    {
        return audioClipArray[Random.Range(0, audioClipArray.Length)];
    }

    void Start()
    {
        cctv_cam.enabled = true;
        game_cam.enabled = false;

        firstFloor.SetActive(true);
        sixFloor.SetActive(false);

        itemsOnFloor.SetActive(false);
        hand.SetActive(false);
        canvas.SetActive(false);

        _elevScreen.text = "1"; // elev screen text
        changePerSecond = 1; // floor number change speed

        // here to play cutscene
        intoElevator();
    }

    void intoElevator()
    {
        // after playing the initial cutscene, game start
        timeline.GetComponent<TimelinePlayer>().StartTimeline();
        // gameStart();
    }

    public void gameStart()
    {
        // activate game cam
        game_cam.enabled = true;
        cctv_cam.enabled = false;
        
        // reset time and score
        time = 0;
        click_num = 0;
        miss_num = 0;
        gameover = false;

        hand.SetActive(true);
        canvas.SetActive(true);

        // hide those between game panel and cam
        foreach(GameObject g in needToHide)
        {
            g.SetActive(false);
        }
    }

    void gameEnd()
    {
        // play animation to activate 
        elevPanel.GetComponent<Animator>().SetBool("play", true);
        child.GetComponent<Animator>().SetBool("isExcited", true);
        firstFloor.SetActive(false);
        sixFloor.SetActive(true);

        hand.SetActive(false);
        canvas.SetActive(false);
    }

    void itemsDrop()
    {
        foreach (Transform item in itemsOnFloor.transform)
        {
            item.GetComponent<Rigidbody>().useGravity = false;
        }
        itemsOnFloor.SetActive(true);
        if (!itemsOnFloor.GetComponent<AnimationEvent>().animFinished)
        {
            itemsOnFloor.GetComponent<Animator>().SetBool("play", true);
        }
    }

    void changeElevScreen()
    {
        _elevScreen.text = ""+ Mathf.RoundToInt(elevFloor);
    }

    void npcCry()
    {
        UICanvas.GetComponent<uiController>().showEmoji = true;
        foreach(GameObject npc in npcs)
        {
            npc.GetComponent<NpcController>().Cry();
        }
    }

    void Update()
    {
        // after playing the initial cutscene, game start
        if (Input.GetKeyDown("space"))
        {
            gameStart();
        }

        // after elevator panel animation finished
        if (elevPanel.GetComponent<AnimationEvent>().animFinished) {
            // switch camera and activate hidden objects 
            game_cam.enabled = false;
            cctv_cam.enabled = true;
            foreach (GameObject g in needToHide)
            {
                g.SetActive(true);
            }
            // items flying and dropping
            itemsDrop();
            // start to change elevator screen
            changeElevScreen();

            // elevator number grow 
            if (Mathf.CeilToInt(elevFloor) <= 6)
            {
                elevFloor = elevFloor + changePerSecond * Time.deltaTime;
            }

            // people crying
            npcCry();
        }

        // if itemDrop is finished, activate gravity
        if (itemsOnFloor.GetComponent<AnimationEvent>().animFinished)
        {
            foreach (Transform item in itemsOnFloor.transform)
            {
                item.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        // if elevator floor number reach 6, and door is closed, open the door
        if (Mathf.CeilToInt(elevFloor) > 6 & !door_open)
        {
            door.GetComponent<Animation>().Play("door_open");
            door_open = true;
            timeline2.GetComponent<TimelinePlayer>().StartTimeline();
        }


        //if (Input.GetKeyDown("space"))
        //{
        //    gameStart();
        //}
        
        // end of the game
        if (success_count == 9)
        {
            // play a success visual effect???
            _success.gameObject.SetActive(true);
            gameover = true;
            gameEnd();
        }
        
        // timer
        if (!gameover)
        {
            time += Time.deltaTime;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            if (seconds > 9)
            {
                _timer.text = "0" + minutes + ":" + seconds;
            }
            else
            {
                _timer.text = "0" + minutes + ":0" + seconds;
            }
        }


        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        // raycast
        Ray ray = game_cam.ScreenPointToRay(Input.mousePosition);
        // hover
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable"))
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                }
            }
            else
            {
                highlight = null;
            }
        }

        // Select
        if (Input.GetMouseButtonDown(0) && gameover == false) 
        {
            click_num++;
            _click.text = "Total Click: " + click_num;
            if (click_num == 5 | click_num == 70 | click_num == 150)
            {
                ipad.GetComponent<Animation>().Play("ipad03");
            }
            if (click_num == 15 | click_num == 45 | click_num == 75 | click_num == 125)
            {
                bottle1.GetComponent<Animation>().Play("bottle04");
            }
            if (click_num == 85 | click_num == 105 | click_num == 135 | click_num == 155)
            {
                bottle1.GetComponent<Animation>().Play("bottle01");
                ipad.GetComponent<Animation>().Play("ipad01");
            }
            if (click_num == 10 | click_num == 40 | click_num == 80 | click_num == 130)
            {
                briefcase.GetComponent<Animation>().Play("case01");
                bottle.GetComponent<Animation>().Play("bottle04");
            }
            if (click_num == 20 | click_num == 90 | click_num == 140)
            {
                briefcase.GetComponent<Animation>().Play("case02");
                bottle.GetComponent<Animation>().Play("bottle01");
            }
            if (click_num == 30 | click_num == 120)
            {
                ipad.GetComponent<Animation>().Play("ipad01");
                bottle.GetComponent<Animation>().Play("bottle03");
            }
            if (click_num == 50 | click_num == 110)
            {
                briefcase.GetComponent<Animation>().Play("case02");
            }
            if (click_num == 60 | click_num == 100)
            {
                bottle1.GetComponent<Animation>().Play("bottle04");
                bottle.GetComponent<Animation>().Play("bottle02");
            }
            if (highlight)
            {
                audioSource.PlayOneShot(RandomClip());
                selection = raycastHit.transform;
                //print(selection.transform.localScale.x);
                if (selection.transform.localScale.x == scale1)
                {
                    selection.transform.localScale = new Vector3(scale2, scale2, scale2);
                }
                else if(selection.transform.localScale.x == scale2)
                {
                    selection.transform.localScale = new Vector3(scale3, scale3, scale3);
                }
                else if (selection.transform.localScale.x == scale3)
                {
                    selection.transform.localScale = new Vector3(scale4, scale4, scale4);
                }
                else
                {
                    //print(selection.transform.parent.gameObject);
                    selection.transform.parent.gameObject.SetActive(false);
                    success_count++;
                }
                //print(selection);
                //if (selection.name == "hand1")
                //{
                //    print("click 1");
                //}
                //else if (selection.name == "hand2")
                //{
                //    print("click 2");
                //}
                //else if (selection.name == "hand3")
                //{
                //    print("click 3");
                //}
                //else if (selection.name == "hand4")
                //{
                //    print("click 4");
                //}
                //else if (selection.name == "hand5")
                //{
                //    print("click 5");
                //}
                //else if (selection.name == "hand6")
                //{
                //    print("click 6");
                //}
                //else if (selection.name == "hand7")
                //{
                //    print("click 7");
                //}
                //else if (selection.name == "hand8")
                //{
                //    print("click 8");
                //}
                //else
                //{
                //    print("click 9");
                //}
            }
            else
            {
                miss_num++;
                _miss.text = "Miss: " + miss_num;
            }
        }
    }
}
