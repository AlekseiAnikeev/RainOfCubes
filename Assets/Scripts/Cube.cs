using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private bool _isContact = true;

    public event Action Contact;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            if (_isContact)
            {
                Contact?.Invoke();

                _renderer.material.color = CreateRandomColor;

                _isContact = false;
            }
        }
    }

    private Color CreateRandomColor => new(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
}