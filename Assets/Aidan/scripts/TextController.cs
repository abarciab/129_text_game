using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{

    [SerializeField, Header("Dependencies")]
    TextMeshProUGUI introText;
    [SerializeField] TextMeshProUGUI mainText, prompt, decodedPrompt, chosenResponse, gameplayPrompt;
    [SerializeField] GameObject textParent, nextEventButton, promptButton;
    public AudioSource textSFX;
    [SerializeField] AudioSource promptAppearSFX;

    [Space(20)]
    [SerializeField] float textSpeed = 0.01f;
    [SerializeField] List<StoryEvent> storyEvents = new List<StoryEvent>();
    int storyIndex;
    List<string> currentTextBlock = new List<string>();
    TextMeshProUGUI currentTextBox;
    string addedText;
    string lastPrompt;
    string lastChoice;
    bool donewText;
    bool displayingText;

    private void Start()
    {
        StartStoryEvent();
        GameManager.instace.textScript = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) SkipText();
    }

    public void ClickPrompt()
    {
        textParent.SetActive(false);
        gameplayPrompt.gameObject.SetActive(true);
        gameplayPrompt.text = storyEvents[storyIndex].GetPrompt();
    }

    public void StartNextStory()
    {
        GameManager.instace.NextEvent();
        storyIndex += 1;
        if (storyEvents.Count > storyIndex) StartStoryEvent();
    }

    public void StartStoryEvent()
    {
        ClearAllText();
        HideAllObjects();

        textParent.SetActive(true);
        introText.gameObject.SetActive(true);

        currentTextBox = introText;
        currentTextBlock = storyEvents[storyIndex].GetNextTextBlock();
        StartCoroutine(AddText(currentTextBlock[0]));
    }
    public void MakeChoice(int choiceNum)
    {
        lastPrompt = storyEvents[storyIndex].GetPrompt();
        lastChoice = choiceNum == 1 ? storyEvents[storyIndex].GetOption1() : storyEvents[storyIndex].GetOption2();
        storyEvents[storyIndex].MakeChoice(choiceNum);
        gameplayPrompt.GetComponent<PromptScramble>().Unscramble();
    }
    public void DisplayNextChapter()
    {
        GameManager.instace.CompleteScene();
        ClearAllText();
        HideAllObjects();

        textParent.SetActive(true);
        decodedPrompt.gameObject.SetActive(true);
        decodedPrompt.text = lastPrompt;

        chosenResponse.gameObject.SetActive(true);
        chosenResponse.text = "> " + lastChoice;

        mainText.gameObject.SetActive(true);
        currentTextBlock = storyEvents[storyIndex].GetNextTextBlock();
        currentTextBox = mainText;
        StartCoroutine(AddText(currentTextBlock[0]));
    }
    public string GetOption1()
    {
        return storyEvents[storyIndex].GetOption1();
    }
    public string GetOption2()
    {
        return storyEvents[storyIndex].GetOption2();
    }

    void SkipText()
    {
        if (!displayingText && currentTextBlock.Count > 0) {
            StartCoroutine(AddText(currentTextBlock[0]));
            return;
        }

        displayingText = false;
        StopAllCoroutines();
        if (!textParent.activeInHierarchy) return;
        if (currentTextBlock.Count > 0) {
            currentTextBox.text += currentTextBlock[0].Replace(addedText, "");
            currentTextBlock.RemoveAt(0);
            DoneDisplaying();
        }

        if (currentTextBlock.Count > 0) return;
        if (!donewText) { donewText = true; return; }
        if (storyEvents[storyIndex].IsFinalChapter()) {
            ShowNextEventButton();
            return;
        }
        ShowPrompt();
    }
    void ClearAllText()
    {
        introText.text = mainText.text = prompt.text = decodedPrompt.text = chosenResponse.text = gameplayPrompt.text = "";
    }
    void HideAllObjects()
    {
        introText.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
        promptButton.SetActive(false);
        decodedPrompt.gameObject.SetActive(false);
        chosenResponse.gameObject.SetActive(false);
        gameplayPrompt.gameObject.SetActive(false);
        textParent.SetActive(false);
        nextEventButton.SetActive(false);
    }

    IEnumerator AddText(string textToDisplay)
    {
        displayingText = true;
        addedText = "";
        for (int i = 0; i < textToDisplay.Length; i++) {
            currentTextBox.text += textToDisplay[i];
            addedText += textToDisplay[i];
            textSFX.pitch = Random.Range(1.5f, 1.8f);
            textSFX.Play();
            yield return new WaitForSeconds(textSpeed);
        }
        if (currentTextBlock.Contains(textToDisplay)) currentTextBlock.Remove(textToDisplay);
        displayingText = false;
        DoneDisplaying();
    }

    void DoneDisplaying()
    {
        
        if (currentTextBlock.Count > 0) {
            currentTextBox.text += "\n\n";
            return;
        }
    }
    void ShowPrompt()
    {
        if (storyEvents[storyIndex].deleteLast() && storyEvents.Count == 7) storyEvents.RemoveAt(storyEvents.Count - 1);
        if (storyEvents[storyIndex].deleteSecondToLast() && storyEvents.Count == 7) storyEvents.RemoveAt(storyEvents.Count - 2);
        if (!promptButton.activeInHierarchy) promptAppearSFX.Play();
        promptButton.SetActive(true);
        prompt.text = storyEvents[storyIndex].GetPrompt();
    }
    void ShowNextEventButton()
    {
        promptAppearSFX.Play();
        nextEventButton.SetActive(true);
    }

}
