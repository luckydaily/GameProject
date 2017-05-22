using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupData : MonoBehaviour
{
    public PopupType type { get; set; }

    public PopupData(PopupType type)
    {
        this.type = type;
    }
}
