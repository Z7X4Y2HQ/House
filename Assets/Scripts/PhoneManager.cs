using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    private HandleProgress handleProgress;
    public Animator phoneUI;
    private Animator Player;
    public bool phoneOut = false;
    public bool phoneOutFirstTime = false;

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
                if (GameObject.FindWithTag("Takahashi_Summer_home") != null)
                {
                    Player = GameObject.FindWithTag("Takahashi_Summer_home").GetComponent<Animator>();
                }
                else if (GameObject.FindWithTag("Takahashi_Summer_school") != null)
                {
                    Player = GameObject.FindWithTag("Takahashi_Summer_school").GetComponent<Animator>();
                }
                takeOutPhone();
            }
            else if (phoneOut && Input.GetKeyDown(KeyCode.Tab))
            {
                putBackPhone();
            }
        }
    }

    public void takeOutPhone()
    {
        phoneOut = true;
        Debug.Log("phone out =  true");
        Player.Play("Taking out phone");
        phoneUI.Play("Phone slide up");
        StartCoroutine(phoneAnimation());
    }

    public void putBackPhone()
    {
        phoneOut = false;
        phoneOutFirstTime = true;
        Debug.Log("phone out =  false");
        Player.Play("Putting phone back");
        phoneUI.Play("Phone slide down");
    }

    private IEnumerator phoneAnimation()
    {
        yield return new WaitForSeconds(3.1f);
        if (phoneOut)
        {
            Player.Play("Texting");
        }
    }
}
