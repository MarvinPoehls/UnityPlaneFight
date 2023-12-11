using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject activeMenu;
    [SerializeField] private Text menuText;

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenMenu(GameObject menu)
    {
        activeMenu.SetActive(false);
        menu.SetActive(true);
        activeMenu = menu;
        menuText.text = activeMenu.name;
    }
}
