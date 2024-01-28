using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinDrop : MonoBehaviour
{
    public float dropSpeed = 0.5f;
    private bool disabled = false;

    void Update()
    {
        transform.position += new Vector3(0f, - dropSpeed * Time.deltaTime, 0f);
        if(transform.position.y < 3 & !disabled)
        {
            disabled = true;
            CoinGame.instance.total_count++;
        }
    }
}
