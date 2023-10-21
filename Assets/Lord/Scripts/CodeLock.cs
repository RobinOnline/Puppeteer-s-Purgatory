using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CodeLock : MonoBehaviour
{
    [SerializeField] private string CodeToUnlock;
    [SerializeField] private TMPro.TMP_Text CodeText;
    [SerializeField] private Ease TextEase;
    private string enterdCode = "";
    public bool Lock = true;

    public void EnterCode(string number)
    {
        if (enterdCode.Length < 4)
        {
            enterdCode += number;
            CodeText.text = enterdCode;
        }
        else
            Debug.Log("Code length is 4!");
    }

    public void CheckCode()
    {
        if (enterdCode == CodeToUnlock)
        {
            Lock = false;
            CodeText.color = Color.green;
            CodeText.rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f).SetEase(TextEase).OnComplete(() =>
            {
                CodeText.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(TextEase).OnComplete(() =>
                {
                    CodeText.color = Color.white;
                    UiManager.Instance.ClosePanel("codelock");
                });
            });
        }
        else
        {
            CodeText.color = Color.red;
            CodeText.rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f).SetEase(TextEase).OnComplete(() =>
            {
                CodeText.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(TextEase).OnComplete(() =>
                {
                    CodeText.color = Color.white;
                });
            });
        }
    }

    public void DeleteCode()
    {
        if (enterdCode.Length > 0)
            enterdCode = enterdCode.Remove(enterdCode.Length - 1);
        CodeText.text = enterdCode;
    }
}
