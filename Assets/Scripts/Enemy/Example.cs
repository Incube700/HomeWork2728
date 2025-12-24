using System;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    private EnemyDestroyerService _service;
    private readonly List<Enemy> _spawned = new List<Enemy>();

    public void Construct(EnemyDestroyerService service)
    {
        _service = service;
    }

    private void Update()
    {
        if (_service == null)
            return;

        // Z — логическая смерть
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Enemy enemy = SpawnEnemy();
            _spawned.Add(enemy);

            _service.Register(enemy, () => enemy.IsDead);
        }

        // X — таймер
        if (Input.GetKeyDown(KeyCode.X))
        {
            Enemy enemy = SpawnEnemy();
            _spawned.Add(enemy);

            float bornTime = Time.time;
            _service.Register(enemy, () => Time.time - bornTime >= 3f);
        }

        // C — по количеству
        if (Input.GetKeyDown(KeyCode.C))
        {
            Enemy enemy = SpawnEnemy();
            _spawned.Add(enemy);

            _service.Register(enemy, () => _service.Count > 5);
        }

        // V — убить всех
        if (Input.GetKeyDown(KeyCode.V))
        {
            for (int i = 0; i < _spawned.Count; i++)
            {
                if (_spawned[i] != null)
                    _spawned[i].Kill();
            }
        }
    }

    private Enemy SpawnEnemy()
    {
        Camera cam = Camera.main;

        Vector3 pos = cam.transform.position + cam.transform.forward * 6f;
        pos.y = 1f;
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