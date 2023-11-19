using System.Collections;
using UnityEngine;

public class UITransitions : MonoBehaviour
{
    //Для красивых переходов

    internal static UITransitions Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(CanvasGroup group)
    {
        StartCoroutine(ShowPanel(group));
    }

    public void Hide(CanvasGroup group)
    {
        StartCoroutine(HidePanel(group));
    }

    IEnumerator ShowPanel(CanvasGroup group)
    {
        for(float i = 0; i <= 0.5f; i += Time.deltaTime)
        {
            group.alpha = i / 0.5f;

            yield return new WaitForEndOfFrame();
        }

        group.alpha = 1;
        group.interactable = true;
    }

    IEnumerator HidePanel(CanvasGroup group)
    {
        group.interactable = false;
        for (float i = 0.5f; i > 0; i -= Time.deltaTime)
        {
            group.alpha = i / 0.5f;

            yield return new WaitForEndOfFrame();
        }

        group.gameObject.SetActive(false);
    }
}
