using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private bool _isContact = true;

    private int _minLifetime = 2;
    private int _maxLifeTime = 6;

    private Renderer _renderer;
    private Action<Cube> _contact;

    public void Init(Action<Cube> conatact)
    {
        _contact = conatact;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (_isContact)
            {
                _renderer.material.color = CreateRandomColor;

                _isContact = false;
            }

            Invoke(nameof(RemoveToPool), UnityEngine.Random.Range(_minLifetime, _maxLifeTime));
        }
    }

    private void RemoveToPool()
    {
        _isContact = true;

        _contact(this);
    }

    private Color CreateRandomColor => new(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
}
