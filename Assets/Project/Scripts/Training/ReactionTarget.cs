using UnityEngine;

public class ReactionTarget : MonoBehaviour
{
    public System.Action onReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onReached?.Invoke();
            gameObject.SetActive(false);
        }
    }
}