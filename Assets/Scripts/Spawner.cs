using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private int _spawnAmount = 20;
    [SerializeField] private float _repeatRate = 3f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private float _minCoordinateValue = -5f;
    private float _maxCoordinateValue = 5f;

    private Color defaulColor = new(0, 0, 0);

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
            actionOnGet: cube => cube.gameObject.SetActive(true),
            actionOnRelease: cube => cube.gameObject.SetActive(false),
            actionOnDestroy: cube => Destroy(cube.gameObject),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, _repeatRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            var cube = _pool.Get();
            cube.transform.position = GetPosition();
            cube.SetColor(defaulColor);
            cube.Init(RemoveToPool);
        }
    }

    private Vector3 GetPosition()
    {
        float coordinatX = Random.Range(_minCoordinateValue, _maxCoordinateValue);
        float coordinatZ = Random.Range(_minCoordinateValue, _maxCoordinateValue);
        float coordinatY = 6;

        return new Vector3(coordinatX, coordinatY, coordinatZ);
    }

    private void RemoveToPool(Cube cube)
    {
        _pool.Release(cube);
    }

    private Color CreateRandomColor => new(Random.value, Random.value, Random.value);
}
