using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : BaseScreen
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("ShowLevel", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public override IEnumerator EnterAsync(BaseScreen previous)
    {
        yield break;
    }

    public override IEnumerator ExitAsync(BaseScreen next)
    {
        yield break;
    }

    public override void OnBack()
    {
    }
}
