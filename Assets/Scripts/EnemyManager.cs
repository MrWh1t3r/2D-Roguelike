using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

    public static EnemyManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OnPlayerMove()
    {
        StartCoroutine(MoveEnemies());
    }

    IEnumerator MoveEnemies()
    {
        yield return new WaitForFixedUpdate();

        foreach (var enemy in enemies)
        {
            if(enemy!=null)
                enemy.Move();
        }
    }
}
