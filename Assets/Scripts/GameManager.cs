using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int level;
    public int baseSeed;

    private int _prevRoomPlayerHealth;
    private int _prevRoomPlayerCoins;

    private Player _player;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        level = 1;
        baseSeed = PlayerPrefs.GetInt("Seed");
        Random.InitState(baseSeed);
        Generation.Instance.Generate();
        UI.Instance.UpdateLevelText(level);

        _player = FindObjectOfType<Player>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Game")
        {
            Destroy(gameObject);
            return;
        }

        _player = FindObjectOfType<Player>();
        level++;
        baseSeed++;
        
        Generation.Instance.Generate();

        _player.curHp = _prevRoomPlayerHealth;
        _player.coins = _prevRoomPlayerCoins;
        
        UI.Instance.UpdateHealth(_prevRoomPlayerHealth);
        UI.Instance.UpdateCoinText(_prevRoomPlayerCoins);
        UI.Instance.UpdateLevelText(level);
        
    }

    public void GoToNextLevel()
    {
        _prevRoomPlayerHealth = _player.curHp;
        _prevRoomPlayerCoins = _player.coins;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
