using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instace;
    void Awake() { instace = this; }

    [SerializeField] int nextLevel = 0;
    [SerializeField] List<int> levelBuildIndices = new List<int>();
    [SerializeField] GameObject textParent;
    [SerializeField] TextMeshProUGUI textPrompt;
    [SerializeField] TextMeshProUGUI GameplayPrompt;
    [HideInInspector] public PromptScramble scrambleScript;
    [HideInInspector] public TextController textScript;
    bool choiceMade;

    public void NextEvent()
    {
        nextLevel += 1;
        if (nextLevel >= levelBuildIndices.Count) SceneManager.LoadScene(8);
    }

    public void StartNextLevel()
    {
        GetComponent<AudioSource>().Play();
        choiceMade = false;
        GameplayPrompt.gameObject.SetActive(true);
        GameplayPrompt.text = textPrompt.text;
        textParent.gameObject.SetActive(false);

        if (levelBuildIndices.Count <= nextLevel) return;
        SceneManager.LoadScene(levelBuildIndices[nextLevel], LoadSceneMode.Additive);
    }

    public void CompleteScene(int buildIndex = -1)
    {
        if (buildIndex == -1) buildIndex = levelBuildIndices[nextLevel];
        SceneManager.UnloadSceneAsync(buildIndex);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        GameplayPrompt.gameObject.SetActive(false);
    }

    public void MakeChoice(int choice)
    {
        if (choiceMade) return;
        GetComponent<AudioSource>().Play();
        choiceMade = true;
        textScript.MakeChoice(choice);
    }
}
