using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public static Sprite Head { get; set; }
    public static Sprite Body { get; set; }
    public static Sprite Legs { get; set; }
    public static bool IsEnd { get; set; }
    public static bool IsStart = false;

    [SerializeField] private Button _button;
    [SerializeField] private Button _backButton;

    [SerializeField] private SpriteRenderer _golemHead;
    [SerializeField] private SpriteRenderer _golembody;
    [SerializeField] private SpriteRenderer _golemlegs;

    [SerializeField] private BoxCollider2D _golemHeadCollider;
    [SerializeField] private BoxCollider2D _golemBodyCollider;
    [SerializeField] private BoxCollider2D _golemLegsCollider;

    [SerializeField] private Rigidbody2D _golemHeadRb;
    [SerializeField] private Rigidbody2D _golemBodyRb;
    [SerializeField] private Rigidbody2D _golemLegsRb;

    [SerializeField] private BoxCollider2D _enemyHeadCollider;
    [SerializeField] private BoxCollider2D _enemyBodyCollider;
    [SerializeField] private BoxCollider2D _enemyLegsCollider;

    [SerializeField] private Rigidbody2D _enemyHeadRb;
    [SerializeField] private Rigidbody2D _enemyBodyRb;
    [SerializeField] private Rigidbody2D _enemyLegsRb;

    [SerializeField] private GameObject _golem;
    [SerializeField] private Animator _mageAnimator;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GolemState _golemState;
    [SerializeField] private WindowView _victory;
    [SerializeField] private WindowView _defeat;
    [SerializeField] private AudioClip _stoneBreak;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _creatingGolemSound;

    [SerializeField] private float _speed;
    [SerializeField] private float _golemSpeed;
    [SerializeField] private float _force;
    [SerializeField] private int _windowDelayMs;
    [SerializeField] private List<RightKeys> _rightKeys;

    private int _level;

    private void Awake()
    {
        _button.onClick.AddListener(StartBattle);

        Head = null;
        Body = null;
        Legs = null;

        _golem.SetActive(false);
    }

    private void Start()
    {
        _level = SceneSwitcher.Level;

        _golemHead.material.SetFloat("_Value", 0);
        _golembody.material.SetFloat("_Value", 0);
        _golemlegs.material.SetFloat("_Value", 0);
    }

    private async void StartBattle()
    {   
        if (!Head || !Body || !Legs)
        {
            Debug.LogWarning("Not all keys are set");
            return;
        }

        IsStart = true;

        _button.interactable = false;
        _backButton.interactable = false;

        _golemHead.sprite = Head;
        _golembody.sprite = Body;
        _golemlegs.sprite = Legs;
        _golem.SetActive(true);
        _mageAnimator.SetTrigger("Attack");
        SoundManager.Instance.PlaySound(_creatingGolemSound);
        await UniTask.Create(SmoothShowGolem);
        await UniTask.Create(MoveGolem);

        bool isHeadRight = false, isBodyRight = false, isLegsRight = false;

        for (var i = 0; i < _rightKeys.Count; i++)
        {
            if (_rightKeys[i].HeadKey == _golemState.HeadState && _rightKeys[i].Level == _level)
            {
                isHeadRight = true;
            }
        }

        for (var i = 0; i < _rightKeys.Count; i++)
        {
            if (_rightKeys[i].BodyKey == _golemState.BodyState && _rightKeys[i].Level == _level)
            {
                isBodyRight = true;
            }
        }

        for (var i = 0; i < _rightKeys.Count; i++)
        {
            if (_rightKeys[i].LegsKey == _golemState.LegsState && _rightKeys[i].Level == _level)
            {
                isLegsRight = true;
            }
        }

        if (isLegsRight && isHeadRight && isBodyRight)
        {
            SoundManager.Instance.PlaySound(_stoneBreak);

            _golemHeadCollider.isTrigger = true;
            _golemBodyCollider.isTrigger = true;
            _golemLegsCollider.isTrigger = true;

            _enemyHeadCollider.isTrigger = false;
            _enemyBodyCollider.isTrigger = false;
            _enemyLegsCollider.isTrigger = false;

            _enemyHeadRb.bodyType = RigidbodyType2D.Dynamic;
            _enemyBodyRb.bodyType = RigidbodyType2D.Dynamic;
            _enemyLegsRb.bodyType = RigidbodyType2D.Dynamic;

            _enemyHeadRb.AddForce(new Vector3(Random.Range(-10f, 10f) * _force, Random.Range(-10f, 10f) * _force, 0f), ForceMode2D.Impulse);
            _enemyBodyRb.AddForce(new Vector3(Random.Range(-10f, 10f) * _force, Random.Range(-10f, 10f) * _force, 0f), ForceMode2D.Impulse);
            _enemyLegsRb.AddForce(new Vector3(Random.Range(-10f, 10f) * _force, Random.Range(-10f, 10f) * _force, 0f), ForceMode2D.Impulse);

            if (ResourceBank.Instance.AvailableLevel <= _level)
            {
                ResourceBank.Instance.AvailableLevel = _level + 1;
            }
            
            IsEnd = false;
            await UniTask.Delay(_windowDelayMs);
            _victory.Show();
            SoundManager.Instance.PlaySound(_winSound);
            IsStart = false;
            return;
        }

        SoundManager.Instance.PlaySound(_stoneBreak);

        _golemHeadCollider.isTrigger = false;
        _golemBodyCollider.isTrigger = false;
        _golemLegsCollider.isTrigger = false;

        _golemHeadRb.bodyType = RigidbodyType2D.Dynamic;
        _golemBodyRb.bodyType = RigidbodyType2D.Dynamic;
        _golemLegsRb.bodyType = RigidbodyType2D.Dynamic;

        _golemHeadRb.AddForce(new Vector3(Random.Range(-10f, 10f) * _force, Random.Range(-10f, 10f) * _force, 0f), ForceMode2D.Impulse);
        _golemBodyRb.AddForce(new Vector3(Random.Range(-10f, 10f) * _force, Random.Range(-10f, 10f) * _force, 0f), ForceMode2D.Impulse);
        _golemLegsRb.AddForce(new Vector3(Random.Range(-10f, 10f) * _force, Random.Range(-10f, 10f) * _force, 0f), ForceMode2D.Impulse);

        await UniTask.Delay(_windowDelayMs);
        _defeat.Show();
        SoundManager.Instance.PlaySound(_loseSound);
        IsEnd = false;
        IsStart = false;
    }

    private async UniTask MoveGolem()
    {
        while (!IsEnd)
        {
            _golem.transform.position += new Vector3(_golemSpeed * Time.deltaTime, 0f, 0f);
            await UniTask.NextFrame();
        }
    }

    private async UniTask SmoothShowGolem()
    {
        var step = 0f;

        while (step < 1f)
        {
            if (step > 1f)
            {
                step = 1f;
            }

            step += 0.001f * _speed * Time.deltaTime;
            _golemHead.sharedMaterial.SetFloat("_Value", step);
            _golembody.sharedMaterial.SetFloat("_Value", step);
            _golemlegs.sharedMaterial.SetFloat("_Value", step);

            await UniTask.NextFrame();
        }
    }
}