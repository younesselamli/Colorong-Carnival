using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawSelection : MonoBehaviour
{
    public int currentDraw;
    public Button[] Draws;

    void Start()
    {
        foreach (Button drawButton in Draws)
        {
            drawButton.onClick.AddListener(SelectDraw);
        }
    }

    public void SelectDraw()
    {
        // Find the index of the clicked button in the Draws array
        int buttonIndex = System.Array.IndexOf(Draws, UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>());

        // Set currentDraw based on the button's position (plus one)
        currentDraw = buttonIndex + 1;

        Debug.Log("Current Draw: " + currentDraw);

        // Reset to 0 if it exceeds the maximum button index
        if (currentDraw > Draws.Length)
        {
            currentDraw = 0;
        }

        PlayerPrefs.SetInt("CurrentDraw", currentDraw);
        PlayerPrefs.Save();

        // Load the scene with the ObjectActivation script
        UnityEngine.SceneManagement.SceneManager.LoadScene("Draw");
    }


}
