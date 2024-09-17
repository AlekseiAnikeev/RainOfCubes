using TMPro;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private Spawner<Cube> _cubeSpawner;
    [SerializeField] private Spawner<Bomb> _bombSpawner;
    [SerializeField] private TextMeshProUGUI _textCubeCreate;
    [SerializeField] private TextMeshProUGUI _textCubeSpawn;
    [SerializeField] private TextMeshProUGUI _textCubeActive;
    [SerializeField] private TextMeshProUGUI _textBombCreate;
    [SerializeField] private TextMeshProUGUI _textBombSpawn;
    [SerializeField] private TextMeshProUGUI _textBombActive;

    private void OnEnable()
    {
        _cubeSpawner.IsCreate += DrawCubeStatistic;
        _bombSpawner.IsCreate += DrawBombStatistic;
    }

    private void OnDisable()
    {
        _cubeSpawner.IsCreate -= DrawCubeStatistic;
        _bombSpawner.IsCreate -= DrawBombStatistic;
    }

    private void DrawCubeStatistic(int totalСreated, int numberCreate, int numberActive)
    {
        _textCubeCreate.text = $"Создано новых: {numberCreate}";
        _textCubeSpawn.text = $"Всего создано: {totalСreated}";
        _textCubeActive.text = $"Активно: {numberActive}";
    }

    private void DrawBombStatistic(int totalСreated, int numberCreate, int numberActive)
    {
        _textBombCreate.text = $"Создано новых: {numberCreate}";
        _textBombSpawn.text = $"Всего создано: {totalСreated}";
        _textBombActive.text = $"Активно: {numberActive}";
    }
}