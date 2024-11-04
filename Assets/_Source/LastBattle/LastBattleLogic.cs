using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LastBattleLogic : MonoBehaviour
{
    public static bool IsStart;
    [SerializeField] private GameObject _golem;
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _hearts;
    [SerializeField] private float _enemyYPosition;
    [SerializeField] private float _enemyDownSpeed;
    [SerializeField] private AudioClip _stoneBreak;
    [SerializeField] private Button _buttonToSummon;
    [SerializeField] private GolemState _golemState;
    [SerializeField] private InventoryItem[] _godItems;
    [SerializeField] private InventoryItem[] _golemItems;
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    private GameObject _enemyToDown;
    private int _lives = 3;

    private void Awake()
    {
        _buttonToSummon.interactable = false;
        _buttonToSummon.onClick.AddListener(SummonGolem);
    }

    private void Update()
    {
        if (_lives == 0)
        {
            _hearts[0].SetActive(false);
            _hearts[1].SetActive(false);
            _hearts[2].SetActive(false);
        }
        else if (_lives == 1)
        {
            _hearts[0].SetActive(true);
            _hearts[1].SetActive(false);
            _hearts[2].SetActive(false);
        }
        else if (_lives == 2)
        {
            _hearts[0].SetActive(true);
            _hearts[1].SetActive(true);
            _hearts[2].SetActive(false);
        }
        else if (_lives == 3)
        {
            _hearts[0].SetActive(true);
            _hearts[1].SetActive(true);
            _hearts[2].SetActive(true);
        }
    }

    private void SummonGolem()
    {
        if (string.IsNullOrEmpty(_golemState.HeadState) || string.IsNullOrEmpty(_golemState.BodyState) || string.IsNullOrEmpty(_golemState.LegsState))
        {
            return;
        }

        UniTask.Create(StartBattle);
    }

    private async UniTask StartBattle()
    {
        IsStart = true;

        Check("water", "tree", "fire");
        Check("stone", "fire", "water");
        Check("ice", "water", "tree");

        foreach (var item in _godItems)
        {
            ResourceBank.Instance.Inventory.Add(item);
        }

        Check("god", "god", "god");

        Debug.Log("Victory");
        _sceneSwitcher.Switch("FinalLevel");

        IsStart = false;
    }

    private void Start()
    {
        UniTask.Create(DownGolem);
    }

    private void Check(string key1, string key2, string key3)
    {
        bool win = false;
        _buttonToSummon.interactable = false;

        if (_golemState.HeadState == key1 && _golemState.BodyState == key2 && _golemState.LegsState == key3)
        {
            win = true;
        }

        if (win)
        {
            SoundManager.Instance.PlaySound(_stoneBreak);
            BoxCollider2D[] colliders = _enemies[0].GetComponentsInChildren<BoxCollider2D>(true);

            foreach (BoxCollider2D collider in colliders)
            {
                collider.isTrigger = false;
            }

            Rigidbody2D[] rbs = _enemies[0].GetComponentsInChildren<Rigidbody2D>(true);

            foreach (Rigidbody2D rb in rbs)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }

            _lives -= 1;
            UniTask.Create(DownGolem);
        }
        else
        {
            Defeat();
            return;
        }
    }

    private void Defeat()
    {
        SoundManager.Instance.PlaySound(_stoneBreak);
        BoxCollider2D[] colliders = _golem.GetComponentsInChildren<BoxCollider2D>(true);

        foreach (BoxCollider2D collider in colliders)
        {
            collider.isTrigger = false;
        }

        Rigidbody2D[] rbs = _golem.GetComponentsInChildren<Rigidbody2D>(true);

        foreach (Rigidbody2D rb in rbs)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        IsStart = false;
        Debug.Log("Проиграл");
        _sceneSwitcher.Switch("LastBattle");
    }

    private async UniTask DownGolem()
    {
        if (_lives == 3)
        {
            _enemyToDown = _enemies[0];
        }
        if (_lives == 2)
        {
            _enemyToDown = _enemies[1];
        }
        if (_lives == 1)
        {
            _enemyToDown = _enemies[2];
        }
        if (_lives == 0)
        {
            _enemyToDown = _enemies[3];
        }

        while (_enemyToDown.transform.position.y > _enemyYPosition)
        {
            _enemyToDown.transform.position += new Vector3(0f, -_enemyDownSpeed * Time.deltaTime, 0f);
            await UniTask.NextFrame();
        }

        SoundManager.Instance.PlaySound(_stoneBreak);
        _buttonToSummon.interactable = true;
    }
}
