using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHoverMenuScript : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(OnMouseHoverMenu.onHover);
        if (OnMouseHoverMenu.lastHoverButton == "PlayBtn")
        {
            Animator animator = GameObject.Find("PlayText").GetComponent<Animator>();
            if (OnMouseHoverMenu.onHover)
            {
                animator.SetBool("isHoverPlay", true);
            }
            else
            {
                Debug.Log("koi b false chl jae");
                animator.SetBool("isHoverPlay", false);
            }
        }
        else if (OnMouseHoverMenu.lastHoverButton == "ContinueBtn")
        {
            Animator animator = GameObject.Find("ContinueText").GetComponent<Animator>();
            if (OnMouseHoverMenu.onHover)
            {
                animator.SetBool("isHoverContinue", true);
            }
            else
            {
                Debug.Log("koi b false chl jae");
                animator.SetBool("isHoverContinue", false);
            }
        }
        else if (OnMouseHoverMenu.lastHoverButton == "SettingBtn")
        {
            Animator animator = GameObject.Find("SettingText").GetComponent<Animator>();
            if (OnMouseHoverMenu.onHover && !MenuScript.clickedOnSettings)
            {
                animator.SetBool("isHoverSetting", true);
            }
            else
            {
                Debug.Log("koi b false chl jae");
                animator.SetBool("isHoverSetting", false);
            }
        }
        else if (OnMouseHoverMenu.lastHoverButton == "BookBtn")
        {
            Animator animator = GameObject.Find("BookText").GetComponent<Animator>();
            if (OnMouseHoverMenu.onHover && !MenuScript.clickedOnChapters)
            {
                animator.SetBool("isHoverBook", true);
            }
            else
            {
                Debug.Log("koi b false chl jae");
                animator.SetBool("isHoverBook", false);
            }
        }
        else if (OnMouseHoverMenu.lastHoverButton == "ExitBtn")
        {
            Animator animator = GameObject.Find("ExitText").GetComponent<Animator>();
            if (OnMouseHoverMenu.onHover)
            {
                animator.SetBool("isHoverExit", true);
            }
            else
            {
                Debug.Log("koi b false chl jae");
                animator.SetBool("isHoverExit", false);
            }
        }
    }
}
