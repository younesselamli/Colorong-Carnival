using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject DrawPanel;

    public void OpenPanle()
    {
        DrawPanel.SetActive(true);
    }
    public void ClosePanel()
    {
        DrawPanel.SetActive(false);
    }
}
