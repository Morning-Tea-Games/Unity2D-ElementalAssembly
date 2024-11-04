using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    public void Activate()
    {
        ResourceBank.Instance.Reset();
    }
}