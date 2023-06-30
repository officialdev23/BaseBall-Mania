using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; }

    public int levelNumber;

    public List<int> levelBalls;
    public List<int> homeRunNeeded;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //List<int> levelBalls = new List<int>() { 5, 10, 15 };
        Debug.Log("Level selection screen");
        levelNumber = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(levelNumber);
        Debug.Log(levelBalls[levelNumber]);
    }

    public void SetLevelNo( int levelNo)
    {
        levelNumber = levelNo;
    }

    public void MoveToDerby()
    {
        SceneManager.LoadScene("DerbyScene", LoadSceneMode.Single);
    }

}
