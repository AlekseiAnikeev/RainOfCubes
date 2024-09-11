using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private int _spawnAmount = 20;
    [SerializeField] private float _repeatRate = 3f;
    
    public event Action<Vector3> CubeDeactivated;

    private readonly float _minCoordinateValue = -5f;
    private readonly float _maxCoordinateValue = 5f;
    
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, _repeatRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            var cube = GetObject();
            cube.transform.position = GetPosition();
            cube.SetStartColor();
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
}