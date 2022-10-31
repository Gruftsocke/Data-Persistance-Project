using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button exitButton = null;
    [SerializeField] private TMP_InputField inputText = null;
    [SerializeField] private UserStatistics userStatistics = null;

    private const int MIN_CHAR_LENGTH = 3;
    private const int MAX_CHAR_LENGTH = 14;

    private void Start()
    {
        startButton.onClick.AddListener(OnStart);
        exitButton.onClick.AddListener(OnExit);
        inputText.onValueChanged.AddListener(OnEnterText);

        if (GameManager.Current)
        {
            inputText.text = GameManager.Current.Username;

            userStatistics.SetHightscoreList(GameManager.Current.HighscoreList);
        }
    }

    public static string FirstCharToUpper(string input)
    {
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("ARGH!");
        return input.First().ToString().ToUpper() + input.Substring(1);
    }

    private void OnEnterText(string value)
    {
        startButton.interactable = value.Length >= MIN_CHAR_LENGTH;
        string temp = value;

        if (!string.IsNullOrEmpty(value) && char.IsLetter(value[0]))
            temp = FirstCharToUpper(value);

        if (temp.Length > MAX_CHAR_LENGTH)
            inputText.text = temp.Substring(0, MAX_CHAR_LENGTH);
        else
            inputText.text = temp;
    }

    private void OnStart()
    {
        GameManager.Current.Username = inputText.text;
        SceneManager.LoadScene(1);
    }

    private void OnExit()
    {
        GameManager.Current.SaveData();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
       Application.Quit();
#endif
    }
}
