using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Coin,
    Health
}

public class Pickup : MonoBehaviour
{
    public PickupType type;
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (type == PickupType.Coin)
            {
                col.GetComponent<Player>().AddCoins(value);
                Destroy(gameObject);
            }
            else if (type == PickupType.Health)
            {
                if (col.GetComponent<Player>().AddHealth(value))
                    Destroy(gameObject);
            }
        }
    }
}
