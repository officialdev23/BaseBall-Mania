using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DerbyManager : BaseScreen
{
    public Text RemainCountText;
    public VoidEvent GameOverEvent;
    public MenuInputReader InputReader;
    public bool showLevel;

    public static DerbyManager Instance { get; private set; }

    public int m_Count;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        m_Count = LevelManager.Instance.levelBalls[LevelManager.Instance.levelNumber - 1];
        RemainCountText.text = ""+m_Count;
    }

    public void Update()
    {
        if(m_Count == 0 )
        {
            StartCoroutine("waitBeforeExit");
            //afterWinScene();
        }
    }
    public void DecrementCount()
    {
        --m_Count;
        RemainCountText.text = "" + m_Count;
        if (m_Count == 0 || m_Count< 0)
        {
            //GameOver
            //Display Result
            GameOverEvent.Raise();
            InputReader.StartActions += Restart;
        }
    }

    public void afterWinScene()
    {
        Debug.Log("I am called");
        GameOverEvent.Raise();
        InputReader.StartActions += Restart;
    }

    public void Restart()
    {
        
        InputReader.StartActions -= Restart;
        
        SceneManager.LoadScene("DerbyScene", LoadSceneMode.Single);
    }

    private IEnumerator waitBeforeExit()
    {
        PlayerPrefs.SetInt("ShowLevel", 1);
        yield return new WaitForSecondsRealtime(5);
        //ScreenManager.Instance.SetScreen(SelectionScreen);
        
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        //ScreenManager.Instance.Start();
    }



    //BaseScreen Implementation
    public override IEnumerator EnterAsync(BaseScreen previous)
    {
       // BracketManager.Instance.SetSelectedGameObject(levelButton);
        yield break;

    }

    public override IEnumerator ExitAsync(BaseScreen next)
    {
        yield break;
    }

    public override void OnBack() => ScreenManager.Instance.SetScreen(MenuControl);

}
