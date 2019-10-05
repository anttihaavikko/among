using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechPoint : MonoBehaviour
{
    public Transform bubble;
    public bool oneTime = false;
    public float delay = 0f;
    public SpeechBubble bubbleText;
    public int colorIndex;

    private Vector3 fullSize;
    private bool shown = false;

    private string message;

    // Start is called before the first frame update
    void Awake()
    {
        fullSize = bubble.localScale;
        bubble.localScale = Vector3.zero;
        message = bubbleText.textArea.text;
        bubbleText.textArea.text = "";
        Debug.Log(colorIndex);
    }

    private void Start()
    {
        bubbleText.SetColor(Manager.Instance.messageColors[colorIndex]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Soul") return;
        Invoke("Show", delay);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Soul") return;
        CancelInvoke("Show");
        Tweener.Instance.ScaleTo(bubble, Vector3.zero, 0.1f, 0f, TweenEasings.QuadraticEaseIn);
    }

    void Show()
    {
        if(!oneTime || !shown)
        {
            bubbleText.ShowMessage(message, true);
            Tweener.Instance.ScaleTo(bubble, fullSize, 0.2f, 0f, TweenEasings.QuadraticEaseOut);
            shown = true;
        }
    }
}
