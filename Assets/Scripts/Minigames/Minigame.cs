using UnityEngine;
using TMPro;
using System.Collections;

public abstract class Minigame : MonoBehaviour{
	[Header("Общие параметры миниигры")]
	//Билеты
	[SerializeField] protected TMP_Text _ticketsPrizeText;
	[SerializeField] protected int _ticketsPrize;
    [HideInInspector] protected int _collectedTickets = 0;

	//Эффект окончания миниигры
	[SerializeField] protected GameObject _winEffect;

	//Задняя панелька
	[SerializeField] private CanvasGroup _panelGroup;

	//Попап
	[SerializeField] private GameObject _popup;

	private Vector2 _bottom;
	[Space]

	//Таймер
	[SerializeField] protected TMP_Text _timer;
	[SerializeField] protected int _minigameTime;

	private Animator _animator;
	
	public void Awake(){
		_bottom = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0f));
		_animator = GetComponent<Animator>();
	}

	public void AddTickets(){
		_collectedTickets += _ticketsPrize;
		_ticketsPrizeText.text = $"Собрано: {_collectedTickets}";
	}

	public virtual void FinishMinigame(){
		Instantiate(_winEffect, _bottom, Quaternion.identity);
		TicketWallet.Instance.AddTicket(_collectedTickets);
		_animator.enabled = true;
		_animator.SetTrigger("Hide");

		GameObject popup = Instantiate(_popup, Vector2.zero, Quaternion.identity);
		popup.GetComponentInChildren<PopUp>().SetValue(_collectedTickets);

        StartCoroutine(HideMinigame());
	}

	public virtual void StartGame()
	{
		_collectedTickets = 0;
        _ticketsPrizeText.text = $"Собрано: 0";

        _animator.SetTrigger("Show");
		StartCoroutine(DisableAnimator());
		StartCoroutine(CountTime());
	}

	IEnumerator HideMinigame()
	{
        UITransitions.Instance.Hide(_panelGroup);
        yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
	}

	IEnumerator DisableAnimator()
	{
		yield return new WaitForSeconds(0.5f);
		_animator.enabled = false;
	}

	IEnumerator CountTime(){
		for(int i = _minigameTime; i >= 1; i -= 1){
			_timer.text = $"Время: {i}";
			yield return new WaitForSeconds(1);
		}

		FinishMinigame();
	}
}