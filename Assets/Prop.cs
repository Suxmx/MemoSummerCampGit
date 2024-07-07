using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int MaxHp=5;
    public int CurrentHp;

    private void Start()
    {
        CurrentHp = MaxHp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            CurrentHp--;
            if (CurrentHp == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
