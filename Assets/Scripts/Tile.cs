using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region Variables
    readonly float SPAWN_INTERVAL_TIME = 3.0f;

    [SerializeField] float tileFlashTime = 4.0f;

    float startSpawnTime = 0.0f;
    bool isGenerate = false;

    Color originColor;
    Material material;
    #endregion Variables

    #region Unity Methods
    private void Start()
    {
        material = GetComponent<Renderer>().material;
        originColor = material.color;
    }

    private void Update()
    {
        if(isGenerate)
        {
            ColorPingPong();

            if (Time.time - startSpawnTime > SPAWN_INTERVAL_TIME)
            {
                material.color = originColor;
                isGenerate = false;

                InGameSceneManager.instance.SpawnManager.SpawnEnemy(transform.position);
            }
        }
            
    }
    #endregion Unity Methods

    #region Helper Methods
    void ColorPingPong()
    {
        material.color = Color.Lerp(originColor, Color.red, Mathf.PingPong(Time.time * tileFlashTime, 1));
    }
    #endregion Helper Methods

    #region Other Methods
    public void Spawn()
    {
        startSpawnTime = Time.time;
        isGenerate = true;
    }
    #endregion Other Methods
}
