using TMPro;
using UnityEngine;

public class TicketWallet : MonoBehaviour
{
    //Кошелёк

    private TMP_Text _ticketCounter;
    private int _ticketsAmount = 0;

    internal static TicketWallet Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _ticketCounter = GetComponent<TMP_Text>();
    }

    public void AddTicket(int amount)
    {
        _ticketsAmount += amount;
        _ticketCounter.text = $"{_ticketsAmount}";  
    }

    //Списывание денег
    public void Buy(int price)
    {
        _ticketsAmount -= price;
        _ticketCounter.text = $"{_ticketsAmount}";
    }

    //Просто проверка цен
    public bool CheckPrice(int price)
    {
        if(price <= _ticketsAmount)
            return true;

        return false;
    }
}
