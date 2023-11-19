using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoadUpgrades : MonoBehaviour
{
    // �� ��� ����� ���� �������� � ������, �� ��� ���� ���� + � ��� ��������
    // :(

    /*
        * Price - ��, ��� ������� � �����
        * Level - �������������� �������
        * Delta - �� ������� ���������� ��������
    */

    [Header("��������� �������")]
    [SerializeField] private BusRoad _road;

    [Header("��������� ����")]
    [SerializeField] private int _priceMultiplier;

    [Header("��������")]
    [SerializeField] private Button _speedButton;
    [SerializeField] private TMP_Text _speedPriceText;
    [SerializeField] private TMP_Text _speedLevelText;
    [SerializeField] private int _speedPrice;
    [SerializeField] private int _speedLevel = 1;
    [SerializeField] private float _speedDelta;

    [Header("�����������")]
    [SerializeField] private Button _capacityButton;
    [SerializeField] private TMP_Text _capacityPriceText;
    [SerializeField] private TMP_Text _capacityLevelText;
    [SerializeField] private int _capacityPrice;
    [SerializeField] private int _capacityLevel = 1;
    [SerializeField] private int _capacityDelta;

    [Header("��������")]
    [SerializeField] private Button _amountButton;
    [SerializeField] private TMP_Text _amountPriceText;
    [SerializeField] private TMP_Text _amountLevelText;
    [SerializeField] private int _amountPrice;
    [SerializeField] private int _amountLevel = 1;

    [Header("�������� ��������")]
    [SerializeField] private Button _unlockButton;
    [SerializeField] private GameObject _lockedPanel;
    [SerializeField] private GameObject _unlockedPanel;
    [SerializeField] private GameObject _lockedRoad;
    [SerializeField] private int _unlockPrice;

    private TicketWallet _wallet;

    private void Start()
    {
        //��� ���� ���� �������� ����� ������� � �����
        _capacityPriceText.text = $"{_capacityPrice}";
        _amountPriceText.text = $"{_amountPrice}";
        _speedPriceText.text = $"{_speedPrice}";
    }

    private void OnEnable()
    {
        _wallet = TicketWallet.Instance;
        StartCoroutine(CheckButtons());
    }

    //�������� ��������
    public void OpenRoad()
    {
        if (_wallet.CheckPrice(_unlockPrice))
        {
            _wallet.Buy(_unlockPrice);
            _lockedPanel.SetActive(false);
            _unlockedPanel.SetActive(true);
            _lockedRoad.SetActive(true);
        }
    }

    //��������
    public void UpgradeSpeed()
    {
        if (_wallet.CheckPrice(_speedPrice) && _speedLevel < 5)
        {
            _wallet.Buy(_speedPrice);
            _speedLevel++;
            _speedPrice *= _priceMultiplier;
            _road.IncreaseSpeed(_speedDelta);

            _speedPriceText.text = $"{_speedPrice}";
            _speedLevelText.text = $"{_speedLevel}";
            _speedButton.interactable = _wallet.CheckPrice(_speedPrice) ? true : false;
        }

        if (_speedLevel == 5)
            _speedPriceText.text = "����.";
    }

    //�����������
    public void UpgradeCapacity()
    {
        if (_wallet.CheckPrice(_capacityPrice) && _capacityLevel < 5)
        {
            _wallet.Buy(_capacityPrice);
            _capacityLevel++;
            _capacityPrice *= _priceMultiplier;
            _road.IncreaseCapacity(_capacityDelta);   

            _capacityPriceText.text = $"{_capacityPrice}";
            _capacityLevelText.text = $"{_capacityLevel}";
            _capacityButton.interactable = _wallet.CheckPrice(_capacityPrice) ? true : false;
        }

        if (_capacityLevel == 5)
            _capacityPriceText.text = "����.";
    }

    //���-�� ���������
    public void UpgradeAmount()
    {
        if(_wallet.CheckPrice(_amountPrice) && _amountLevel < 3)
        {
            _wallet.Buy(_amountPrice);
            _amountLevel++;
            _amountPrice *= _priceMultiplier;
            _road.AddBus();

            _amountPriceText.text = $"{_amountPrice}";
            _amountLevelText.text = $"{_amountLevel}";
            _amountButton.interactable = _wallet.CheckPrice(_amountPrice) ? true : false;
        }

        if (_amountLevel == 3)
            _amountPriceText.text = "����.";
    }

    //�������� �� ����������� �������
    IEnumerator CheckButtons()
    {
        while (true)
        {
            _speedButton.interactable = _wallet.CheckPrice(_speedPrice) & _speedLevel < 5 ? true : false;
            _capacityButton.interactable = _wallet.CheckPrice(_capacityPrice) & _capacityLevel < 5 ? true : false;
            _amountButton.interactable = _wallet.CheckPrice(_amountPrice) & _amountLevel < 3 ? true : false;
            _unlockButton.interactable = _wallet.CheckPrice(_unlockPrice) ? true : false;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
