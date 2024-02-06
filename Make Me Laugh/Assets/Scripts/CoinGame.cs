using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinGame : MonoBehaviour
{
    public bool isPlaying = false;
    public static CoinGame instance;
    [SerializeField]
    private TMP_Text _hit;

    public int hit_count;
    public AudioSource coinAudio;
    public int total_count = 0;
    public GameObject robot;
    public GameObject robot_green;

    private bool animPlayed = false;

    public void gameStart()
    {
        isPlaying = true;
    }

    public void gameEnd()
    {
        robot.SetActive(false);
        planeGame.instance.MiniGameEnd("coin");
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            _hit.text = "Hit: " + hit_count;
            if (total_count == 20)
            {
                robot.GetComponent<Animation>().Play("coin_after");
                robot_green.GetComponent<Animation>().Play("coin_after");
                animPlayed = true;
                isPlaying = false;
            }
        }
        if (animPlayed)
        {
            if (!robot.GetComponent<Animation>().isPlaying | !robot_green.GetComponent<Animation>().isPlaying)
            {
                gameEnd();
            }
        }
    }


    public void Hit()
    {
        hit_count++;
        coinAudio.Play();
    }
}
