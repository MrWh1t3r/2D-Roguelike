using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Player player;

    public int health;
    public int damage;
    public float attackChance = 0.5f;

    public GameObject deathDropPrefab;
    public SpriteRenderer sr;

    public LayerMask moveLayerMask;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void TakeDamage(int damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            if (deathDropPrefab != null)
                Instantiate(deathDropPrefab, transform.position, quaternion.identity);
            
            Destroy(gameObject);
        }

        StartCoroutine(DamageFlash());
        
        if(Random.value > attackChance)
            player.TakeDamage(damage);
    }

    IEnumerator DamageFlash()
    {
        Color defaultColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(0.05f);

        sr.color = defaultColor;
    }

    public void Move()
    {
        if(Random.value <0.5f)
            return;

        Vector3 dir = Vector3.zero;
        bool canMove = false;

        while (canMove == false)
        {
            dir = GetRandomDirection();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.0f, moveLayerMask);

            if (hit.collider == null)
                canMove = true;
        }

        transform.position += dir;
    }

    Vector3 GetRandomDirection()
    {
        int ran = Random.Range(0, 4);
        
        if(ran==0)
            return Vector3.up;
        else if(ran==1)
            return Vector3.down;
        else if(ran==2)
            return Vector3.left;
        else if(ran==3)
            return Vector3.right;

        return Vector3.zero;
    }
}
