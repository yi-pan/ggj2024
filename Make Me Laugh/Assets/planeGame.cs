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

    public GameObject timeline;
    public GameObject coin_table;
    public GameObject candy_table;
    public GameObject juice_table;
    public GameObject glasses_table;
    public bool startPlaying = false;

    public GameObject coin_game;
    public GameObject game_canvas;

    public Camera character_cam;
    public Camera game_cam;
    public Camera coin_cam;

    private Transform objToScale;
    private bool isScaledUp = false;
    private bool inGame = false;

    void wakeUp()
    {
        timeline.GetComponent<PlayableDirector>().Play();
    }

    public void gameStart()
    {
        startPlaying = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        game_cam.enabled = false;
        coin_cam.enabled = false;
        game_canvas.SetActive(false);
        coin_game.SetActive(false);
        character_cam.enabled = true;
        // play first cutscene
        wakeUp();
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

                if (highlight.CompareTag("Selectable"))
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
    }

    void collectTool(GameObject tool)
    {
        // deactivate object
        tool.SetActive(false);

        // show the object on the table
        if (tool.name == "juice")
        {
            juice_table.SetActive(true);
            Debug.Log("collected juice");
        }
        if(tool.name == "coin")
        {
            coin_table.SetActive(true);
            inGame = true;
            CoinGameStart();
            Debug.Log("collected coin");
        }
        if(tool.name == "candy")
        {
            candy_table.SetActive(true);
            Debug.Log("collected candy");
        }
        if(tool.name == "glasses")
        {
            glasses_table.SetActive(true);
            Debug.Log("collected glasses");
        }

        // switch to game cam  -> do game -> play animation
        switchCamera();
    }

    void CoinGameStart()
    {
        game_cam.enabled = false;
        character_cam.enabled = false;
        coin_cam.enabled = true;
        game_canvas.SetActive(true);
        coin_game.SetActive(true);
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
        float num = (float) (1 / 1.4);
        Vector3 temp = Vector3.Scale(obj.transform.localScale, new Vector3(num, num, num));
        obj.transform.localScale = temp;
    }
}
