using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using LitJson;

public class HSRankUI : HSSingleton<HSRankUI>
{
    private Text[] RankText;
    private Transform trans;

    void Start()
    {
        trans = this.transform;

        RankText = this.GetComponentsInChildren<Text>();
        for (int i = 1; i < 13; i++)
        {
            RankText[i].text = i.ToString() + ". " + "None" + " 0";
        }

        GetValuesToServer();
    }

    void Update()
    {
        if (HSGameManager.I.eMenuState.Equals(E_HS_MENU_STATE.E_RANK))
            trans.localPosition = Vector3.Lerp(this.trans.localPosition, Vector3.zero, Time.deltaTime * 2.5f);
        else
            trans.localPosition = Vector3.Lerp(this.trans.localPosition, HSGameManager.I.vMenuErasePos, Time.deltaTime * 2.5f);
    }

    public void SendValuesToServer()
    {
        string sUrl = "http://asptestproject.azurewebsites.net/api/values/" + HSGameManager.I.Nickname.makeNickname();

        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sUrl);
        httpWebRequest.ContentType = "text/json";
        httpWebRequest.Method = "PUT";


        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{'score':" + HSGameManager.I.nScore + "}";
            streamWriter.Write(json);
        }
    }

    public void GetValuesToServer()
    {
        string sUrl = "http://asptestproject.azurewebsites.net/api/values/";

        WWW www = new WWW(sUrl);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if(www.error == null)
        {
            JsonData jsonData = JsonMapper.ToObject(www.data);

            for(int i = 0; i < (jsonData.Count <= 12 ? jsonData.Count : 12); i++)
            {
                RankText[i + 1].text = (i + 1).ToString() + " " + jsonData[i]["nickname"] + " " + jsonData[i]["score"];
            }
        }

        StartCoroutine(Repeater());
        StopCoroutine("WaitForRequest");
    }

    IEnumerator Repeater()
    {
        yield return new WaitForSeconds(3.0f);

        GetValuesToServer();
    }

    public void RankExitClickEvent()
    {
        HSGameManager.I.eMenuState = E_HS_MENU_STATE.E_MENU;
    }
}
