using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : BaseScreen
{
    public static MenuControl Instance { get; private set; }

    public GameObject menu;
    public GameObject levelSelection;
    public int menuStatus;

    private bool returningFromDerbyScene = false;

    private void Update()
    {
        Debug.Log("This is test");
        //if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Menu))
        //{
        //    StartGame();
        //}

        StartCoroutine("waitBeforeLoad");
    }

    public override IEnumerator EnterAsync(BaseScreen previous)
    {
        Debug.Log("Back to MenuControl");
        returningFromDerbyScene = true; // Set returningFromDerbyScene to true when entering the menu scene
        yield break;
    }

    public override IEnumerator ExitAsync(BaseScreen next)
    {
        yield break;
    }

    public override void OnBack()
    {
    }

    public IEnumerator waitBeforeLoad()
    {
        yield return new WaitForSeconds(5f);
        StartGame();
    }
    public void StartGame()
    {
        ScreenManager.Instance.SetScreen(SelectionScreen);
    }
}
