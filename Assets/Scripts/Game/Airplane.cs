using System.Collections;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    [SerializeField] private GameObject _popup;
    [SerializeField] private GameObject _effect;
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody2D;
    private TicketWallet _wallet;
    private int _prize;

    private void Start()
    {
        _wallet = TicketWallet.Instance;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.up * _speed;
    }

    public void SetPrize(int prize)
    {
        _prize = prize;
    }

    private void OnMouseDown()
    {
        Instantiate(_effect, transform.position, Quaternion.identity);
        GameObject popup = Instantiate(_popup, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        popup.GetComponentInChildren<PopUp>().SetValue(_prize);
        _wallet.AddTicket(_prize);

        Destroy(gameObject);
    }
}
