using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CandyGame : MonoBehaviour
{
    public Camera game_cam;

    private bool isPlaying;
    private RaycastHit raycastHit;

    
    public void gameStart()
    {
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            // raycast
            Ray ray = game_cam.ScreenPointToRay(Input.mousePosition);
            // hover
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
            {
                
            }
        }
    }
}
