using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private int score = 0;

    void Start()
    {
        textMeshPro.text = score.ToString();
        EnemyController.onEnemyDie += IncreaseScore;

        StartCoroutine(AddScoreEverySecond());
    }

    IEnumerator AddScoreEverySecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            IncreaseScore(1);
        }
    }

    public void IncreaseScore (int amount)
    {
        score += amount;
        textMeshPro.text = score.ToString();
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    void OnDestroy()
    {
        EnemyController.onEnemyDie -= IncreaseScore;
    }
}