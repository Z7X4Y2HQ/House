using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacter : MonoBehaviour
{
    public GameObject Takahashi_Summer_home;
    public GameObject Takahashi_Summer_school;
    private Animator animator;

    void Awake()
    {
        animator = GameObject.Find("LevelLoader").GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (playerInWardrobeRange.changeClothes)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (playerInWardrobeRange.currentCloths == "Takahashi_Summer_school")
                {
                    Debug.Log("Same Cloths");
                }
                else
                {
                    StartCoroutine(SwapCloths(Takahashi_Summer_school));
                }
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                if (playerInWardrobeRange.currentCloths == "Takahashi_Summer_home")
                {
                    Debug.Log("Same Cloths");
                }
                else
                {
                    StartCoroutine(SwapCloths(Takahashi_Summer_home));
                }
            }
        }
    }

    IEnumerator SwapCloths(GameObject character)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1.4f);
        Swap(character);
        animator.Play("Crossfade End");
    }

    public void Swap(GameObject character)
    {
        Vector3 position = this.gameObject.transform.position;
        Quaternion rotation = this.gameObject.transform.rotation;

        Destroy(this.gameObject);
        Instantiate(character, position, rotation);
    }
}
