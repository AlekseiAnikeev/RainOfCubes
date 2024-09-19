using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<T> _pool;

    private int _countTotalCreated;
    private int _countNumberNewOnes;

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
    }

    protected T GetObject()
    {
        _countTotalCreated++;

        Created?.Invoke(new SpawnerCountInfo(_countTotalCreated, _countNumberNewOnes, _pool.CountActive));

        return _pool.Get();
    }

    protected virtual void RemoveToPool(T obj)
    {
        Debug.Log(2);
        _pool.Release(obj);
    }

    private T Create()
    {
        Debug.Log(1);
        _countNumberNewOnes++;

        return Instantiate(_prefab);
    }
}