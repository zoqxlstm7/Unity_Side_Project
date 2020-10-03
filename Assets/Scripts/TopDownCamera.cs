using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    #region Variables
    [SerializeField] float height = 5.0f;

    [SerializeField] Transform target = null;
    #endregion Variables

    #region Unity Methods
    private void LateUpdate()
    {
        HandleCamera();
    }
    #endregion Unity Methods

    #region Helper Methods
    void HandleCamera()
    {
        if (target == null)
            return;

        Vector3 worldPosition = Vector3.up * height;
        Vector3 finalCameraPosition = worldPosition + target.position;
        //finalCameraPosition.x = transform.position.x;

        transform.position = finalCameraPosition;
    }
    #endregion Helper Methods
}
