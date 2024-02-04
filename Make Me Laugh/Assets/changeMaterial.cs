using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class changeMaterial : MonoBehaviour
{
    public GameObject body;
    public GameObject light;
    public GameObject arm;

    public Material body_material;
    public Material light_material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            ChangeBody();
        }
        if (Input.GetKeyDown("2"))
        {
            ChangeLight();
        }
        if(Input.GetKeyDown("3"))
        {
            ChangeArm();
        }
    }

    public void ChangeBody()
    {
        body.GetComponent<Renderer>().sharedMaterial = body_material;
    }

    public void ChangeLight()
    {
        light.GetComponent<Renderer>().sharedMaterial = light_material;
    }
    public void ChangeArm()
    {
       
        arm.transform.localRotation = Quaternion.Euler(180f, 0, 0);
        arm.transform.localPosition = arm.transform.localPosition + new Vector3(0, 0.017f, 0);
    }
}
