using System;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IColorable
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;
    [SerializeField] private TextMeshProUGUI _textObjectCreate;
    [SerializeField] private TextMeshProUGUI _textObjectSpawn;
    [SerializeField] private TextMeshProUGUI _textObjectActive;

    private ObjectPool<T> _pool;

    private int _countObjectCreate;
    private int _countObjectSpawn;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: Create,
            actionOnGet: prefab => prefab.gameObject.SetActive(true),
            actionOnRelease: prefab => prefab.gameObject.SetActive(false),
            actionOnDestroy: prefab => Destroy(prefab.gameObject),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Update()
    {
        _textObjectSpawn.text = $"Всего создано: {_countObjectSpawn}";
        _textObjectCreate.text = $"Создано новых: {_countObjectCreate}";
        _textObjectActive.text = $"Активно: {_pool.CountActive}";
    }

    protected T GetObject()
    {
        _countObjectSpawn++;


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