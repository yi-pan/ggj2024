using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class planeGame : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    private bool animPlayed = false;

    public static planeGame instance;
    public GameObject timeline;
    public GameObject coin_table;
    public GameObject candy_table;
    public GameObject juice_table;
    public GameObject glasses_table;
    public bool startPlaying = false;

    public GameObject coin_game;
    public GameObject juice_game;
    public GameObject candy_game;
    public GameObject glasses_game;

    public GameObject candy_green;

    public GameObject game_canvas;
    public GameObject score_canvas;

    public GameObject npc_coin;
    public GameObject npc_juice;
    public GameObject npc_candy;
    public GameObject npc_glasses;

    public Camera character_cam;
    public Camera game_cam;
    public Camera coin_cam;

    public GameObject dialogue_end;
    public GameObject coin_teach;
    public GameObject juice_teach;
    public GameObject glasses_teach;
    public GameObject candy_teach;

    // object and animation
    public GameObject coin;
    public GameObject juice;
    public GameObject glasses;
    public GameObject candy;

    public GameObject robot_sep;
    public GameObject robot_fnished;

    private Transform objToScale;
    private bool isScaledUp = false;
    private bool inGame = false;

    private int miniGameCount = 0;

    private bool coinGameStart = false;
    private bool glassesGameStart = false;
    private bool candyGameStart = false;
    private bool juiceGameStart = false;
    private bool selectable = true;

    void wakeUp()
    {
        timeline.GetComponent<PlayableDirector>().Play();
    }

    public void gameStart()
    {
        startPlaying = true;
        inGame = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        game_cam.enabled = false;
        coin_cam.enabled = false;
        game_canvas.SetActive(false);
        score_canvas.SetActive(false);
        coin_game.SetActive(false);
        juice_game.SetActive(false);
        candy_game.SetActive(false);
        glasses_game.SetActive(false);
        character_cam.enabled = true;
        robot_fnished.SetActive(false);

        // play first cutscene
        wakeUp();
        //gameStart();
    }

    void switchCamera()
    {
        if (character_cam.enabled)
        {
            character_cam.enabled = false;
            game_cam.enabled = true;
        }
        else
        {
            character_cam.enabled = true;
            game_cam.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startPlaying & !inGame)
        {
            // switch camera
            if (Input.GetKeyDown("space"))
            {
                switchCamera();
            }

            // hightlight
            if (highlight != null)
            {
                highlight.gameObject.GetComponent<Outline>().enabled = false;
                highlight = null;
            }
            // raycast
            Ray ray = character_cam.ScreenPointToRay(Input.mousePosition);
            // hover
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
            {
                highlight = raycastHit.transform;

                if (highlight.CompareTag("Selectable") & selectable)
                {
                    // Debug.Log(highlight.name);
                    objToScale = highlight;

                    if (highlight.gameObject.GetComponent<Outline>() != null)
                    {
                        highlight.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        Outline outline = highlight.gameObject.AddComponent<Outline>();
                        outline.enabled = true;
                    }
                    if (highlight.gameObject.GetComponent<Outline>().scaleUp)
                    {
                        highlight.gameObject.GetComponent<Outline>().scaleUp = false;
                        if (!isScaledUp)
                        {
                            scaleUp(objToScale);
                        }
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        collectTool(highlight.gameObject);
                    }
                }
                else
                {
                    if (isScaledUp)
                    {
                        scaleDown(objToScale);
                    }
                    highlight = null;
                }
            }
        }

        // 
        if (coinGameStart)
        {
            if (!coin.GetComponent<Animation>().isPlaying)
            {
                coin_teach.SetActive(true);
                inGame = true;
                coinGameStart = false;
            }
        }
        if (glassesGameStart)
        {
            if (!glasses.GetComponent<Animation>().isPlaying)
            {
                glasses_teach.SetActive(true);
                inGame = true;
                glassesGameStart = false;
            }
        }
        if (candyGameStart)
        {
            if (!candy.GetComponent<Animation>().isPlaying)
            {
                candy_teach.SetActive(true);
                inGame = true;
                candyGameStart = false;
            }
        }
        if (juiceGameStart)
        {
            if (!juice.GetComponent<Animation>().isPlaying)
            {
                juice_teach.SetActive(true);
                inGame = true;
                juiceGameStart = false;
            }
        }

        if (animPlayed)
        {
            if (!robot_fnished.GetComponent<Animation>().isPlaying)
            {
                character_cam.enabled = true;
                game_cam.enabled = false;
                dialogue_end.SetActive(true);
                npc_candy.GetComponent<AudioSource>().volume = 0.4f;
                npc_coin.GetComponent<AudioSource>().volume = 0.4f;
                npc_glasses.GetComponent<AudioSource>().volume = 0.4f;
                npc_juice.GetComponent<AudioSource>().volume = 0.4f;
            }
        }
    }



    void collectTool(GameObject tool)
    {
        // deactivate object
        //tool.SetActive(false);
        tool.tag = "Untagged";

        if (tool.name == "coin")
        {
            selectable = false;
            npc_coin.GetComponent<NpcController>().Cry();
            coin.GetComponent<Animation>().Play("coins_before");
            //Debug.Log("collected coin. Animation Here");
            coinGameStart = true;
        }

        if (tool.name == "juice")
        {
            selectable = false;
            npc_juice.GetComponent<NpcController>().Cry();
            npc_juice.transform.localPosition = npc_juice.transform.localPosition - new Vector3(0, 0, 0.8f);
            juice.GetComponent<Animation>().Play("juice_before");
            //Debug.Log("collected juice. Animation Here");
            juiceGameStart = true;
        }


        if (tool.name == "candy")
        {
            selectable = false;
            npc_candy.GetComponent<NpcController>().Cry();
            npc_candy.transform.localPosition = npc_candy.transform.localPosition - new Vector3(0, 0, 0.8f);
            candy.GetComponent<Animation>().Play("candy_before");
            //Debug.Log("collected candy. Animation Here");
            candyGameStart = true;
        }

        if (tool.name == "glasses")
        {
            selectable = false;
            npc_glasses.GetComponent<NpcController>().Cry();
            npc_glasses.transform.localPosition = npc_glasses.transform.localPosition - new Vector3(0, 0, 0.8f);
            glasses.GetComponent<Animation>().Play("glasses_before");
            //Debug.Log("collected glasses. Animation Here");
            glassesGameStart = true;
        }
    }

    public void GlassesGameStart()
    {
        game_cam.enabled = true;
        character_cam.enabled = false;
        glasses_game.SetActive(true);
        glasses_game.GetComponent<GlassesGame>().gameStart();
    }

    public void CandyGameStart()
    {
        game_cam.enabled = true;
        character_cam.enabled = false;
        candy_game.SetActive(true);
        candy_game.GetComponent<CandyGame>().gameStart();
    }

    public void CoinGameStart()
    {
        game_cam.enabled = false;
        character_cam.enabled = false;
        coin_cam.enabled = true;
        game_canvas.SetActive(true);
        score_canvas.SetActive(true);
        coin_game.SetActive(true);
        coin_game.GetComponent<CoinGame>().gameStart();
    }

   public void JuiceGameStart()
    {
        game_cam.enabled = true;
        character_cam.enabled = false;
        juice_game.SetActive(true);
        juice_game.GetComponent<JuiceGame>().gameStart();
    }

    public void MiniGameEnd(string tool)
    {
        selectable = true;
        inGame = false;
        miniGameCount++;
        if (miniGameCount < 4)
        {
            game_cam.enabled = false;
            character_cam.enabled = true;
        }
        else
        {
            robot_sep.SetActive(false);
            robot_fnished.SetActive(true);
            animPlayed = true;
            candy_green.SetActive(false);
            robot_fnished.GetComponent<Animation>().Play("fnished_robot_dance");

            character_cam.enabled = false;
            game_cam.enabled = true;
            npc_candy.GetComponent<AudioSource>().volume = 0.4f;
            npc_coin.GetComponent<AudioSource>().volume = 0.4f;
            npc_glasses.GetComponent<AudioSource>().volume = 0.4f;
            npc_juice.GetComponent<AudioSource>().volume = 0.4f;
        }
        if (tool == "coin")
        {
            Debug.Log("play coin ending animation");
            coin.SetActive(false);
            coin_cam.enabled = false;
            game_canvas.SetActive(false);
            score_canvas.SetActive(false);
            coin_game.SetActive(false);
        }
        if (tool == "glasses")
        {
            Debug.Log("play glasses ending animation");
            glasses.SetActive(false);
            glasses_game.SetActive(false);
        }
        if (tool == "juice")
        {
            juice.SetActive(false);
            Debug.Log("play juice ending animation");
        }
        if (tool == "candy")
        {
            candy.SetActive(false);
            Debug.Log("play candy ending animation");
        }

        //juice_game.SetActive(false);
    }

    void scaleUp(Transform obj)
    {
        isScaledUp = true;
        Vector3 temp = Vector3.Scale(obj.transform.localScale, new Vector3(1.4f, 1.4f, 1.4f));
        obj.transform.localScale = temp;
    }

    void scaleDown(Transform obj)
    {
        isScaledUp = false;
        float num = (float)(1 / 1.4);
        Vector3 temp = Vector3.Scale(obj.transform.localScale, new Vector3(num, num, num));
        obj.transform.localScale = temp;
    }
}