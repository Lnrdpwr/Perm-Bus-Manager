using System.Collections;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    //Красивенький текст
    [SerializeField] private float _fadeTime = 1;

    private TMP_Text _text;

    void Awake()
    {
        _text = GetComponent<TMP_Text>();
        StartCoroutine(Fade());
    }

    public void SetValue(int value)
    {
        _text.text = $"{value}";
    }

    IEnumerator Fade()
    {
        for(float i = _fadeTime; i > 0; i -= Time.deltaTime)
        {
            _text.alpha = i / _fadeTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
