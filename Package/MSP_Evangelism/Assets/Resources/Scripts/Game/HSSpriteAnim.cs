using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSSpriteAnim : MonoBehaviour
{
    public Sprite[] SpriteAnim;
    private Image Img;
    private int AnimIdx;
    private float fTimer;

    void Start()
    {
        Img = this.GetComponent<Image>();
        AnimIdx = 0;
        fTimer = 0f;
    }

    void Update()
    {
        fTimer += Time.deltaTime;

        if (fTimer > 0.3f)
        {
            AnimIdx++;
            if (AnimIdx == SpriteAnim.Length)
                AnimIdx = 0;

            fTimer -= 0.3f;
            Img.sprite = SpriteAnim[AnimIdx];
        }
    }
}
