using System.Collections;
using UnityEngine;

public class MinigameButton : MonoBehaviour
{
    [Header("Покупка миниигры")]
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private int _price;

    [Header("Миниигра")]
    [SerializeField] private Minigame _minigame;
    [SerializeField] private GameObject _minigamePanel;

    private TicketWallet _wallet;
    private bool _isOpen = false;

    private void Start()
    {
        _wallet = TicketWallet.Instance;
    }

    public void OnClick()
    {
        if (_isOpen)
        {
            _minigamePanel.SetActive(true);
            UITransitions.Instance.Show(_minigamePanel.GetComponent<CanvasGroup>());

            _minigame.gameObject.SetActive(true);
            _minigame.StartGame();
        }
        else
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
}
