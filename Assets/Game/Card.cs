using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public Image backImage;
    public GameObject frontRoot;
    public Image frontImage;

    public int id;
    public bool IsRevealed { get; private set; }
    public bool IsMatched { get; private set; }

    private System.Action<Card> onClick;

    public void Init(int pairId, Sprite frontSprite, System.Action<Card> clickHandler)
    {
        id = pairId;
        onClick = clickHandler;
        frontImage.sprite = frontSprite;
        SetRevealed(false, instant: true);
        IsMatched = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsMatched || IsRevealed) return;
        onClick?.Invoke(this);
    }

    public void SetMatched()
    {
        IsMatched = true;
        var btn = GetComponent<Button>();
        if (btn) btn.interactable = false;
    }

    public void SetRevealed(bool show, bool instant = false)
    {
        IsRevealed = show;
        frontRoot.SetActive(show);
        backImage.enabled = !show;
        if (!instant)
        {
            transform.localScale = Vector3.one * 1.03f;
            LeanTween.scale(gameObject, Vector3.one, 0.08f).setEaseOutQuad();
        }
    }
}
