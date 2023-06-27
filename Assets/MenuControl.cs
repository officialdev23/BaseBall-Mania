using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.Menu))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("DerbyScene", LoadSceneMode.Single);
    }
}
