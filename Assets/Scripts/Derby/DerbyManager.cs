using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DerbyManager : MonoBehaviour
{
    public Text RemainCountText;
    public VoidEvent GameOverEvent;
    public MenuInputReader InputReader;

    public static DerbyManager Instance { get; private set; }

    public int m_Count = 5;

    public void Awake()
    {
        Instance = this;
    }
    public void DecrementCount()
    {
        --m_Count;
        RemainCountText.text = "" + m_Count;
        if (m_Count == 0)
        {
            //GameOver
            //Display Result
            GameOverEvent.Raise();
            InputReader.StartActions += Restart;
        }
    }

    public void afterWinScene()
    {
        GameOverEvent.Raise();
        InputReader.StartActions += Restart;
    }

    private void Restart()
    {
        InputReader.StartActions -= Restart;
        SceneManager.LoadScene("DerbyScene", LoadSceneMode.Single);
    }
}
