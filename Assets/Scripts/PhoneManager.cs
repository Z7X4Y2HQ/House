using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PhoneManager : MonoBehaviour
{
    public Animator phoneUI;
    private Animator Player;
    public bool phoneOut = false;
    public bool phoneOutFirstTime = false;
    private LockMouse lockMouse;
    private void Awake()
    {
        lockMouse = GameObject.Find("LockMouse").GetComponent<LockMouse>();
    }

    private void Update()
    {
        if (HandleProgress.pickedUpPhone)
        {
            if (!phoneOut && Input.GetKeyDown(KeyCode.Tab) && !PauseMenuScript.gameIsPaused && !DialogueManager.dialogueIsPlaying && HandleTimeline.timeline.state != PlayState.Playing)
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
        lockMouse.Unlock();
        phoneOut = true;
        Debug.Log("phone out =  true");
        Player.Play("Taking out phone");
        phoneUI.Play("Phone slide up");
        StartCoroutine(phoneAnimation());
    }

    public void putBackPhone()
    {
        lockMouse.Lock();
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
