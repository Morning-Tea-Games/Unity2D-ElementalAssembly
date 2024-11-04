using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ResourceBank : MonoBehaviour
{
    // Singleton
    public static ResourceBank Instance { get; private set; }

    // Components
    [SerializeField] private int _autoSaveIntervalMs = 1000;
    [SerializeField] private bool _logging = false;

    // Fields
    public List<InventoryItem> Inventory { get; private set; }
    public int AvailableLevel { get; set; }
    public float SoundVolume { get; set; }
    public float MusicVolume { get; set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Inventory = new List<InventoryItem>();
        Load();
        Inventory.Clear();
    }

    private void Start()
    {
        UniTask.Create(AutoSaving);
    }

    private async UniTask AutoSaving()
    {
        while (Application.isPlaying)
        {
            Save();
            await UniTask.Delay(_autoSaveIntervalMs);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("AvailableLevel", AvailableLevel);
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);

        if (_logging)
        {
            Debug.Log("Data saved...");
        }
    }

    public void Load()
    {
        if (_logging)
        {
            if (PlayerPrefs.HasKey("AvailableLevel"))
            {
                Debug.Log("Data loaded...");
            }
            else
            {
                Debug.Log("No data found. Creating new...");
            }
        }

        AvailableLevel = PlayerPrefs.GetInt("AvailableLevel", 1);
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.3f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        AvailableLevel = 1;
        SoundVolume = 0.3f;
        MusicVolume = 0.3f;

        if (_logging)
        {
            Debug.Log("Data removed...");
        }
    }
}