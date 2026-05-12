using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music")]
    public AudioClip currentMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ======================
    // SFX
    // ======================
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    // ======================
    // MUSIC
    // ======================
    public void PlayMusic(AudioClip music)
    {
        if (musicSource.clip == music)
            return;

        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}