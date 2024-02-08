using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousePosition;
    public Camera game_cam;
    public AudioSource drag_audio;
    private Vector3 GetMousePos()
    {
        return game_cam.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
        mousePosition.y = 0;
        drag_audio.Play();
    }

    private void OnMouseDrag()
    {
        transform.position = game_cam.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }
}
