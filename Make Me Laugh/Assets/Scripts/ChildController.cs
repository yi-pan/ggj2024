using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childController : MonoBehaviour
{
    Animator childAnim;
    AudioSource childAudio;
    public bool sitBool = false;
    public bool cryBool = false;
    public bool happyBool = false;

    private void Awake()
    {
        childAnim = GetComponent<Animator>();
    }

    void Start()
    {
        childAnim.SetBool("isCrying", false);
        childAnim.SetBool("isHappy", false);
    }

    public void Cry()
    {
        cryBool = true;
        childAnim.SetBool("isCrying", true);
    }

    public void StopCry()
    {
        childAnim.SetBool("isCrying", false);
    }


    public void Sit()
    {
        childAnim.SetBool("isSitting", true);
    }

    public void Stand()
    {
        childAnim.SetBool("isSitting", false);
    }

    public void Happy()
    {
        childAnim.SetBool("isHappy", true);
    }

    void Update()
    {
        childAnim.SetBool("isSitting", sitBool);
        childAnim.SetBool("isCrying", cryBool);
        childAnim.SetBool("isHappy", happyBool);
    }
}
