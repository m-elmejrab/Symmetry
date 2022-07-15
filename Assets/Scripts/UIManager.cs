using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> //Class responsible for handling UI
{
    [SerializeField] Text levelHint;

    public void UpdateLevelHint(string hint)
    {
        levelHint.text = hint;
    }
    
}
