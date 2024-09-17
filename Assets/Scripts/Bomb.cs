using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private readonly int _minLifetime = 2;
    private readonly int _maxLifeTime = 6;

    [SerializeField] private float _explosionRadius = 20;
    [SerializeField] private float _explosionForce = 200;

    private Renderer _renderer;
    private Coroutine _coroutine;
    private Action<Bomb> _contact;

    private float _lifeTime;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(Action<Bomb> contact)
    {
        _renderer.material.color = new(0, 0, 0);
        _lifeTime = UnityEngine.Random.Range(_minLifetime, _maxLifeTime);
        _contact = contact;

        Invoke(nameof(RemoveToPool), _lifeTime);

        StopAllCoroutines();
        _coroutine = StartCoroutine(Detonation());
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObgect())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObgect()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> units = new();

        units.AddRange(hits.Where(hit => hit.attachedRigidbody != null).Select(hit => hit.attachedRigidbody));

        return units;
    }

    private IEnumerator Detonation()
    {
        float maxAlpha = 1f;
        float minAlpha = 0f;

        for (float i = 0; i < maxAlpha; i += Time.deltaTime / _lifeTime)
        {
            Color color = _renderer.material.color;
            color.a = Mathf.Lerp(maxAlpha, minAlpha, i);

            _renderer.material.color = color;

            yield return null;
        }

        Explode();
    }

    private void RemoveToPool()
    {
        _contact(this);
    }
}