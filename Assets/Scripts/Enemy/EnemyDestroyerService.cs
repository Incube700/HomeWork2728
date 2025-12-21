using System;
using System.Collections.Generic;

public class EnemyDestroyerService
{
    private class Entry
    {
        public Enemy Enemy;
        public Func<bool> ShouldDestroy;
    }

    private readonly List<Entry> _entries = new List<Entry>();
    private readonly Action<Enemy> _destroyAction;

    public int Count => _entries.Count;

    public EnemyDestroyerService(Action<Enemy> destroyAction)
    {
        _destroyAction = destroyAction;
    }

    public void Register(Enemy enemy, Func<bool> shouldDestroy)
    {
        if (enemy == null || shouldDestroy == null)
            return;

        _entries.Add(new Entry { Enemy = enemy, ShouldDestroy = shouldDestroy });
    }

    public void Tick()
    {
        //  выводим сколько врагов зарегистрировано покадрово
        UnityEngine.Debug.Log("Enemies registered: " + _entries.Count);

        for (int i = _entries.Count - 1; i >= 0; i--)
        {
            Entry entry = _entries[i];

            if (entry.Enemy == null)
            {
                _entries.RemoveAt(i);
                continue;
            }

            if (entry.ShouldDestroy())
            {
                _destroyAction?.Invoke(entry.Enemy);
                _entries.RemoveAt(i);
            }
        }
    }

    public static Func<bool> AnyOf(params Func<bool>[] conditions)
    {
        return () =>
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (conditions[i] != null && conditions[i]())
                    return true;
            }

            return false;
        };
    }
}