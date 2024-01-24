using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s")) 
        {
            animator.SetBool("isSitting", true);
            Vector3 temp = new Vector3(0, 0.5f, 1.1f);
            transform.position += temp;
        }
    }
}
