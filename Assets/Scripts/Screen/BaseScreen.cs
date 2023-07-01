using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour
{
   // //public static Gameplay Gameplay { get; private set; }
    public static MenuControl MenuControl { get; private set; }
    // public static Pause Pause{ get; private set; }
    //  public static WinScreen WinScreen { get; private set; }
     public static SelectionScreen SelectionScreen { get; private set; }

   /// public static SelectionScreenTwo Selection2 { get; private set; }
   //// // public static ModeSelection ModeSelection { get; private set; }
    // public static GameOver GameOver{ get; private set; }

    public GameObject defaultButton;
    private void OnEnable()
    {
        switch (this)
        {
            case MenuControl m: MenuControl = m; break;
           // case Gameplay gp: Gameplay = gp; break;
           // // case Pause p:     Pause = p; break;
           //// case WinScreen w: WinScreen = w; break;
            case SelectionScreen s: SelectionScreen = s; break;

           // case SelectionScreenTwo s2: Selection2 = s2; break;
                // case ModeSelection ms:     ModeSelection = ms; break;
                // case GameOver go:     GameOver = go; break;
        }
    }

    public void InitializeScreen()
    {
        gameObject.SetActive(true);
        ScreenManager.Instance.SelectGameObject(defaultButton);
    }

    public void UnsetScreen() => gameObject.SetActive(false);

    public abstract IEnumerator EnterAsync(BaseScreen previous);
    public abstract IEnumerator ExitAsync(BaseScreen next);
    public abstract void OnBack();

}
