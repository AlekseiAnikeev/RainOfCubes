using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private int _spawnAmount = 1;
    [SerializeField] private float _repeatRate = 3f;

    private readonly float _minCoordinateValue = -5f;
    private readonly float _maxCoordinateValue = 5f;

    private Coroutine _coroutine;

    public event Action<Vector3> CubeDeactivated;

    private void Start()
    {
        _coroutine = StartCoroutine(Countdown(_repeatRate));
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            var cube = GetObject();
            cube.transform.position = GetPosition();
            cube.Init(RemoveToPool);
        }
    }

    private Vector3 GetPosition()
    {
        const float coordinateY = 6;

        float coordinateX = Random.Range(_minCoordinateValue, _maxCoordinateValue);
        float coordinateZ = Random.Range(_minCoordinateValue, _maxCoordinateValue);

        return new Vector3(coordinateX, coordinateY, coordinateZ);
    }

    protected override void RemoveToPool(Cube cube)
    {
        CubeDeactivated?.Invoke(cube.transform.position);

        base.RemoveToPool(cube);
    }

    private IEnumerator Countdown(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Spawn();

            yield return wait;
        }
    }
}