using System;
using System.Collections;
using UnityEngine;

public class PopupAlert : MonoBehaviour
{
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

    public PopupAlert(string title, string message, Action callback)
    {
        this.Title = title;
        this.Message = message;
        this.Callback = callback;
    }
}
