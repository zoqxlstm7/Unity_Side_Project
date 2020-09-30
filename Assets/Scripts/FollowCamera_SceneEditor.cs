using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowCamera))]
public class FollowCamera_SceneEditor : Editor
{
    private void OnSceneGUI()
    {
        FollowCamera followCamera = target as FollowCamera;

        if (followCamera == null)
            return;

        if (followCamera.Target == null)
            return;

        followCamera.HandleCamera();
    }
}
