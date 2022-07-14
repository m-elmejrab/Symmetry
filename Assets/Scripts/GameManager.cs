using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    int correctItems = 0;
    int totalItems = 0;
    GameObject level;
    bool initialized = false;
    List<GameLevel> levels = new List<GameLevel>();
    int currentLevel = 0;
    bool isPlaying = false;
    float totalPlayTime = 0f;


    // Use this for initialization
    void Start()
    {

        PrepareLevelsData();
        InitializeGame(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        SceneManager.sceneLoaded += InitializeGame;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlaying)
        {
            totalPlayTime += Time.fixedDeltaTime;
        }
    }

    void InitializeGame(Scene loadedScene, LoadSceneMode mode)
    {
        initialized = true;
        isPlaying = true;
        totalPlayTime = 0f;

        UIManager.instance.UpdateLevelHint(levels[currentLevel].question);
        totalItems = 0;
        level = GameObject.FindGameObjectWithTag("LevelObject");
        foreach (Transform ts in level.transform)
        {
            totalItems++;
        }

        List<Transform> levelObjects = level.GetComponentsInChildren<Transform>().ToList();

        foreach (Transform ts in levelObjects)
        {
            if (ts.tag != "LevelObject")
            {

                Rotatable r = ts.gameObject.GetComponent<Rotatable>();
                if (r != null)
                {
                    r.statusChanged += ObjectChanged;
                    if (r.IsCorrect())
                        correctItems++;
                }
                else
                {
                    RotatableBend r2 = ts.gameObject.GetComponent<RotatableBend>();
                    r2.statusChanged += ObjectChanged;

                    if (r2.IsCorrect())
                        correctItems++;
                }
            }
        }


    }

    public void ObjectChanged(bool isCorrect)
    {
        if (isCorrect)
        {
            correctItems++;
        }
        else
        {
            correctItems--;
        }

        if (correctItems == totalItems)
        {

            string winningText = "Well done, that took you \n" + totalPlayTime.ToString("F1") + " Seconds\n\n\n";
            UIManager.instance.UpdateLevelHint(winningText + "\"" + levels[currentLevel].message + "\"");
            currentLevel++;
            isPlaying = false;
            Invoke("ChangeScene", 5f);
        }

    }

    void ChangeScene()
    {

        if (currentLevel < 5)
        {
            initialized = false;
            correctItems = 0;
            totalItems = 0;
            SceneManager.LoadScene(currentLevel);
        }


    }



    private void PrepareLevelsData()
    {
        GameLevel eightLevel = new GameLevel(0, "Eight", "Tap to rotate, until it's eight", "Drink water ;)");
        GameLevel noteLevel = new GameLevel(1, "Note", "A note can be more than a piece of paper", "Appreciate different tastes");
        GameLevel tvLevel = new GameLevel(4, "TV", "We watch it all day", "I'm afraid this is the end of our journey.\n more levels to come later");
        GameLevel stickmanLevel = new GameLevel(2, "Stickman", "It could be me or it could be you, just made of sticks", "Things don't have to be complicated to be helpful");
        GameLevel doorLevel = new GameLevel(3, "Door", "I wonder what's behind?", "Don't be afraid to open new doors");


        levels.Add(eightLevel);
        levels.Add(noteLevel);
        levels.Add(stickmanLevel);
        levels.Add(doorLevel);
        levels.Add(tvLevel);
    }

    private class GameLevel
    {
        public int index;
        public string name;
        public string question;
        public string message;

        public GameLevel() { }
        public GameLevel(int i, string s, string q, string m)
        {
            index = i;
            name = s;
            question = q;
            message = m;
        }


    }
}
