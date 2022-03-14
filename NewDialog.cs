using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialog : MonoBehaviour
{
    private GameObject DialogUI;  //告示牌物体
    
    void Start()
    {
        DialogUI = transform.GetChild(0).GetChild(0).gameObject; //找到告示牌下的物体
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            DialogUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DialogUI.SetActive(false);
        }
    }
}
