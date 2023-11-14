using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseHoverMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private float duration = 0.15f;
    private float targetScaleUp = 1.2f;
    private float targetScaleDown = 1f;
    private Animator animator;
    public static bool onHover = false;
    public static string lastHoverButton;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHover = true;
        lastHoverButton = gameObject.name;
        animator.SetBool("isHover", true);
        StartCoroutine(ScaleUp());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onHover = false;
        animator.SetBool("isHover", false);
        StartCoroutine(ScaleDown());
    }

    IEnumerator ScaleUp()
    {
        float startScale = transform.localScale.x;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float currentScale = Mathf.Lerp(startScale, targetScaleUp, elapsed / duration);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(targetScaleUp, targetScaleUp, targetScaleUp);

    }

    IEnumerator ScaleDown()
    {
        float startScale = transform.localScale.x;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float currentScale = Mathf.Lerp(startScale, targetScaleDown, elapsed / duration);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(targetScaleDown, targetScaleDown, targetScaleDown);
    }
}