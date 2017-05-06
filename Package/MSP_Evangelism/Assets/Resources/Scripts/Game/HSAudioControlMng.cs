using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 오디오 소스 매니징 스크립트입니다.
/// 오디오 관련 편의기능 함수들이 있습니다.
/// 싱글턴 클래스 상속되어 있습니다.
/// 오디오 소스배열 0번은 무조건 BGM, 그 외에는 모두 FX용 오디오 소스이며,
/// 소스를 여러개 사용하는 이유는 채널때문입니다.
/// </summary>
public class HSAudioControlMng : HSSingleton<HSAudioControlMng>
{
    /// <summary>
    /// 오디오 소스가 추가되는 오브젝트입니다.
    /// </summary>
    [SerializeField]
    private GameObject HS_ManagerObject;
    /// <summary>
    /// 게임 사운드 리스트입니다.
    /// </summary>
    [SerializeField]
    private List<AudioClip> HS_GameSoundList;
    /// <summary>
    /// 오디오 채널 갯수 입니다.
    /// </summary>
    [SerializeField]
    private int HS_nAudioChannel = 10;

    /// <summary>
    /// 오디오 소스 배열
    /// </summary>
    private AudioSource[] HS_AudioSourceArray;
    /// <summary>
    /// 일시정지 체크용 배열
    /// </summary>
    private bool[] HS_bPauseCheckArray;
    /// <summary>
    /// 어플리케이션 정지시 체크용 배열
    /// </summary>
    private bool[] HS_bApplicationPauseCheckArray;

    void Awake()
    {
        // 매니저 오브젝트 지정 안했을 시 이 스크립트 오브젝트에 추가
        if (HS_ManagerObject == null)
            HS_ManagerObject = this.gameObject;

        // 오디오 소스 생성
        for (int i = 0; i < HS_nAudioChannel; i++)
            HS_ManagerObject.AddComponent<AudioSource>();

        // 관련 배열 이니셜라이징
        HS_AudioSourceArray = HS_ManagerObject.GetComponents<AudioSource>();
        HS_bPauseCheckArray = new bool[HS_AudioSourceArray.Length];
        HS_bApplicationPauseCheckArray = new bool[HS_AudioSourceArray.Length];
    }

    // 원래 자채적으로 되는대 따로 정의하는 이유는 유니티 업데이트에 따라서 오디오쪽에 문제가 생기는 일이 잦음으로..
    // 홈버튼이나 다른 앱 실행으로 인한 어플리케이션 정지 상태시 호출
    // 버전마다 대응하기 귀찮으니 그냥 후처리용으로 정의..
    void OnApplicationPause(bool bPauseStatus)
    {
        if (bPauseStatus)
        {
            for (int i = 0; i < HS_AudioSourceArray.Length; i++)
            {
                if (HS_AudioSourceArray[i].isPlaying)
                {
                    HS_AudioSourceArray[i].Pause();
                    HS_bApplicationPauseCheckArray[i] = true;
                }
            }
        }

        else if (!bPauseStatus)
        {
            for (int i = 0; i < HS_AudioSourceArray.Length; i++)
            {
                if (HS_bApplicationPauseCheckArray[i])
                {
                    HS_AudioSourceArray[i].UnPause();
                    HS_bApplicationPauseCheckArray[i] = false;
                }
            }
        }
    }

    /// <summary>
    /// BGM을 플레이 합니다.
    /// </summary>
    /// <param name="sClipName">클립이름</param>
    /// <param name="bLoopCheck">루프체크</param>
    public void PlayBGM(string sClipName, bool bLoopCheck)
    {
        for (int i = 0; i < HS_GameSoundList.Count; i++)
        {
            if (HS_GameSoundList[i].name.Equals((sClipName)))
            {
                HS_bPauseCheckArray[0] = false;
                HS_AudioSourceArray[0].Stop();
                HS_AudioSourceArray[0].loop = bLoopCheck;
                HS_AudioSourceArray[0].clip = HS_GameSoundList[i];
                HS_AudioSourceArray[0].Play();
                break;
            }
        }
    }

