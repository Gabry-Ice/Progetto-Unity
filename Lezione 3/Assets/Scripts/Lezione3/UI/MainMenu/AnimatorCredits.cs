using System.Collections;
using UnityEngine;

namespace MiciomaXD
{
    /// <summary>
    /// Manages the opening and closing animation of the credits panel in the main menu. The animation is performed by moving the panel from its original position (-255, 0) to the (0, 0) target position and vice versa (pay attention to where the panel pivot is located).
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class AnimatorCredits : MonoBehaviour
    {
        public float animSpeed = 500;

        RectTransform rectTransform;

        bool isInProgress;
        bool isOpen;

        Vector2 originalPosition;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            isInProgress = false;
            isOpen = false;
            originalPosition = rectTransform.anchoredPosition;
        }

        public void PerformCreditsAnimation()
        {
            if (isInProgress)
            {
                return;
            }

            if (isOpen)
                PerformClosing();
            else
                PerformOpening();
        }

        void PerformOpening()
        {
            isInProgress = true;
            Vector2 toReach = new Vector2(0f, rectTransform.anchoredPosition.y);
            StartCoroutine(DoPerformMovement(toReach));
        }

        void PerformClosing()
        {
            isInProgress = true;
            StartCoroutine(DoPerformMovement(originalPosition));
        }

        private IEnumerator DoPerformMovement(Vector2 toReach)
        {
            while (Vector2.Distance(rectTransform.anchoredPosition, toReach) > 0.01f)
            {
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, toReach, animSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            rectTransform.anchoredPosition = toReach;
            isInProgress = false;
            isOpen = !isOpen;
        }
    }
}