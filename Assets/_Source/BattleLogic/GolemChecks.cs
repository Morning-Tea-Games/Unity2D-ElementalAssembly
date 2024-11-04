using UnityEngine;

public class GolemChecks : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Golem")
        {
            Battle.IsEnd = true;
        }
    }
}
