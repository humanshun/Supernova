using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource; // BGMを再生するAudioSourceをアタッチ
    public AudioClip bgmClip; // 再生したいBGMのAudioClipをアタッチ

    public AudioClip selectClip;
    public AudioClip pressClip;
    public AudioClip planetClip;

    private void Awake()
    {
        // シングルトンの設定
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (audioSource != null && bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.loop = true; // ループ再生を有効にする
            audioSource.Play();
        }
    }

    public void StopBGM()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void SelectSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(selectClip);
        }
    }
    public void pressSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(pressClip);
        }
    }
    public void planetSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(planetClip);
        }
    }
}
