using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Door Objects")] 
    public Transform northDoor;
    public Transform southDoor;
    public Transform eastDoor;
    public Transform westDoor;

    [Header("Wall Objects")]
    public Transform northWall;
    public Transform southWall;
    public Transform eastWall;
    public Transform westWall;

    [Header("Values")]
    public int insideWidth;
    public int insideHeight;

    [Header("prefabs")]
    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public GameObject healthPrefab;
    public GameObject keyPrefab;
    public GameObject exitDoorPrefab;

    private List<Vector3> _usedPositions = new List<Vector3>();

    public void GenerateInterior()
    {
        if (Random.value < Generation.Instance.enemySpawnChance)
            SpawnPrefab(enemyPrefab, 1, Generation.Instance.maxEnemiesPerRoom + 1);
        if (Random.value < Generation.Instance.coinSpawnChance)
            SpawnPrefab(coinPrefab, Generation.Instance.maxCoinsPerRoom + 1);
        if (Random.value < Generation.Instance.healthSpawnChance)
            SpawnPrefab(healthPrefab, Generation.Instance.maxHealthPerRoom + 1);
    }

    public void SpawnPrefab(GameObject prefab, int min = 0, int max = 0)
    {
        int num = 1;

        if (min != 0 || max != 0)
            num = Random.Range(min, max);

        for (int x = 0; x < num; ++x)
        {
            GameObject obj = Instantiate(prefab);
            Vector3 pos = transform.position + new Vector3(Random.Range(-insideWidth/2,insideWidth/2+1),Random.Range(-insideHeight / 2,insideHeight / 2 + 1),0);

            while (_usedPositions.Contains(pos))
            {
                pos = transform.position + new Vector3(Random.Range(-insideWidth/2,insideWidth/2+1),Random.Range(-insideHeight / 2,insideHeight / 2 + 1),0);
            }

            obj.transform.position = pos;
            _usedPositions.Add(pos);
            
            if(prefab==enemyPrefab)
                EnemyManager.Instance.enemies.Add(obj.GetComponent<Enemy>());
        }
    }
}
