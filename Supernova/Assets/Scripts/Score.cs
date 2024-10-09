using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public GameObject scoreObject;
    public TMP_Text scoreText;
    void Start()
    {
        scoreText = scoreObject.GetComponent<TMP_Text>();
        UpdateScore();
    }
    public void UpdateScore()
    {
        int score = GM.Instance.score;
        scoreText.text = score.ToString();
    }
}
