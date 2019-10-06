using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

	public bool isEnd = false;
	public ColorObject[] endBlocks;

    // Start is called before the first frame update
    void Awake()
    {
        fullSize = bubble.localScale;
        bubble.localScale = Vector3.zero;
        message = bubbleText.textArea.text;
        bubbleText.textArea.text = "";
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
		DoSound();
	}

    void Show()
    {
        if(!oneTime || !shown)
        {
            bubbleText.ShowMessage(message, true);
            Tweener.Instance.ScaleTo(bubble, fullSize, 0.2f, 0f, TweenEasings.QuadraticEaseOut);
			DoSound();
            shown = true;

            if(isEnd)
			{
				Invoke("DoEnd", 1.5f);
			}
        }
    }

    void DoSound()
	{
		AudioManager.Instance.PlayEffectAt(8, transform.position, 0.818f);
		AudioManager.Instance.PlayEffectAt(11, transform.position, 0.445f);
		AudioManager.Instance.PlayEffectAt(5, transform.position, 0.526f);
	}

    void DoEnd()
	{
        endBlocks.ToList().ForEach(eb =>
        {
            eb.UpdateColor(0);
        });
	}
}
