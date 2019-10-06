using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoter : MonoBehaviour
{
    public Face face;
    public Face.Emotion emotion;
    public Face.Emotion leaveEmotion = Face.Emotion.Default;
    public float delay = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("DoEmotion", delay);
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
}
