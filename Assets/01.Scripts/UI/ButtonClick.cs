using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private Image _checkImage;

    public void ButtonClickEvent()
    {
        _checkImage.enabled = true;
    }

    public void OffCheckImage()
    {
        _checkImage.enabled = false;
    }
}
