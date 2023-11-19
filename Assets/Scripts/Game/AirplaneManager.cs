using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneManager : MonoBehaviour
{
    [Header("Самолёты")]
    [SerializeField] private GameObject _airplane;
    [SerializeField] private int _prize;

    [Header("Спавн")]
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    private List<Vector2> _allCorners = new List<Vector2>();

    private void Start()
    {
        _allCorners.Add(Camera.main.ViewportToWorldPoint(new Vector2(-0.2f, 0.5f)));//Лево
        _allCorners.Add(Camera.main.ViewportToWorldPoint(new Vector2(1.2f, 0.5f)));//Право
        _allCorners.Add(Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1.2f)));//Верх
        _allCorners.Add(Camera.main.ViewportToWorldPoint(new Vector2(0.5f, -0.2f)));//Низ

        StartCoroutine(SpawnAirplane());
    }

    public void IncreasePrize(int amount)
    {
        _prize += amount;    
    }

    IEnumerator SpawnAirplane()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

            //Выбираем место спавна и поворачиваем к центру
            Vector3 spawnPosition = _allCorners[Random.Range(0, _allCorners.Count)];
            Vector3 destination = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f)));
            Vector3 delta = destination - spawnPosition;

            float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90;

            Debug.Log(angle);

            //Спавним самолёт и задаём приз
            Airplane airplane = Instantiate(_airplane, spawnPosition, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<Airplane>();
            airplane.SetPrize(_prize);
        }
    }
}
