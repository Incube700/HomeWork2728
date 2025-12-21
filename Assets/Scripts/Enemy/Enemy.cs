using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool IsDead { get; private set; }

    public void Kill()
    {
        IsDead = true;
    }
}