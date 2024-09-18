using TMPro;
using UnityEngine;

public abstract class SpawnerView<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Spawner<T> _objectSpawner;
    [SerializeField] private TextMeshProUGUI _textObjectCreate;
    [SerializeField] private TextMeshProUGUI _textObjectSpawn;
    [SerializeField] private TextMeshProUGUI _textObjectActive;

    private void OnEnable()
    {
        _objectSpawner.Created += DrawObjectStatistic;
    }

    private void OnDisable()
    {
        _objectSpawner.Created -= DrawObjectStatistic;
    }

    private void DrawObjectStatistic(SpawnerCountInfo countInfo)
    {
        _textObjectSpawn.text = $"Всего создано: {countInfo.TotalСreated}";
        _textObjectCreate.text = $"Создано новых: {countInfo.NumberNewOnes}";
        _textObjectActive.text = $"Активно: {countInfo.CountActive}";
    }
}