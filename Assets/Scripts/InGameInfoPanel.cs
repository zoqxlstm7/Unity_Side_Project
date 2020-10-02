using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameInfoPanel : BasePanel
{
    #region Variables
    [SerializeField] Text waveCountText = null;
    [SerializeField] Text scoreText = null;
    #endregion Variables

    #region BasePanel Methods
    public override void InitializePanel()
    {
        base.InitializePanel();

        SetWaveCount();
        SetScore();
    }
    #endregion BasePanel Methods

    #region Other Methods
    public void SetWaveCount()
    {
        waveCountText.text = InGameSceneManager.instance.waveCount.ToString();
    }

    public void SetScore()
    {
        scoreText.text = InGameSceneManager.instance.score.ToString();
    }
    #endregion Other Methods
}
