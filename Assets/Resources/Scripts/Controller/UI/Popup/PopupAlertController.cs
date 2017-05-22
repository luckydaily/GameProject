using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopupAlertController : PopupController
{
    public Text popupTitle;
    public Text popupMessage;

    PopupAlertData Data
    {
        get;
        set;
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();

        PopupSystem.Instance.Regist(PopupType.Alert, this);
    }

    public override void Build(PopupData data)
    {
        base.Build(data);

        if((data is PopupAlertData) == false)
        {
            Debug.LogError("Invalid Popup Data!");
            return;
        }

        Data = data as PopupAlertData;
        popupTitle.text = Data.Title;
        popupMessage.text = Data.Message;
    }

    public void OnClickOK()
    {
        if(Data != null && Data.Callback != null)
        {
            Data.Callback();
        }

        PopupSystem.Instance.Pop();
    }
}
