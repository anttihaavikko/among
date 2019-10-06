using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoter : MonoBehaviour
{
    public Face face;
    public Face.Emotion emotion;
    public Face.Emotion leaveEmotion = Face.Emotion.Default;
    public float delay = 0f;
    public bool isEnd = false;
    public Dimmer dimmer;
    public Transform logo;
    private Vector3 logoSize;
    public Eater eater;

    private void Start()
    {
        if(logo)
        {
            logoSize = logo.localScale;
            logo.localScale = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("DoEmotion", delay);

        if(isEnd)
        {
            Invoke("DoEnd", 1f);
            AudioManager.Instance.Highpass(true);
            eater.ended = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Invoke("DoLeaveEmotion", delay);
    }

    void DoEmotion()
    {
        face.Emote(emotion);
    }

    void DoLeaveEmotion()
    {
        face.Emote(leaveEmotion);
    }

    void DoEnd()
    {
        dimmer.FadeIn(2f);
        Tweener.Instance.ScaleTo(logo, logoSize, 0.4f, 1.7f, TweenEasings.BounceEaseOut);

        Invoke("DoSound", 1.7f);
    }

    void DoSound()
    {
        var cp = Camera.main.transform.position;
        AudioManager.Instance.PlayEffectAt(1, cp, 0.502f);
        AudioManager.Instance.PlayEffectAt(3, cp, 1f);
        AudioManager.Instance.PlayEffectAt(5, cp, 1f);
        AudioManager.Instance.PlayEffectAt(12, cp, 1f);
        AudioManager.Instance.PlayEffectAt(14, cp, 1f);
    }
}
