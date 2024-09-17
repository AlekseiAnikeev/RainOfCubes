using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<T> _pool;

    private int _countObjectCreate;
    private int _countObjectSpawn;

    public event Action<int, int, int> IsCreate;

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
        _countObjectSpawn++;

        IsCreate?.Invoke(_countObjectSpawn, _countObjectCreate, _pool.CountActive);

        return _pool.Get();
    }

    protected virtual void RemoveToPool(T obj)
    {
        _pool.Release(obj);
    }

    private T Create()
    {
        T obj = Instantiate(_prefab);

        _countObjectCreate++;

        return obj;
    }
}