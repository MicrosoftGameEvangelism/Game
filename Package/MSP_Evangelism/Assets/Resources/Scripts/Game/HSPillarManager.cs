using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSPillarManager : HSSingleton<HSPillarManager>
{
    private HSPillarPrefabCtrl[] PillarArray;
    private bool bIsGameStarted;
    private float fTimer;
    private float fTick;

    void Start()
    {
        PillarArray = this.GetComponentsInChildren<HSPillarPrefabCtrl>(true);

        //StartGame();
    }

    void Update()
    {
        if(bIsGameStarted)
        {
            fTimer += Time.deltaTime;

            if(fTimer >= fTick)
            {
                fTimer -= fTick;

                fTick = Random.Range(HSGameManager.I.fIntervalMin, HSGameManager.I.fIntervalMax);

                for(int i  = 0; i < PillarArray.Length; i++)
                {
                    if (!PillarArray[i].bIsActive)
                    {
                        PillarArray[i].Activate();
                        break;
                    }
                }

            }
        }
    }

    public void StartGame()
    {
        bIsGameStarted = true;

        fTimer = 0.0f;
        fTick = 2f;
    }

    public void EndGame()
    {
        bIsGameStarted = false;
        fTimer = 0.0f;
    }
}
