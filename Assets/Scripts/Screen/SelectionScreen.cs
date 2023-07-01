using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScreen : BaseScreen
{

    static SelectionScreen instance;

    public GameObject levelButton;
    public GameObject levelSelectionScreen;


    public static SelectionScreen Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType(typeof(SelectionScreen)) as SelectionScreen;

            return instance;
        }
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
        gameObject.name = this.GetType().Name;
        DontDestroyOnLoad(gameObject);
        //	InitializeAds();
    }
    public override IEnumerator EnterAsync(BaseScreen previous)
    {
        BracketManager.Instance.SetSelectedGameObject(levelButton);
        yield break;
        
    }

    public override IEnumerator ExitAsync(BaseScreen next)
    {
        yield break;
    }

    public override void OnBack() => ScreenManager.Instance.SetScreen(MenuControl);

    public void ShowLevelSelectionScreen()
    {
        levelSelectionScreen.SetActive(true);
    }
}
