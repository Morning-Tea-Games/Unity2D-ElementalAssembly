using UnityEngine;

public class Tutor : MonoBehaviour
{
    private void Start()
    {
        if (SceneSwitcher.Level == 1)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
