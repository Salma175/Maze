using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var rb = other.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezeAll;

            GameEvents.LevelComplete();

            AudioManager.Instance.PlaySFX(AudioClipName.LevelSuccess);
        }
    }
}
