using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserStatistics : MonoBehaviour
{
    [SerializeField] private Button prevButton = null;
    [SerializeField] private Button nextButton = null;

    private ScoreItemContainer[] items = null;
    private int currentPageIndex = 0;
    private int maxItemCount = 0;
    private List<GameManager.UserData> userDatas = null;

    private void Awake()
    {
        items = GetComponentsInChildren<ScoreItemContainer>();

        prevButton.onClick.AddListener(OnPrevPage);
        nextButton.onClick.AddListener(OnNextPage);

        maxItemCount = items.Length;

        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void SetHightscoreList(List<GameManager.UserData> userDatas)
    {
        foreach (var item in items)
            item.gameObject.SetActive(false);

        if (userDatas.Count > 0)
        {
            userDatas.Sort((a,b) => a.score.CompareTo(b.score));
            userDatas.Reverse();

            int count = Mathf.Min(userDatas.Count, items.Length);

            for (int i = 0; i < count; i++)
            {
                items[i].SetNumber(i + 1);
                items[i].SetUsername(userDatas[i].username);
                items[i].SetScore(userDatas[i].score);
                items[i].gameObject.SetActive(true);
            }
        }

        this.userDatas = userDatas;
    }

    private void OnNextPage()
    {

    }

    private void OnPrevPage()
    {
        if (currentPageIndex == 0)
            return;

        currentPageIndex--;
        prevButton.interactable = currentPageIndex > 0;
    }
}
