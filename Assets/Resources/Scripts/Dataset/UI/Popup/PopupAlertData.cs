using System;
using UnityEngine;

public class PopupAlertData : PopupData {

	public string Title
    {
        get;
        private set;
    }

    public string Message
    {
        get;
        private set;
    }

    public Action Callback
    {
        get;
        private set;
    }

    public PopupAlertData(string title, string message, Action callback = null) : base(PopupType.Alert)
    {
        this.Title = title;
        this.Message = message;
        this.Callback = callback;
    }
}
