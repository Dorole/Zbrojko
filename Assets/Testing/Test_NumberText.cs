using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Test_NumberText : MonoBehaviour
{
    private void OnValidate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform grandchild = transform.GetChild(i).GetChild(0);
            TextMeshProUGUI l_text =grandchild.GetChild(0).GetComponent<TextMeshProUGUI>();
            l_text.text = (i+1).ToString();
        }
    }
}
