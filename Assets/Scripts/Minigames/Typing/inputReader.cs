using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class inputReader : MonoBehaviour
{
    [Header("Possible Code Syntax Lists:")]
    public string[] possiblePhrases;
    public List<string> StringPattern { get { return stringPattern; } }
    private List<string> stringPattern;
    [Space]

    [Header("For Current Game Style, Keep at 1")]
    public int stringAmount;


    //string index
    public int CurrentIndex { get { return currentIndex; } }
    private int currentIndex = 0;

    //character index:
    public int CurrentCharacterIndex { get { return currentCharacterIndex; } }
    private int currentCharacterIndex = 0;

    [Header("General Variables:")]
    public float waitTime; //float used for the amount of seconds of delay when putting the wrong input.

    private bool canInput = true; //bool toggle when an input is messed up working alongside the waitTime.

    private void Awake()
    {
        stringPattern = new List<string>();
    }
    void Start()
    {
        CreatePattern();
    }


    void Update()
    {
        if (canInput)
        {
            CheckInput();
        }
    }

    //ADJUSTED TO ALLOW ALL CHARACTERS NUMBERS ETC:
    void CheckInput()
    {
        if (currentIndex >= stringPattern.Count) return;

        string currentPhrase = stringPattern[currentIndex];

        if (!string.IsNullOrEmpty(Input.inputString))
        {
            foreach (char c in Input.inputString)
            {
                char requiredKey = currentPhrase[currentCharacterIndex];

                // normalize both to lowercase for case-insensitive compare
                if (char.ToLower(c) == char.ToLower(requiredKey))
                {
                    Debug.Log("Correct Key Entered!!");
                    currentCharacterIndex++;

                    if (currentCharacterIndex >= currentPhrase.Length)
                    {
                        currentCharacterIndex = 0;
                        currentIndex++;

                        if (currentIndex >= stringPattern.Count)
                        {
                            ExitMinigame();
                        }
                    }
                }
                else
                {
                    Debug.Log("WRONG INPUT KEY! WAIT FOR COOLDOWN!");
                    StartCoroutine(DisableInputs());
                }

                break; // process only one char per frame
            }
        }
    }

    IEnumerator DisableInputs()
    {
        canInput = false;
        yield return new WaitForSeconds(waitTime);
        canInput = true;
    }


    public void CreatePattern()
    {
        if (stringPattern == null)
        {
            stringPattern = new List<string>();
        }

        stringPattern.Clear();
        currentIndex = 0;
        currentCharacterIndex = 0;

        if (possiblePhrases == null || possiblePhrases.Length == 0)
        {
            Debug.Log("NO PHRASES AVAILABLE!!!");
            return;
        }

        //CREATES A FOR LOOP IN WHICH BASED ON THE PATTERN LENGTH, CHOOSES A RANDOM KEYCODE FROM POSSIBLE INPUTS, THEN ADDS IT TO THE ARRAY OF THE INPUT PATTERN, REPEATS UNTIL PATTERN LENGTH IS REACHED.
        for (int i = 0; i < stringAmount; i++)
        {
            string randomString = possiblePhrases[Random.Range(0, possiblePhrases.Length)];
            stringPattern.Add(randomString);
        }

        Debug.Log("NEW LIST CREATED!!");
    }


    void ExitMinigame()
    {
        BadTypingMinigame wrapper = GetComponentInParent<BadTypingMinigame>();

        if (wrapper != null)
        {
            wrapper.OnCompleted();
        }
    }
}
