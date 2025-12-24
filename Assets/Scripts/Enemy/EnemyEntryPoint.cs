using UnityEngine;

public class EnemyEntryPoint : MonoBehaviour
{
    [SerializeField] private EnemyDestroyerRunner _runner;
    [SerializeField] private Example _example;

    private EnemyDestroyerService _service;

    private void Awake()
    {
        _service = new EnemyDestroyerService(DestroyEnemy);

        if (_runner != null)
            _runner.Construct(_service);

        if (_example != null)
            _example.Construct(_service);
    }

    private void DestroyEnemy(Enemy enemy)
    {
        if (enemy != null)
            Destroy(enemy.gameObject);
    }
}