using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager3D : MonoBehaviour
{
    public AudioClip[] songs;
    private AudioSource audioSource;
    public float fadeDuration = 2f;
    public Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomSong();
    }

    void PlayRandomSong()
    {
        if (songs.Length == 0) return;

        AudioClip chosen = songs[Random.Range(0, songs.Length)];
        StartCoroutine(FadeInMusic(chosen));

        StartCoroutine(WaitForOutro());
    }

    IEnumerator FadeInMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.volume = 0f;
        audioSource.Play();

        float time = 0f;
        while (time < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(0f, 0.09f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0.09f;
    }

    IEnumerator FadeOutMusic()
    {
        audioSource.volume = 0.09f;

        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;

        SceneManager.LoadScene("EndScreenUI");
    }

    IEnumerator WaitForOutro()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Outro01"))
            yield return null;

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
        StartCoroutine(FadeOutMusic());
    }
}
