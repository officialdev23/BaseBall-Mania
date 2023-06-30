using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public GameObject menu;
    public GameObject trial;
    private void Update()
    {
        if (Input.GetKey(KeyCode.P)|| Input.GetKey(KeyCode.Menu))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("LevelSelectionScene", LoadSceneMode.Single);
        //menu.SetActive(false);
        //trial.SetActive(true);
    }
}
