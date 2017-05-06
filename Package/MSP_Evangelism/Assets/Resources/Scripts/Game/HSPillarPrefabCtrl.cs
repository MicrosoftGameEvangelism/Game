using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HSPillarPrefabCtrl : MonoBehaviour
{
    private GameObject GameObj;
    private Transform MainTrans;
    private Transform[] PillarTrans;
    public bool bIsActive;

    public void Activate()
    {
        GameObj.SetActive(true);
    }

    void Awake()
    {
        GameObj = this.gameObject;
        MainTrans = this.transform;
        PillarTrans = this.GetComponentsInChildren<Transform>();
        GameObj.SetActive(false);
    }

    void Update()
    {
        if(bIsActive)
        {
            MainTrans.localPosition += Vector3.left * HSGameManager.I.fPillarSpeed * Time.deltaTime;

            if (MainTrans.localPosition.x <= HSGameManager.I.vEndPos.x)
                GameObj.SetActive(false);
        }
    }

    void OnEnable()
    {
        MainTrans.localPosition = HSGameManager.I.vDefPos;

        // Position Randomize
        MainTrans.localPosition += Random.Range(HSGameManager.I.fPositionMin, HSGameManager.I.fPositionMax) * Vector3.up;

        // Space Randomize
        float fSpace = Random.Range(HSGameManager.I.fSpaceMin, HSGameManager.I.fSpaceMax);
        PillarTrans[1].localPosition = Vector3.zero + Vector3.up * (fSpace / 2.0f);
        PillarTrans[2].localPosition = Vector3.zero + Vector3.down * (fSpace / 2.0f);

        // Activate Pillar
        bIsActive = true;

    }

    void OnDisable()
    {
        MainTrans.localPosition = HSGameManager.I.vDefPos;
        bIsActive = false;
    }
}
