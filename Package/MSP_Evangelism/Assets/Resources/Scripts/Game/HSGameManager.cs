using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum E_HS_MENU_STATE
{
    E_MENU,
    E_RANK,
    E_GAME, 
    E_MAX
}

[System.Serializable]
public class UserName
{
    public string First;
    public string Second;
    public string Third;
    public string Fourth;

    public string makeNickname()
    {
        return First + Second + Third + Fourth;
    }
}

public class HSGameManager : HSSingleton<HSGameManager>
{
    void Start()
    {
        HSAudioControlMng.I.PlayBGM("Background", true);
        #region Score
        if(GameObject.Find("0_Score") != null)
        {
            ScoreText = GameObject.Find("0_Score").GetComponent<Text>();
        }
        #endregion
    }

    [Header("Menu")]
    public E_HS_MENU_STATE eMenuState;
    public Vector3 vMenuErasePos;

    [Header("Pillar Default Position")]
    public Vector3 vDefPos;
    public Vector3 vEndPos;

    [Header("Pillar Space Randomize")]
    public float fSpaceMin;
    public float fSpaceMax;

    [Header("Pillar Position Randomize")]
    public float fPositionMin;
    public float fPositionMax;

    [Header("Pillar Instantiate Interval")]
    public float fIntervalMin;
    public float fIntervalMax;

    [Header("Pillar Speed")]
    public float fPillarSpeed;

    [Header("Character")]
    public float fCharRotationDownSpeed;
    public float fCharRotationUpSpeed;
    public float fCharJumpPower;
    public Vector3 vDefCharPos;

    [Header("Game")]
    public UserName Nickname;
    public int nScore;
    public Text ScoreText;
}
