using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    // 팝업창의 Transform
    public Transform popup;

    public bool IsVisible
    {
        get
        {
            return popup.gameObject.activeSelf;
        }

        private set
        {
            popup.gameObject.SetActive(value);
        }
    }

    public virtual void Awake()
    {

    }

	public virtual void Start ()
    {
		
	}

    // 팝업이 화면에 나타날 때 OnEnter() 열거형 함수로 애니메이션 구현
    private IEnumerator OnEnter(Action callback)
    {
        IsVisible = true;

        if(callback != null)
        {
            callback();
        }

        yield break;
    }

    // 팝업이 화면에 사라질 때 OnEnter() 열거형 함수로 애니메이션 구현
    private IEnumerator OnExit(Action callback)
    {
        IsVisible = false;

        if (callback != null)
        {
            callback();
        }

        yield break;
    }

    public virtual void Build(PopupData data)
    {

    }

    public void Show(Action callback)
    {
        StartCoroutine(OnEnter(callback));
    }

    public void Close(Action callback)
    {
        StartCoroutine(OnExit(callback));
    }
}
