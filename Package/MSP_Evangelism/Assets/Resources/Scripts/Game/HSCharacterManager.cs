using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSCharacterManager : HSSingleton<HSCharacterManager>
{
    private bool bIsActive;

    private Transform trans;
    private Rigidbody2D rigid2D;
    private Vector3 vRot;
    
    void Start()
    {
        trans = this.transform;
        rigid2D = this.GetComponent<Rigidbody2D>();
        vRot = new Vector3();
    }   

    void Update()
    {
        if (bIsActive && Input.GetMouseButtonDown(0))
            BirdClick();
        if (!bIsActive)
            trans.localPosition = Vector3.Lerp(trans.localPosition, HSGameManager.I.vDefCharPos, Time.deltaTime * 3f);
    }

    void FixedUpdate()
    {
        if(bIsActive)
            BirdRotation();
    }

    public void Activate()
    {
        bIsActive = true;
        trans.localPosition = HSGameManager.I.vDefCharPos;
        trans.localEulerAngles = Vector3.zero;
        rigid2D.gravityScale = 0.5f;
    }

    public void Reset()
    {
        HSRankUI.I.SendValuesToServer();
        bIsActive = false;
        rigid2D.velocity = Vector2.zero;
        trans.localEulerAngles = Vector3.zero;
        rigid2D.gravityScale = 0.0f;
    }

    void BirdRotation()
    {
        float degrees = 0.0f;

        if (rigid2D.velocity.y > 0)
            degrees = 6.0f * HSGameManager.I.fCharRotationUpSpeed;
        else
            degrees = -3.0f * HSGameManager.I.fCharRotationDownSpeed;

        vRot = new Vector3(0f, 0f, Mathf.Clamp(vRot.z + degrees, -90, 45));
        trans.eulerAngles = vRot;
    }

    void BirdClick()
    {
        HSAudioControlMng.I.PlayFX("sfx_wing", false);
        rigid2D.velocity = new Vector2(0f, HSGameManager.I.fCharJumpPower);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!bIsActive)
            return;

        if (col.tag.Equals("Score"))
        {
            HSGameManager.I.nScore++;
            HSGameManager.I.ScoreText.text = HSGameManager.I.nScore.ToString();
            HSAudioControlMng.I.PlayFX("sfx_point", false);
        }
        else
        {
            HSGameManager.I.eMenuState = E_HS_MENU_STATE.E_MENU;
            Reset();
            HSPillarManager.I.EndGame();
        }
    }
}
