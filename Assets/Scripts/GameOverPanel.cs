using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel
{
    #region Variables
    readonly float DELAY_INTERVAL_TIME = 0.001f;

    [SerializeField] Text scoreText = null;

    float startTime = 0.0f;
    int calcScore = 0;
    #endregion Variables

    #region BasePanel Methods
    public override void InitializePanel()
    {
        base.InitializePanel();
        scoreText.text = calcScore.ToString();

        Close();
    }

    public override void UpdatePanel()
    {
        base.UpdatePanel();

        if (calcScore >= InGameSceneManager.instance.score)
            return;

        if(Time.time - startTime > DELAY_INTERVAL_TIME)
        {
            calcScore += 1;
            scoreText.text = calcScore.ToString();

            startTime = Time.time;
        }
    }
    #endregion BasePanel Methods

    #region Helper Methods
    public void RetryBtn()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
    #endregion Helper Methods
}
