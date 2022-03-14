using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialog : MonoBehaviour
{
    private GameObject DialogUI;
    // Start is called before the first frame update
    void Start()
    {
        DialogUI = transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
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
