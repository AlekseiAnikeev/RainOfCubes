using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour, IColorable
{
    private readonly Color _defaultColor = new(0, 0, 255);
    
    private bool _isContact = true;

    private int _minLifetime = 2;
    private int _maxLifeTime = 6;

    private Renderer _renderer;
    private Action<Cube> _contact;


    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(Action<Cube> contact)
    {
        _contact = contact;
    }

    public void SetStartColor()
    {
        _renderer.material.color = _defaultColor;
    }

    private void SetColor(Color color)
    {
        _renderer.material.color = color;
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

            Invoke(nameof(RemoveToPool), UnityEngine.Random.Range(_minLifetime, _maxLifeTime));
        }
    }

    private void RemoveToPool()
    {
        _isContact = true;

        _contact(this);
    }

    private Color CreateRandomColor =>
        new(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
}