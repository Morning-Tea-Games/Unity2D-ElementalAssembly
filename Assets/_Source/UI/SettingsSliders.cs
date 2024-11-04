using UnityEngine;
using UnityEngine.UI;

public class SettingsSliders : MonoBehaviour
{
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    private void Start()
    {
        _soundSlider.onValueChanged.AddListener(OnSoundValueChanged);
        _musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            _soundSlider.value = ResourceBank.Instance.SoundVolume;
            _musicSlider.value = ResourceBank.Instance.MusicVolume;
        }
    }

    private void OnMusicValueChanged(float value)
    {
        SoundManager.Instance.MusicVolume = value;
        ResourceBank.Instance.MusicVolume = value;
    }

    private void OnSoundValueChanged(float value)
    {
        SoundManager.Instance.SoundVolume = value;
        ResourceBank.Instance.SoundVolume = value;
    }
}
