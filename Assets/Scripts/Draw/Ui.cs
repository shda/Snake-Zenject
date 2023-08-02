using System;
using TMPro;
using UnityEngine;

namespace Draw
{
    public class Ui : MonoBehaviour , IUiDraw
    {
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private TextMeshProUGUI gameOverLabel;

        private void Awake()
        {
            gameOverLabel.gameObject.SetActive(false);
        }

        public void SetScore(int score)
        {
            scoreLabel.text = $"Score: {score}";
        }

        public void ShowGameOver()
        {
            gameOverLabel.gameObject.SetActive(true);
        }
    }
}