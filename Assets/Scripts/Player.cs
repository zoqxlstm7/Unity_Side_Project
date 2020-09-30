using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    #region Variables
    [SerializeField] float moveSpeed = 5.0f;
    #endregion Variables

    #region Actor Methods
    public override void UpdateActor()
    {
        UpdateMove();
    }
    #endregion Actor Methods

    #region Helper Methods
    void UpdateMove()
    {
        Vector2 inputVector = InGameSceneManager.instance.JoyStick.GetInputVector();
        if (inputVector == Vector2.zero)
            return;

        Vector3 moveVector = new Vector3(inputVector.x, 0.0f, inputVector.y);

        transform.forward = moveVector;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }
    #endregion Helper Methods
}
