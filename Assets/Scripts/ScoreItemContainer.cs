using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreItemContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText = null;
    [SerializeField] private TMP_Text usernameText = null;
    [SerializeField] private TMP_Text scoreText = null;

    private static readonly int MAX_NUMBERS = 9999;
    private static readonly int MAX_SCORE_NUMBERS = 999999999;

    public void SetNumber(int number)
    {
        if (number > MAX_NUMBERS)
            return;

        numberText.text = number.ToString();
    }

    public void SetScore(int score)
    {
        if (score > MAX_SCORE_NUMBERS)
            return;

        scoreText.text = score.ToString();
    }

    public void SetUsername(string name)
    {
        usernameText.text = name;
    }
}
