using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using UnityEngine.Events;

//Made by Fran Klasic

public class StartScreenUI : MonoBehaviour
{
    public Button StartButton;
    public Button Quit;
    public AudioClip song;
    private AudioSource audioSource;
    private float fadeDuration = 2f;
    public Image loadScreen;
    void OnQuitButtonPressed()
    {
        MoveLogger.ResetJson();
        Application.Quit();
    }

    void OnStartButtonPressed()
    {
        MoveLogger.ResetJson();
        StartCoroutine(FadeOutAndLoadScene("Chess2dPart"));
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = song;
        audioSource.loop = true;
        audioSource.volume = 0.03f;
        audioSource.Play();

        Color color = loadScreen.color;
        color.a = Mathf.Clamp01(0f);
        loadScreen.color = color;
        loadScreen.gameObject.SetActive(false);

        if (Quit != null)
        {
            Quit.onClick.AddListener(OnQuitButtonPressed);
        }
        if (StartButton != null)
        {

            StartButton.onClick.AddListener(OnStartButtonPressed);
        }
    }
    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        loadScreen.gameObject.SetActive(true);

        float startVolume = audioSource.volume;
        float time = 0f;

        Color color = loadScreen.color;
        color.a = Mathf.Clamp01(0f);
        loadScreen.color = color;


        while (time < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);

            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            loadScreen.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneName);
    }

}
