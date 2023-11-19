using UnityEngine;
using UnityEngine.Events;

namespace HuangQiaoxin.Lab3
{
    public class EventManager : MonoBehaviour
    {
        // Declare events when the game is over and when the score needs to be updated
        public static readonly UnityEvent onGameOver = new UnityEvent();
        public static readonly UnityEvent onUpdateScore = new UnityEvent();
    }
}
