using UnityEngine;

public class EnemyDestroyerRunner : MonoBehaviour
{
    private EnemyDestroyerService _service;

    public void Construct(EnemyDestroyerService service)
    {
        _service = service;
    }

    private void Update()
    {
        if (_service != null)
            _service.Tick();
    }

    private void DestroyEnemy(Enemy enemy)
    {
        if (enemy != null)
            Destroy(enemy.gameObject);
    }
}