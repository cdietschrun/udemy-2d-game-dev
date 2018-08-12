using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] sprites;

	// Use this for initialization
	void Start ()
    {
        var renderer = GetComponent<SpriteRenderer>();

        if (sprites.Length > 0)
        {
            renderer.sprite = sprites[Random.Range(0, sprites.Length)];
            renderer.sortingOrder = Random.Range(1, 11);
        }
	}
}
