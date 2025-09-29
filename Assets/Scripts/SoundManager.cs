using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip jumpClip;
    public AudioClip clickClip;
    public AudioClip gameOverClip;
    public AudioClip getStarClip;
    public AudioClip bgmClip;

    private AudioSource sfxSource;
    private AudioSource bgmSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        PlayBGM();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    void PlayBGM()
    {
        if (bgmClip == null) return;
        if (bgmSource.clip == bgmClip && bgmSource.isPlaying) return;

        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    void StopBGM()
    {
        bgmSource.Pause();
    }

    public void PlayJump() => PlaySFX(jumpClip);
    public void PlayClick() => PlaySFX(clickClip);
    public void PlayGameOver() => PlaySFX(gameOverClip);
    public void PlayGetStar() => PlaySFX(getStarClip);
}
