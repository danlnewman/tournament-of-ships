using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpriteAnimator : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    int numberOfFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = sprites[(int)(Time.time * 10f)%numberOfFrames];
    }
}
