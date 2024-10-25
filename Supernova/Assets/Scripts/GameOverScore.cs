using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {
        scoreText.text = GM.score.ToString();
    }
}
