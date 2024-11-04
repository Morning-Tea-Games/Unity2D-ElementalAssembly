using UnityEngine;
using UnityEngine.EventSystems;

public class MagicSecret : MonoBehaviour
{
    public static bool Secret;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GameObject _secret;
    private int _clicks;

    private void Start()
    {
        Secret = false;
    }

    private void Update()
    {
        if (Secret)
        {
            Instantiate(_secret);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _clicks++;

            if (_clicks >= 50)
            {
                _animator.SetBool("Secret", true);
                _sr.sprite = _sprite;
            }

            if (_clicks >= 100)
            {
                Secret = true;
            }
        }
    }
}