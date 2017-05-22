using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 팝업 종류 구분
public enum PopupType
{
    Alert,
    Confirm,
    Ranking
}

// 팝업 관리 클래스
public sealed class PopupSystem
{
    // 유저에게 보여줄 팝업창을 저장해 놓는 리스트. 순서대로 출력
    List<PopupData> popupQueue;

    Dictionary<PopupType, PopupController> mapPopup;

    PopupController currentPopup;

    // 싱글톤 패턴으로 하나의 인스턴스를 전역적으로 공유하기 위해 인스턴스 생성
    private static PopupSystem instance = new PopupSystem();

    public static PopupSystem Instance
    {
        get
        {
            return instance;
        }
    }

    private PopupSystem()
    {
        popupQueue = new List<PopupData>();
        mapPopup = new Dictionary<PopupType, PopupController>();
    }

    // regist 함수로 특정 popupType에 매칭되는 popupController를 지정
    public void Regist(PopupType type, PopupController controller)
    {
        mapPopup[type] = controller;
    }

    public void Push(PopupData data)
    {
        popupQueue.Add(data);

        if(currentPopup == null)
        {
            ShowNext();
        }
    }

    public void Pop()
    {
        if(currentPopup != null)
        {
            currentPopup.Close(
                delegate
                {
                    currentPopup = null;

                    if (popupQueue.Count > 0)
                    {
                        ShowNext();
                    }
                }
            );
        }
    }

    private void ShowNext()
    {
        PopupData next = popupQueue[0];

        PopupController controller = mapPopup[next.type].GetComponent<PopupController>();

        currentPopup = controller;
        currentPopup.Build(next);
        currentPopup.Show(delegate { });
        popupQueue.RemoveAt(0);
    }

    public bool IsShowing()
    {
        return currentPopup != null;
    }
}
