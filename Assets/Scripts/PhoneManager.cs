using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    private HandleProgress handleProgress;
    public Animator phoneUI;
    private Animator Player;
    private bool phoneOut = false;

    private void Awake()
    {
        handleProgress = GameObject.Find("HandleHUD").GetComponent<HandleProgress>();
    }

    private void Update()
    {
        if (handleProgress.pickedUpPhone)
        {
            if (!phoneOut && Input.GetKeyDown(KeyCode.Tab))
            {
                phoneOut = true;
                Debug.Log("phone out =  true");
                phoneUI.Play("Phone slide up");
            }
            else if (phoneOut && Input.GetKeyDown(KeyCode.Tab))
            {
                phoneOut = false;
                Debug.Log("phone out =  false");
                phoneUI.Play("Phone slide down");
            }
        }
    }
}
