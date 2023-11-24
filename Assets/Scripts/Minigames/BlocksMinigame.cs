using UnityEngine;
using System.Collections;

public class BlocksMinigame : Minigame
{
    [Header("Блок")]
    [SerializeField] private GameObject _block;
    [SerializeField] private float _createTime;
    [SerializeField] private GameObject _effect;

    [Header("Крюк")]
    [SerializeField] private Transform _hook;
    [SerializeField] private Transform _grabJoint;
    [SerializeField] private AnimationCurve _dragCurve;
    [SerializeField] private float _dragTime;

    [Header("Крайние точки перемещения")]
    [SerializeField]  private Vector2 _leftCorner;
    [SerializeField]  private Vector2 _rightCorner;

    [Header("Триггер")]
    [SerializeField] private Collider2D _trigger;

    private GameObject _currentBlock;
    private bool _createdBlock = false;

    internal static BlocksMinigame Instance;

    new void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public override void StartGame()
    {
        if(_currentBlock != null)
            Destroy(_currentBlock);

        base.StartGame();
        _trigger.enabled = true;
        StartCoroutine(Drag());
        StartCoroutine(CreateBlocks());
        CreateBlock();
    }

    void CreateBlock()
    {
        Instantiate(_effect, _grabJoint.position, Quaternion.identity);
        _currentBlock = Instantiate(_block, _grabJoint);
        _createdBlock = true;
    }

    public override void FinishMinigame()
    {
        StopAllCoroutines();
        CancelInvoke();

        base.FinishMinigame();

        _trigger.enabled = false;
        Instantiate(_effect, _grabJoint.position, Quaternion.identity);
    }

    IEnumerator CreateBlocks()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (Input.GetMouseButton(0) && _createdBlock)
            {
                _createdBlock = false;
                _currentBlock.transform.parent = null;
                _currentBlock.GetComponent<Rigidbody2D>().isKinematic = false;
                AddTickets();

                Invoke("CreateBlock", _createTime);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Drag()
    {
        yield return new WaitForSeconds(0.5f);
        for(float i = _dragTime/2; i < _dragTime; i += Time.deltaTime)
        {
            _hook.position = Vector2.Lerp(_rightCorner, _leftCorner, _dragCurve.Evaluate(i / _dragTime));
            yield return new WaitForEndOfFrame();
        }

        while (true)
        {
            for(float i = 0; i < _dragTime; i += Time.deltaTime)
            {
                _hook.position = Vector2.Lerp(_leftCorner, _rightCorner, _dragCurve.Evaluate(i / _dragTime));
                yield return new WaitForEndOfFrame();
            }

            for (float i = 0; i < _dragTime; i += Time.deltaTime)
            {
                _hook.position = Vector2.Lerp(_rightCorner, _leftCorner, _dragCurve.Evaluate(i / _dragTime));
                yield return new WaitForEndOfFrame();
            }
        }
    }
}