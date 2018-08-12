using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGem : MonoBehaviour
{
    public AudioClip soundEffect;

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D target)
    {
        PersistentManager.dataStore.gemsCollected += 1;

        if (target.gameObject.tag == "Player")
        {
            if (soundEffect)
            {
                AudioSource.PlayClipAtPoint(soundEffect, transform.position);
            }

            Destroy(gameObject);
        }
    }
}
