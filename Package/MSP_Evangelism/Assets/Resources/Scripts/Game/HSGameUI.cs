using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HSGameUI : HSSingleton<HSGameUI>
{
    private Transform trans;

    void Start()
    {
        trans = this.transform;
    }

    void Update()
    {
        if (HSGameManager.I.eMenuState.Equals(E_HS_MENU_STATE.E_GAME))
            trans.localPosition = Vector3.Lerp(this.trans.localPosition, Vector3.zero, Time.deltaTime * 2.5f);
        else
            trans.localPosition = Vector3.Lerp(this.trans.localPosition, HSGameManager.I.vMenuErasePos, Time.deltaTime * 2.5f);
    }
}
