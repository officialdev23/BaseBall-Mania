using GBP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BracketManager : MonoBehaviour
{
	static BracketManager instance;

    public static int LaunchCount
    {
        get => PlayerPrefs.GetInt("LaunchJFCount", 0);
        set => PlayerPrefs.SetInt("LaunchJFCount", value);
    }

    public static BracketManager Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType(typeof(BracketManager)) as BracketManager;

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
	public void SetSelectedGameObject(GameObject go)
	{
		EventSystem.current.SetSelectedGameObject(go);
	}
}
