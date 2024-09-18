using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private readonly Color _defaultColor = new(0, 0, 255);

    private bool _isContact = true;

    private int _minLifetime = 2;
    private int _maxLifeTime = 6;

    private Coroutine _countdown;
    private Renderer _renderer;

    private Action<Cube> _contact;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnDestroy()
    {
        if (_countdown != null)
            StopCoroutine(_countdown);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
        {
            if (_isContact)
            {
                SetColor(CreateRandomColor);

                _isContact = false;
            }
            else
            {
                return;
            }

            _countdown = StartCoroutine(Countdown(UnityEngine.Random.Range(_minLifetime, _maxLifeTime)));
        }
    }

    public void Init(Action<Cube> contact)
    {
        _contact = contact;
        _renderer.material.color = _defaultColor;
    }

    private void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    private void RemoveToPool()
    {
        _isContact = true;

        _contact(this);
    }

    private IEnumerator Countdown(float delay)
    {
        yield return new WaitForSeconds(delay);

        RemoveToPool();
    }

    private Color CreateRandomColor =>
        new(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
}