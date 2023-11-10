using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHoverMenuScript : MonoBehaviour
{
    private Animator animator;
    

    private void Update()
    {
        if (OnMouseHoverMenu.onHover)
        {
            animator = GetComponent<Animator>();
        

            if(gameObject.name == "PlayText")
            {
                
                animator.SetBool("isHoverPlay", true);
            }

          else if (gameObject.name == "ContinueText")
            {

                animator.SetBool("isHoverContinue", true);
            }

            else if(gameObject.name == "SettingText")
            {

                animator.SetBool("isHoverSetting", true);
            }

            else if (gameObject.name == "BookText")
            {

                animator.SetBool("isHoverBook", true);
            }

            else if(gameObject.name == "ExitText")
            {

                animator.SetBool("isHoverExit", true);
            }


        }
        else
        {
            animator = GetComponent<Animator>();
            animator.SetBool("isHoverPlay", false);
            animator.SetBool("isHoverContinue", false);
            animator.SetBool("isHoverSetting", false);
            animator.SetBool("isHoverBook", false);
            animator.SetBool("isHoverExit", false);

        }
    }
}
