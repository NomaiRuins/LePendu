using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TMP_InputField))]
public class InputFieldHandler : MonoBehaviour
{
    [Header("Références")]
    public GameManager gameManager;

    private TMP_InputField inputField;

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

  
    public void ProcessAndClearInput(string newText)
    {
        if (!string.IsNullOrEmpty(newText))
        {
            gameManager.OnLetterPlayed(newText);

            StartCoroutine(ClearFieldCoroutine());
        }
    }

    private IEnumerator ClearFieldCoroutine()
    {
        yield return new WaitForEndOfFrame();

        inputField.text = ""; 
        inputField.ActivateInputField(); 
    }
}