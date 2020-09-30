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
    [SerializeField] JoyStick joyStick;
    public JoyStick JoyStick => joyStick;
    #endregion Variables
}
