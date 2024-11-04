using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public static int Level = 0;
    [SerializeField] private SwitchType _switchType;
    [SerializeField] private int _levelSwitchTo;
    [SerializeField] private Button _buttonToLevel;

    private void Update()
    {
        if (_switchType != SwitchType.ToLevel)
        {
            return;
        }

        if (ResourceBank.Instance.AvailableLevel >= _levelSwitchTo)
        {
            _buttonToLevel.interactable = true;
        }
        else
        {
            _buttonToLevel.interactable = false;
        }   
    }

    public void Switch(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SwitchToLevel()
    {
        Level = _levelSwitchTo;
        Switch("Battle");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private enum SwitchType
    {
        ToScene, ToLevel
    }
}