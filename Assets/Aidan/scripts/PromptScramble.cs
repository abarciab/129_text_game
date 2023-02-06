using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptScramble : MonoBehaviour
{
    TextMeshProUGUI promptText;
    [SerializeField] string FontTag;
    [SerializeField] float speed = 0.1f;
    bool ready;

    private void Start()
    {
        GameManager.instace.scrambleScript = this;
        promptText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!ready) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            ready = false;
            GameManager.instace.textScript.DisplayNextChapter();
        }
    }

    public void Unscramble()
    {
        StartCoroutine(UnscrableText());
    }

    IEnumerator UnscrableText()
    {
        string text = promptText.text;
        string displayText = "";

        for (int i = 0; i <= text.Length; i++) {
            string lastPart = text.Substring(i);
            if (lastPart.Length == 0) {
                displayText = FontTag + text;
                break;
            }
            string firstPart = text.Replace(lastPart, "");
            displayText = FontTag + firstPart + "</font>" + lastPart;
            promptText.text = displayText;
            GameManager.instace.textScript.textSFX.Play();

            yield return new WaitForSeconds(speed);
        }
        promptText.text = displayText;
        ready = true;
    }
}
