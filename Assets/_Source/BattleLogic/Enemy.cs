using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<EnemySet> _enemies;
    [SerializeField] private SpriteRenderer _headField;
    [SerializeField] private SpriteRenderer _bodyField;
    [SerializeField] private SpriteRenderer _legsField;

    private void Start()
    {
        int level = 0;

        foreach (var enemy in _enemies)
        {
            if (enemy.level == SceneSwitcher.Level)
            {
                level = enemy.level;
                break;
            }
        }

        _headField.sprite = _enemies[level - 1].Top;
        _bodyField.sprite = _enemies[level - 1].Mid;
        _legsField.sprite = _enemies[level - 1].Bottom;
    }
}