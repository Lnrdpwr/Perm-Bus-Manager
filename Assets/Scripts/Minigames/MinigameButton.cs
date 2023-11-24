using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinigameButton : MonoBehaviour
{
    [Header("Покупка миниигры")]
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private int _price;

    [Header("Миниигра")]
    [SerializeField] private Minigame _minigame;
    [SerializeField] private GameObject _minigamePanel;
    [SerializeField] private float _cooldown;

    private TicketWallet _wallet;
    private Button _button;
    private Image _image;
    private bool _isOpen = false;
    private bool _reloaded = true;

    private void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _wallet = TicketWallet.Instance;
    }

    public void OnClick()
    {
        if (_isOpen && _reloaded)
        {
            _minigamePanel.SetActive(true);
            UITransitions.Instance.Show(_minigamePanel.GetComponent<CanvasGroup>());

            _minigame.gameObject.SetActive(true);
            _minigame.StartGame();

            StartCoroutine(Cooldown());
        }
        else if (!_isOpen)
            _buyPanel.SetActive(true);
    }

    public void BuyMinigame()
    {
        if (_wallet.CheckPrice(_price))
        {
            _wallet.Buy(_price);
            _isOpen = true;
            UITransitions.Instance.Hide(_buyPanel.GetComponent<CanvasGroup>());
        }
    }

    IEnumerator Cooldown()
    {
        _reloaded = false;
        _button.interactable = false;
        
        for(float i = 0; i < _cooldown; i += Time.deltaTime)
        {
            _image.fillAmount = i / _cooldown;
            yield return new WaitForEndOfFrame();
        }
        _image.fillAmount = 1;

        _reloaded = true;
        _button.interactable = true;
    }
}
