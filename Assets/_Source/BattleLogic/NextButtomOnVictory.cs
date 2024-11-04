using System;
using UnityEngine;
using UnityEngine.UI;

public class NextButtomOnVictory : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    private int _level;

    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void Start()
    {
        _level = SceneSwitcher.Level;
    }

    private void OnButtonClick()
    {
        if (_level == 9)
        {
            _sceneSwitcher.Switch("LastBattle");
        }
        else
        {
            _sceneSwitcher.Switch("MainMap");
        }
    }
}
