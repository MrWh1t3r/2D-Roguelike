using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().hasKey = true;
            UI.Instance.ToggleKeyIcon(true);
            Destroy(gameObject);
        }
    }
}
