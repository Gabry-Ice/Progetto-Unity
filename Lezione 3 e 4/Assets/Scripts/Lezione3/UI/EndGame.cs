using TMPro;
using UnityEditor.Animations;
using UnityEngine;

namespace MiciomaXD
{
    [RequireComponent(typeof(CanvasGroup))]
    public class EndGame : MonoBehaviour
    {
        [SerializeField] DisplayTimer timer;
        [SerializeField] Score score;
        [SerializeField] CanvasGroup endGameCanvasGroup;
        [SerializeField] TMP_Text finalMessage;
        [SerializeField] TMP_Text finalSubMessage;
        [SerializeField] TMP_Text finalScore;
        [SerializeField] CanvasGroup mainGameUICanvasGroup;
        [SerializeField] Animator badgeAnimator;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (score == null)
                score = FindFirstObjectByType<Score>();
            if (endGameCanvasGroup == null)
                endGameCanvasGroup = GetComponent<CanvasGroup>();
            if (timer == null)
                timer = FindFirstObjectByType<DisplayTimer>();

            badgeAnimator.speed = 0;

            ResetEndGameCanvas();
        }

        private void ResetEndGameCanvas()
        {
            endGameCanvasGroup.alpha = 0;
            endGameCanvasGroup.interactable = false;
            endGameCanvasGroup.blocksRaycasts = false;
        }

        public void OnTimerElapsed() => EndGameRoutine();
        public void OnHealthDepleted() => EndGameRoutine();
        public void OnAllEnemyesKilled() => EndGameRoutine();

        private void EndGameRoutine()
        {
            mainGameUICanvasGroup.alpha = 0;
            timer.StopTimer();

            EndGameState endState = CheckWinningConditions();
            //GameObject.FindGameObjectWithTag("Player").SetActive(false);
            Time.timeScale = 0;

            switch (endState)
            {
                case EndGameState.LoseTimer:
                    finalMessage.text = "Hai Perso!";
                    finalSubMessage.text = "Elimina i nemici prima che il tempo finisca!";
                    break;
                case EndGameState.LoseDead:
                    finalMessage.text = "Hai Perso!";
                    finalSubMessage.text = "Schiva i proiettili dei nemici per non morire!";
                    break;
                case EndGameState.Win:
                    finalMessage.text = "Hai Vinto!";
                    finalSubMessage.text = "Hai eliminato tutti i nemici prima che il tempo finisse! Bravo/a!!!";
                    break;
                default:
                    break;
            }

            finalScore.text = score.GetScore().ToString("D6");

            ShowEndGameCanvas(endState == EndGameState.Win);
        }

        private EndGameState CheckWinningConditions()
        {
            PlayerState playerState = FindAnyObjectByType<PlayerState>(FindObjectsInactive.Include);

            if (playerState.curHP <= 0)
                return EndGameState.LoseDead;

            if (!playerState.AreEnemiesDead())
                return EndGameState.LoseTimer;

            // player is alive and has killed all enemyes, so it's a win
            return EndGameState.Win;
        }

        private void ShowEndGameCanvas(bool won)
        {
            endGameCanvasGroup.alpha = 1;
            badgeAnimator.speed = 1;

            if (won)
                badgeAnimator.SetTrigger("Won");
            else
                badgeAnimator.SetTrigger("Lost");
        }
    }
}