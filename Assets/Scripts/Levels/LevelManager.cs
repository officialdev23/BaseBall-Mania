using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; }

    public int levelNumber;

    public List<LvlObject> levelList;
    public int levelBalls;
    public int homeRunNeeded;
    public GameObject Level1;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        BracketManager.Instance.SetSelectedGameObject(Level1);
        //List<int> levelBalls = new List<int>() { 5, 10, 15 };
        Debug.Log("Level selection screen");
       // levelNumber = 0;
        levelBalls = levelList[levelNumber].balls;
        homeRunNeeded = levelList[levelNumber].target;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(levelNumber);
        Debug.Log(levelBalls);
        Debug.Log(homeRunNeeded);
    }

    public void SetLevelNo( int levelNo)
    {
        levelNumber = levelNo;
        levelBalls = levelList[levelNumber].balls;
        homeRunNeeded = levelList[levelNumber].target;
    }

    public void MoveToDerby()
    {

        SceneManager.LoadScene("DerbyScene", LoadSceneMode.Single);
    }

}
