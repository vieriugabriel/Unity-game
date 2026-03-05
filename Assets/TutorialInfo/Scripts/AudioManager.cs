using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource[] audioSources;
    public float volume = 1f;

    void Awake()
    {
     
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("sound"));
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }
    public void PlayAudio(int index)
    {
        if (index >= 0 && index < audioSources.Length)
        {
            audioSources[index].Play();
        }
        else
        {
            Debug.LogWarning("Index-ul audio-ului este în afara limitelor array-ului.");
        }
    }
    public void StopAudio(int index)
    {
        if (index >= 0 && index < audioSources.Length)
        {
            audioSources[index].Stop();
        }
        else
        {
            Debug.LogWarning("Index-ul audio-ului este în afara limitelor array-ului.");
        }
    }
}
