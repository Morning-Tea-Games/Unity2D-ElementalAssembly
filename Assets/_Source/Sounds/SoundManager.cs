using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public float SoundVolume { get; set; } = 1f;
    public float MusicVolume { get; set; } = 1f;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

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
    }

    private void Update()
    {
        SoundVolume = ResourceBank.Instance.SoundVolume;
        MusicVolume = ResourceBank.Instance.MusicVolume;
        _musicSource.volume = MusicVolume;
        _soundSource.volume = SoundVolume;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }
        
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        if (_soundSource.isPlaying)
        {
            _soundSource.Stop();
        }
        
        _soundSource.clip = clip;
        _soundSource.Play();
    }
}