using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    [SerializeField] private float _timeToFreeze;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
            StartCoroutine(Freeze());
        else if (collision.CompareTag("Bottom"))
        {
            BlocksMinigame.Instance.FinishMinigame();
            Destroy(gameObject);
        }
            
    }

    IEnumerator Freeze()
    {
        yield return new WaitForSeconds(_timeToFreeze);
        enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        BlocksMinigame.Instance.AddTickets();
    }
}