    /// <summary>
    /// 효과음을 플레이 합니다.
    /// </summary>
    /// <param name="sClipName">클립이름</param>
    /// <param name="bLoopCheck">루프체크</param>
    public void PlayFX(string sClipName, bool bLoopCheck)
    {
        for (int i = 0; i < HS_GameSoundList.Count; i++)
        {
            if (HS_GameSoundList[i].name.Equals((sClipName)))
            {
                for (int j = 1; j < HS_AudioSourceArray.Length; j++)
                {
                    if (!HS_AudioSourceArray[j].isPlaying)
                    {
                        HS_bPauseCheckArray[j] = false;
                        HS_AudioSourceArray[j].loop = bLoopCheck;
                        HS_AudioSourceArray[j].clip = HS_GameSoundList[i];
                        HS_AudioSourceArray[j].Play();
                        break;
                    }
                }
                break;
            }
        }
    }

    /// <summary>
    /// BGM 일시정지
    /// </summary>
    public void PauseBGM()
    {
        if (HS_AudioSourceArray[0].isPlaying)
        {
            HS_bPauseCheckArray[0] = true;
            HS_AudioSourceArray[0].Pause();
        }
    }

    /// <summary>
    /// BGM 일시정지 후 계속 재생
    /// </summary>
    public void ResumeBGM()
    {
        if (HS_bPauseCheckArray[0])
        {
            HS_bPauseCheckArray[0] = false;
            HS_AudioSourceArray[0].Play();
        }
    }

    /// <summary>
    /// BGM 멈춤
    /// </summary>
    public void StopBGM()
    {
        if (HS_bPauseCheckArray[0])
            HS_bPauseCheckArray[0] = false;

        HS_AudioSourceArray[0].Stop();
    }

    /// <summary>
    /// 효과음 멈춤 (전체)
    /// </summary>
    public void PauseFX()
    {
        for (int i = 1; i < HS_AudioSourceArray.Length; i++)
        {
            if (HS_AudioSourceArray[i].isPlaying)
            {
                HS_bPauseCheckArray[i] = true;
                HS_AudioSourceArray[i].Pause();
            }
        }
    }

    /// <summary>
    /// 효과음 일시정지 후 계속 재생(전체)
    /// </summary>
    public void ResumeFX()
    {
        for (int i = 1; i < HS_bPauseCheckArray.Length; i++)
        {
            if (HS_bPauseCheckArray[i])
            {
                HS_bPauseCheckArray[i] = false;
                HS_AudioSourceArray[i].Play();
            }
        }
    }

    /// <summary>
    /// 효과음 멈춤(전체)
    /// </summary>
    public void StopFX()
    {
        for (int i = 1; i < HS_bPauseCheckArray.Length; i++)
        {
            if (HS_bPauseCheckArray[i])
                HS_bPauseCheckArray[i] = false;

            HS_AudioSourceArray[i].Stop();
        }
    }

    /// <summary>
    /// BGM볼륨 설정 (0.0f ~ 1.0f)
    /// </summary>
    /// <param name="fValue">볼륨</param>
    public void SetBGMVolume(float fValue)
    {
        HS_AudioSourceArray[0].volume = fValue;
    }

    /// <summary>
    /// 효과음볼륨 설정 (0.0f ~ 1.0f)
    /// </summary>
    /// <param name="fValue">볼륨</param>
    public void SetFXVolume(float fValue)
    {
        for (int i = 1; i < HS_AudioSourceArray.Length; i++)
            HS_AudioSourceArray[i].volume = fValue;
    }


    void OnDestroy()
    {
        HS_AudioSourceArray = null;
        HS_ManagerObject = null;
        HS_GameSoundList = null;
        HS_bPauseCheckArray = null;
    }
}
