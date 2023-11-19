using System.Collections;
using UnityEngine;
public class BusRoad : MonoBehaviour
{
    [Header("Дорота")]
    [SerializeField] private Transform[] _points;
    [SerializeField] private AnimationCurve _rideCurve;
    [SerializeField] private GameObject _popup;

    [Header("Автобусы")]
    [SerializeField] private GameObject _bus;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _rideTime;
    [SerializeField] private int _busCapacity;

    //Прочее
    private int _lastPointIndex;
    private TicketWallet _ticketWallet;

    private void Start()
    {
        _lastPointIndex = _points.Length - 1;
        _ticketWallet = TicketWallet.Instance;
    }

    private void OnEnable()
    {
        AddBus();
    }


    //Улучшения
    public void AddBus()
    {
        GameObject bus = Instantiate(_bus, _points[0].position, Quaternion.identity);
        bus.transform.parent = gameObject.transform;

        StartCoroutine(Ride(bus.transform));
    }

    public void IncreaseCapacity(int delta)
    {
        _busCapacity += delta;
    }

    public void IncreaseSpeed(float delta)
    {
        _rideTime -= delta;
    }

    //Поездка автобуса
    IEnumerator Ride(Transform bus)
    {
        int currentIndex = 1;
        int delta = 1;

        while (true)
        {
            //Передвижение до остановки
            Vector3 destination = _points[currentIndex].position;
            Vector3 lastPosition = bus.position;
            
            for(float i = 0; i < _rideTime; i += Time.deltaTime) 
            {
                bus.position = Vector2.Lerp(lastPosition, destination, _rideCurve.Evaluate(i / _rideTime));
                yield return new WaitForEndOfFrame();
            }

            //Создали попап и прибавили билетик
            GameObject popup = Instantiate(_popup, new Vector2(bus.position.x, bus.position.y), Quaternion.identity);
            popup.GetComponentInChildren<PopUp>().SetValue(_busCapacity);

            _ticketWallet.AddTicket(_busCapacity);

            //Подождали какое-то время и сменили следующую точку
            yield return new WaitForSeconds(_waitTime);

            if (currentIndex == _lastPointIndex || currentIndex == 0)
                delta *= -1;

            currentIndex += delta;
        }
    }
}
