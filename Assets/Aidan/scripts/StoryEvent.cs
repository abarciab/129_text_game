using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryEvent
{
    [System.Serializable]
    public class Chapter
    {
        public int chapterID;
        [TextArea(2, 4)]
        public List<string> intro;
        [TextArea(2, 4)]
        public string prompt;
        [Space]
        public bool isFinal;
        public string option1;
        public int option1Dest;
        public string option2;
        public int option2Dest;
        public bool deleteLast;
        public bool deleteSecondToLast;
    }
    [SerializeField] List<Chapter> chapters = new List<Chapter>();
    int currentChapter;

    public List<string> GetNextTextBlock()
    {
        return chapters[currentChapter].intro;
    }
    public string GetOption1()
    {
        return chapters[currentChapter].option1;
    }
    public string GetOption2()
    {
        return chapters[currentChapter].option2;
    }
    public string GetPrompt()
    {
        return chapters[currentChapter].prompt;
    }
    public bool IsFinalChapter()
    {
        return chapters[currentChapter].isFinal;
    }
    public void MakeChoice(int choice)
    {
        if (choice == 1) ChoseOption1();
        else ChoseOption2();
    }

    public bool deleteLast() { return chapters[currentChapter].deleteLast; }
    public bool deleteSecondToLast(){ return chapters[currentChapter].deleteSecondToLast; }
    void ChoseOption1()
    {
        currentChapter = chapters[currentChapter].option1Dest;
    }
    void ChoseOption2()
    {
        currentChapter = chapters[currentChapter].option2Dest;
    }

}
