using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public bool animFinished = false;

    public void AnimFinished()
    {
        animFinished = true;
        //Debug.Log("animation finished");
    }
}
