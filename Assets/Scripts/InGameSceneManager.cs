using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneManager : MonoBehaviour
{
    #region Singleton
    public static InGameSceneManager instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }
    #endregion Singleton

    #region Variables
    public int waveCount = 1;
    public int score = 0;

    public bool isGameOver = false;

    [SerializeField] Player player;
    public Player Player => player;

    CacheSystem cacheSystem = new CacheSystem();
    public CacheSystem CacheSystem => cacheSystem;

    [SerializeField] JoyStick joyStick;
    public JoyStick JoyStick => joyStick;

    [SerializeField] MapGenerator mapGenerator;
    public MapGenerator MapGenerator => mapGenerator;

    [SerializeField] ProjectileManager projectileManager;
    public ProjectileManager ProjectileManager => projectileManager;

    [SerializeField] EnemyManager enemyManager;
    public EnemyManager EnemyManager => enemyManager;

    [SerializeField] SpawnManager spawnManager;
    public SpawnManager SpawnManager => spawnManager;
    #endregion Variables

    #region Unity Methods
    private void Update()
    {
        if(isGameOver)
        {
            PanelManager.GetPanel<GameOverPanel>().Show();
            isGameOver = false;
        }
    }
    #endregion Unity Methods
}
