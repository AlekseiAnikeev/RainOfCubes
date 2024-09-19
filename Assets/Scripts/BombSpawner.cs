using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Start()
    {
        _cubeSpawner.CubeDeactivated += OnActivate;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeDeactivated -= OnActivate;
    }

    private void OnActivate(Vector3 position)
    {
        Bomb bomb = GetObject();
        bomb.transform.position = position;
        bomb.Init(RemoveToPool);
    }
}