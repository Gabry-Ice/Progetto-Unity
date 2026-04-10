using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MiciomaXD
{
    /// <summary>
    /// Controls a countdown timer that updates a UI text element with the remaining time in minutes and seconds.
    /// </summary>
    /// <remarks>The timer starts counting down from the specified 'countdownFrom' value in seconds. When the
    /// countdown reaches zero, it invokes the associated UnityEvent. The timer updates every frame, ensuring the
    /// displayed time is accurate.</remarks>
    public class DisplayTimer : MonoBehaviour
    {
        public TMP_Text timerText;
        public float countdownFrom = 10;
        public UnityEvent timerEvent;

        int[] cachedFormattedTimeArray = new int[2];

        // Update is called once per frame
        void Update()
        {
            countdownFrom -= Time.deltaTime;
            countdownFrom = Mathf.Max(countdownFrom, 0);
            sec2Formatted(countdownFrom, cachedFormattedTimeArray);
            timerText.text = $"{cachedFormattedTimeArray[0]}m {cachedFormattedTimeArray[1]}s";

            if (countdownFrom <= 0)
            {
                timerEvent.Invoke();
                StopUpdate();
            }
        }

        /// <summary>
        /// Delegated array allocation to the caller since this is hot code.
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="cachedArray"></param>
        void sec2Formatted(float seconds, int[] cachedArray)
        {
            cachedArray[0] = (int)seconds / 60;
            cachedArray[1] = (int)seconds - cachedArray[0] * 60;
        }

        void StopUpdate() => enabled = false;

        public void StopTimer() => StopUpdate();
    }
}