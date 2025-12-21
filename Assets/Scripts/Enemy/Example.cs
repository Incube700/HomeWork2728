using System;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] private EnemyDestroyerRunner _runner;

    private readonly List<Enemy> _spawned = new List<Enemy>();

    private void Update()
    {
        // Z — враг умирает по логической смерти (IsDead)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Enemy enemy = SpawnEnemy();
            _spawned.Add(enemy);

            _runner.Service.Register(enemy, () => enemy.IsDead);

            Debug.Log("Spawned enemy with condition: IsDead");
        }

        // X — враг умирает через 3 секунды
        if (Input.GetKeyDown(KeyCode.X))
        {
            Enemy enemy = SpawnEnemy();
            _spawned.Add(enemy);

            float bornTime = Time.time;
            float lifeTime = 3f;

            _runner.Service.Register(enemy, () =>
                Time.time - bornTime >= lifeTime);

            Debug.Log("Spawned enemy with condition: Lifetime 3s");
        }

        // C — враг умирает, если врагов стало больше 5
        if (Input.GetKeyDown(KeyCode.C))
        {
            Enemy enemy = SpawnEnemy();
            _spawned.Add(enemy);

            int limit = 5;

            _runner.Service.Register(enemy, () =>
                _runner.Service.Count > limit);

            Debug.Log("Spawned enemy with condition: Count > " + limit);
        }

        // V — убить всех (IsDead = true)
        if (Input.GetKeyDown(KeyCode.V))
        {
            for (int i = 0; i < _spawned.Count; i++)
            {
                if (_spawned[i] != null)
                    _spawned[i].Kill();
            }

            Debug.Log("Killed all enemies (IsDead = true)");
        }
    }

    private Enemy SpawnEnemy()
    {
        Camera cam = Camera.main;

        Vector3 pos = cam.transform.position + cam.transform.forward * 6f;
        pos.y = 1f;

        // небольшой разброс, чтобы не спавнились друг в друге
        pos += new Vector3(
            UnityEngine.Random.Range(-2f, 2f),
            0f,
            UnityEngine.Random.Range(-1f, 1f)
        );

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        go.transform.position = pos;

        return go.AddComponent<Enemy>();
    }
}
