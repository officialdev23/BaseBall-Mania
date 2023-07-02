using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DerbyManager : BaseScreen
{
    public Text RemainCountText;
    public VoidEvent GameOverEvent;
    public GameObject GameOverScene;
    public GameObject GameWinScene;
    public GameObject nextLevelBtn;
    public GameObject restartLevelBtn;
    public GameObject gameOverRestartLevelBtn;
    public GameObject gameOverHomebtn;
    public GameObject homeBtn;
    public MenuInputReader InputReader;
    public bool showLevel;

    public static DerbyManager Instance { get; private set; }

    public int m_Count;
    public int currentLevel;
    public bool levelComplete = false;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        m_Count = LevelManager.Instance.levelBalls[LevelManager.Instance.levelNumber - 1];
        RemainCountText.text = ""+m_Count;
        currentLevel = LevelManager.Instance.levelNumber - 1;
    }

    public void Update()
    {
        
        if(m_Count == 0  && levelComplete == true)
        {
            currentLevel = currentLevel + 1;
            Debug.Log("The current level is" + currentLevel);
            Time.timeScale = 0;
            GameWinScene.SetActive(true);
            BracketManager.Instance.SetSelectedGameObject(nextLevelBtn);
            //StartCoroutine("waitBeforeExit");
            //afterWinScene();
        }
        if(m_Count == 0 && levelComplete == false)
        {
            
            showGameOver();
            //StartCoroutine("waitBeforeExit");
        }
    }

    public void showGameOver()
    {
        --m_Count;
        Time.timeScale = 0;
        GameOverScene.SetActive(true);
        BracketManager.Instance.SetSelectedGameObject(gameOverRestartLevelBtn);
    }
    public void DecrementCount()
    {
        --m_Count;
        RemainCountText.text = "" + m_Count;
        if (m_Count == 0 || m_Count< 0)
        {
            //GameOver
            //Display Result
           // GameOverEvent.Raise();
            InputReader.StartActions += Restart;
        }
    }

    //public void afterWinScene()
    //{
    //    Debug.Log("I am called");
    //    GameOverEvent.Raise();
    //    InputReader.StartActions += Restart;
    //}

    public void Restart()
    {
        
        InputReader.StartActions -= Restart;
        
        SceneManager.LoadScene("DerbyScene", LoadSceneMode.Single);
    }

    private IEnumerator waitBeforeExit()
    {

        yield return new WaitForSecondsRealtime(5);
        //ScreenManager.Instance.SetScreen(SelectionScreen);
        GameManager.IsMenuOn = false;
       // SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        SceneManager.LoadScene("MenuScene");
        
     
        //ScreenManager.Instance.Start();
    }

    public void nextLevel()
    {
        Time.timeScale = 1;
        currentLevel = currentLevel + 1;
        LevelManager.Instance.SetLevelNo(currentLevel);
        LevelManager.Instance.MoveToDerby();
    }

    public void restartLevel()
    {
        Time.timeScale = 1;
        //m_Count = LevelManager.Instance.levelBalls[LevelManager.Instance.levelNumber - 1];
        //RemainCountText.text = "" + m_Count;
        //currentLevel = LevelManager.Instance.levelNumber - 1;
        //LevelManager.Instance.SetLevelNo(currentLevel);
        Start();
        LevelManager.Instance.MoveToDerby();
    }

    public void goToHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
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
