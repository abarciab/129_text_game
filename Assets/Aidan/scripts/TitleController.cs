using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource hoverSource;
    [SerializeField] AudioSource clickSource;
    [SerializeField] TextMeshProUGUI titleText;
    string originalTitle;
    int nextScene = -1;
    [SerializeField] float musicThreshold = 0.01f;
    [SerializeField] RectTransform wipe;
    [SerializeField] float finalScale;
    [SerializeField] float wipeSpeed = 0.00001f;

    private void Start()
    {
        originalTitle = titleText.text;
    }

    public void Hover()
    {   
        if (nextScene != -1) return;
        hoverSource.Play();
    }

    public void StartGame(int _nextScene)
    {
        if (nextScene != -1) return;
        nextScene = _nextScene;
        clickSource.Play();
        StartCoroutine(FadeAndGo());
    }

    public void ShowCredits()
    {
        clickSource.Play();
        titleText.text = "credits";
    }

    public void HideCredits()
    {
        clickSource.Play();
        titleText.text = originalTitle;
    }


    IEnumerator FadeAndGo()
    {
        while (musicSource.volume > musicThreshold) {
            yield return new WaitForEndOfFrame();
            wipe.localScale = Vector3.Lerp(wipe.localScale, Vector3.one * finalScale, wipeSpeed);
            musicSource.volume = Mathf.Lerp(musicSource.volume, 0, 0.025f);
        }
        SceneManager.LoadScene(nextScene);
    }

}
