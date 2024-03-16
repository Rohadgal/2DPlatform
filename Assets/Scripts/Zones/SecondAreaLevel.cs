using UnityEngine;

public class SecondAreaLevel : MonoBehaviour
{
    bool eventRaised = false;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !eventRaised) {
            EventManager.RaiseEvent();
            eventRaised = true;
        }
    }
}
