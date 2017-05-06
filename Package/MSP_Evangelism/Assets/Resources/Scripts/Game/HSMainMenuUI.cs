using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSMainMenuUI : HSSingleton<HSMainMenuUI>
{
    private Transform trans;

    void Start()
    {
        trans = this.transform;
    }

    void Update()
    {
        if (HSGameManager.I.eMenuState.Equals(E_HS_MENU_STATE.E_MENU))
            trans.localPosition = Vector3.Lerp(this.trans.localPosition, Vector3.zero, Time.deltaTime * 2.5f);
        else
            trans.localPosition = Vector3.Lerp(this.trans.localPosition, HSGameManager.I.vMenuErasePos, Time.deltaTime * 2.5f);
    }

    public void PlayClickEvent()
    {
        HSGameManager.I.eMenuState = E_HS_MENU_STATE.E_GAME;
        HSPillarManager.I.StartGame();
        HSGameManager.I.nScore = 0;
        HSGameManager.I.ScoreText.text = HSGameManager.I.nScore.ToString();
        HSCharacterManager.I.Activate();
    }

    public void RankClickEvent()
    {
        HSGameManager.I.eMenuState = E_HS_MENU_STATE.E_RANK;
    }
}
