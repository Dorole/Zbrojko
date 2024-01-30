using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMessage : MonoBehaviour
{
    //[SerializeField] private MathTeacher _mathTeacher = default;
    [SerializeField] private GameObject _messagePanel = default;
    [SerializeField] private TextMeshProUGUI _messageText = default;
    [SerializeField] private string[] _congratulationsMessages = default; //scriptable obj - customize
    [SerializeField] private float _displayMessageTime = 3f;

    private void Start()
    {
        _messagePanel.SetActive(false);
        MathTeacher.OnCalculationEqual += _mathTeacher_OnCalculationEqual;

    }

    private void _mathTeacher_OnCalculationEqual()
    {
        StartCoroutine(CO_DisplayMessage());
    }

    /// <summary>
    /// Test function. Exchange this for whatever else should happen on equal.
    /// </summary>
    private IEnumerator CO_DisplayMessage()
    {
        int randomMessageIndex = Random.Range(0, _congratulationsMessages.Length);
        _messageText.text = _congratulationsMessages[randomMessageIndex];
        _messagePanel.SetActive(true);

        yield return new WaitForSeconds(_displayMessageTime);

        _messagePanel.SetActive(false);
        _messageText.text = "";
    }

    private void OnDestroy()
    {
        MathTeacher.OnCalculationEqual -= _mathTeacher_OnCalculationEqual;
    }
}
