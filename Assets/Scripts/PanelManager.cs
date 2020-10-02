using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PanelManager
{
    #region Variables
    static Dictionary<System.Type, BasePanel> panels = new Dictionary<System.Type, BasePanel>();
    #endregion Variables

    #region Other Methods
    public static void RegistPanel(System.Type panelType, BasePanel panel)
    {
        if (panels.ContainsKey(panelType))
            return;

        panels.Add(panelType, panel);
    }

    public static void UnRegistPanel(System.Type panelType)
    {
        if (!panels.ContainsKey(panelType))
            return;

        panels.Remove(panelType);
    }

    public static T GetPanel<T>() where T : BasePanel
    {
        System.Type panelType = typeof(T);
        return panels[panelType] as T;
    }
    #endregion Other Methods
}
