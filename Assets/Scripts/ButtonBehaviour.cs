using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonBehaviour : MonoBehaviour
{
    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void EasyButton()
    {
        SceneManager.LoadScene("EasyScene");
    }

    public void MediumButton()
    {
        SceneManager.LoadScene("MediumScene");
    }

    public void HardButton()
    {
        SceneManager.LoadScene("HardScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
