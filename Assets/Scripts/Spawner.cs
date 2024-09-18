using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<T> _pool;
    private SpawnerCountInfo _countInfo;

    public event Action<SpawnerCountInfo> Created;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: Create,
            actionOnGet: prefab => prefab.gameObject.SetActive(true),
            actionOnRelease: prefab => prefab.gameObject.SetActive(false),
            actionOnDestroy: prefab => Destroy(prefab.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );

        _countInfo = gameObject.AddComponent<SpawnerCountInfo>();
    }

    protected T GetObject()
    {
        _countInfo.SetCountActive(_pool.CountActive);
        _countInfo.SetTotalСreated();

        Created?.Invoke(_countInfo);

        return _pool.Get();
    }

    protected virtual void RemoveToPool(T obj)
    {
        _pool.Release(obj);
    }

    private T Create()
    {
        _countInfo.SetNumberNewOnes();

        T obj = Instantiate(_prefab);

        return obj;
    }
}