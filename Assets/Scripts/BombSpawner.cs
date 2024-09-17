using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Start()
    {
        _cubeSpawner.CubeDeactivated += Activation;
    }

    private void Activation(Vector3 position)
    {
        Bomb bomb = GetObject();
        bomb.transform.position = position;
        bomb.Init(RemoveToPool);
    }
}