using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    #region Variables
    [SerializeField] float distance = 10.0f;
    [SerializeField] float height = 15.0f;
    [SerializeField] float lookAtHeight = 2.0f;
    [Range(0.0f, 360.0f)]
    [SerializeField] float angle = 0.0f;

    [SerializeField] float smoothSpeed = 0.1f;

    [SerializeField] Transform target = null;

    Vector3 calcVelocity = Vector3.zero;
    #endregion Variables

    #region Property
    public Transform Target => target;
    #endregion Property

    #region Unity Methods
    private void LateUpdate()
    {
        HandleCamera();
    }
    #endregion Unity Methods

    #region Helper Methods
    public void HandleCamera()
    {
        if (target == null)
            return;

        Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);
        Vector3 rotatePosition = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;

        Vector3 finalTargetPosition = target.position;
        finalTargetPosition.y += lookAtHeight;

        Vector3 finalCameraPosition = rotatePosition + finalTargetPosition;

        transform.position = Vector3.SmoothDamp(transform.position, finalCameraPosition, ref calcVelocity, smoothSpeed);
        transform.LookAt(target.position);
    }
    #endregion Helper Methods
}
