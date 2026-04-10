using UnityEngine;
namespace Lesson2 {
    public class GameManager : MonoBehaviour {
        public int totalCoins = 0;

        public void AddCoin(int coinValue) {
            totalCoins += coinValue;
        }
    }
}