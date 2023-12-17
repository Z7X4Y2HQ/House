using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHoverMenuScript : MonoBehaviour
{
    private void Update()
    {
        if (OnMouseHoverMenu.lastHoverButton == "PlayBtn")
        {
            Animator animator = GameObject.Find("PlayText").GetComponent<Animator>();
            if (OnMouseHoverMenu.onHover)
            {
                animator.SetBool("isHoverPlay", true);
            }
            else
            {
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
                animator.SetBool("isHoverContinue", false);
            }
        }
        else if (OnMouseHoverMenu.lastHoverButton == "SettingBtn")
        {
            Animator animator = GameObject.Find("SettingText").GetComponent<Animator>();
            if (OnMouseHoverMenu.onHover)
            {
                animator.SetBool("isHoverSetting", true);
            }
            else
            {
                animator.SetBool("isHoverSetting", false);
            }
        }
        else if (OnMouseHoverMenu.lastHoverButton == "BookBtn")
        {
            Animator animator = GameObject.Find("BookText").GetComponent<Animator>();
            if (OnMouseHoverMenu.onHover)
            {
                animator.SetBool("isHoverBook", true);
            }
            else
            {
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
                animator.SetBool("isHoverExit", false);
            }
        }
    }
}
