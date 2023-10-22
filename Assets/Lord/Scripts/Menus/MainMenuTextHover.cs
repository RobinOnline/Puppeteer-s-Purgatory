using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MainMenuTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TMPro.TMP_Text text;
    Color whiteFade = new Color(255f, 255f, 255f, 0.2f);
    private void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlayEffect("MenuSelect");
        text.DOKill();
        text.color = Color.white;
        text.rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //text.fontSize = 45;
        //text.color = whiteFade;
        text.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
        text.DOColor(whiteFade, 0.5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlayEffect("MenuClick");
        text.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        text.color = whiteFade;
    }
}
