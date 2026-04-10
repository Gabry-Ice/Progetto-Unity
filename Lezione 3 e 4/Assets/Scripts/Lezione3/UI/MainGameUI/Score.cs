using TMPro;
using UnityEngine;

namespace MiciomaXD
{
    public class Score : MonoBehaviour
    {
        public TMP_Text score;
        int lastScore;

        public void SetScore(int scoreVal)
        {
            lastScore = scoreVal;
            score.text = scoreVal.ToString("D6");
        }

        public int GetScore()
        {
            return lastScore;
        }

        public void AddScore(int scoreVal)
        {
            lastScore += scoreVal;
            score.text = lastScore.ToString("D6");
        }
    }
}