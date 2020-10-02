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
    [SerializeField] Player player;
    public Player Player => player;

    CacheSystem cacheSystem = new CacheSystem();
    public CacheSystem CacheSystem => cacheSystem;

    [SerializeField] JoyStick joyStick;
    public JoyStick JoyStick => joyStick;

    [SerializeField] ProjectileManager projectileManager;
    public ProjectileManager ProjectileManager => projectileManager;
    #endregion Variables
}
