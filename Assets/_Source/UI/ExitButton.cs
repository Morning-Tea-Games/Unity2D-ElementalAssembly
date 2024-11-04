using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private void Awake()
    {
        #if UNITY_WEBGL
            gameObject.SetActive(false);
        #else
            gameObject.SetActive(true);
        #endif
    }
}