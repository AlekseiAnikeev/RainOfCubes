using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;

    private int _minLifetime = 2;
    private int _maxLifeTime = 6;

    private float _minCoordinateValue = -5f;
    private float _maxCoordinateValue = 5f;

    private void OnEnable() => _cube.Contact += Contact;

    private void OnDisable()
    {
        _cube.Contact -= Contact;

        StopCoroutine(nameof(LifeRoutine));
    }

    private void Contact()
    {
        StartCoroutine(nameof(LifeRoutine));

        CreateCube();
    }

    private void CreateCube()
    {
        Instantiate(_cube, GetPosition(), Quaternion.identity);
    }

    private Vector3 GetPosition()
    {
        float coordinatX = Random.Range(_minCoordinateValue, _maxCoordinateValue);
        float coordinatZ = Random.Range(_minCoordinateValue, _maxCoordinateValue);
        float coordinatY = 6;

        return new Vector3(coordinatX, coordinatY, coordinatZ);
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSecondsRealtime(Random.Range(_minLifetime, _maxLifeTime));

        Destroy(gameObject);
    }
}