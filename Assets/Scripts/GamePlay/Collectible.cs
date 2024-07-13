using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    internal void ResetUi()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(AudioClipName.Coin);
            gameObject.SetActive(false);
        }
    }
}
