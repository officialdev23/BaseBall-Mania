using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; set; }
    public BaseScreen CurrentScreen { get; private set; }
    private BaseScreen _nextScreen;
    private Coroutine _switchCoroutine;
    private BaseScreen[] _screens;

    private void Awake()
    {
        Instance = this;
        _screens = GetComponentsInChildren<BaseScreen>();
        PlayerPrefs.SetInt("ShowLevel", 0);
    }

    public void Start()
    {
        HideAllScreens();

        SetScreen(BaseScreen.MenuControl);
        
    }

    public void SetScreen(BaseScreen screen)
    {
        if (!CurrentScreen)
        {
            CurrentScreen = screen;
            screen.InitializeScreen();
            StartCoroutine(screen.EnterAsync(null));
            return;
        }

        // Prepare to set screen
        _nextScreen = screen;

        // Already screen switch is in progress. Try again later
        if (_switchCoroutine != null) return;

        // if (screen.Equals(CurrentScreen)) return;

        // Start switching process
        _switchCoroutine = StartCoroutine(SetScreenAsync(screen));

        // No need to cache now
        _nextScreen = null;
    }

    public void SelectGameObject(GameObject defaultButton)
    {
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    private void Update()
    {
        // Is NextScreen cached but not switched yet? Then switch it.
        if (_nextScreen && _nextScreen != CurrentScreen) SetScreen(_nextScreen);
    }

    private IEnumerator SetScreenAsync(BaseScreen screen)
    {
        yield return CurrentScreen.ExitAsync(screen);
        CurrentScreen.UnsetScreen();

        var prevScreen = CurrentScreen;
        CurrentScreen = screen;
        screen.InitializeScreen();
        yield return screen.EnterAsync(prevScreen);
        _switchCoroutine = null;
    }

    private void HideAllScreens()
    {
        foreach (var screen in _screens)
        {
            screen.gameObject.SetActive(false);
        }
    }
}
