using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    #region Unity Methods
    private void Start()
    {
        InitializePanel();
    }

    private void Update()
    {
        UpdatePanel();   
    }

    private void OnDestroy()
    {
        DestroyPanel();
    }
    #endregion Unity Methods

    #region Helper Methods
    public virtual void InitializePanel()
    {
        PanelManager.RegistPanel(GetType(), this);
    }

    public virtual void UpdatePanel()
    {
        
    }

    public virtual void DestroyPanel()
    {
        PanelManager.UnRegistPanel(GetType());
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    #endregion Helper Methods
}
