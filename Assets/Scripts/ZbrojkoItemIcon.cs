using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZbrojkoItemIcon : MonoBehaviour
{
    public void SetItem(SO_ZbrojkoItem item)
    {
        //var iconImage = GetComponent<Image>();

        //if (item == null)
        //    iconImage.enabled = false;
        //else
        //{
        //    iconImage.enabled = true;
        //    iconImage.sprite = item.Icon;
        //}

        if (item is SO_Number)
        {
            TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = item.Value.ToString();
        }
    }

    //potencijalno nepotrebno, ali ostavi za sad
    public Sprite GetItem()
    {
        var iconImage = GetComponent<Image>();

        if (!iconImage.enabled)
            return null;

        return iconImage.sprite;
    }
}
