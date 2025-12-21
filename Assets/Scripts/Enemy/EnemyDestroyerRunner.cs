using UnityEngine;

public class EnemyDestroyerRunner : MonoBehaviour
{
    public EnemyDestroyerService Service { get; private set; }

    private void Awake()
    {
        Service = new EnemyDestroyerService(DestroyEnemy);
    }

    private void Update()
    {
        Service.Tick();
    }

    private void DestroyEnemy(Enemy enemy)
    {
        if (enemy != null)
            Destroy(enemy.gameObject);
    }
}