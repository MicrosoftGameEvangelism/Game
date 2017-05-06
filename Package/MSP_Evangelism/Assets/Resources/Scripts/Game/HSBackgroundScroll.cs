using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSBackgroundScroll : MonoBehaviour
{
    /// <summary>
    /// 스크롤 시킬 배경 이미지
    /// </summary>
    private RawImage rawImg;
    /// <summary>
    /// uvRect
    /// </summary>
    private Rect uvRect;
    /// <summary>
    /// 스크롤 속도입니다
    /// </summary>
    public float fScrollSpeed;

    void Start()
    {
        // 초기화 및 RawImage 컴포넌트를 가져옵니다.
        rawImg = this.GetComponent<RawImage>();
        uvRect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
    }

    void Update()
    {
        // 선언한 uvRect변수에 스크롤된 값을 넣어서 rawImg변수의 uvRect에 대입합니다.
        uvRect.x += fScrollSpeed * Time.deltaTime;
        rawImg.uvRect = uvRect;
    }
}
