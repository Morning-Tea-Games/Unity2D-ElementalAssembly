using Cysharp.Threading.Tasks;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static string LastTrack { get; private set; }
    [SerializeField] private AudioClip _track;
    [SerializeField] private string _currentTrack;

    private void Start()
    {
        if (!string.IsNullOrEmpty(LastTrack) && LastTrack == _currentTrack)
        {
            return;
        }

        LastTrack = _currentTrack;
        PlayMusicForCurrentScene().Forget();
    }

    private async UniTask PlayMusicForCurrentScene()
    {
        while (LastTrack == _currentTrack)
        {
            SoundManager.Instance.PlayMusic(_track);
            await UniTask.Delay((int)Mathf.Round(_track.length * 1000));
        }
    }
}