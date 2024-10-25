using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Update()
    {
        scoreText.text = GM.score.ToString();
    }
}
