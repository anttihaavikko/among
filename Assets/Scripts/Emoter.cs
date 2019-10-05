using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoter : MonoBehaviour
{
    public Face face;
    public Face.Emotion emotion;
    public Face.Emotion leaveEmotion = Face.Emotion.Default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        face.Emote(emotion);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        face.Emote(leaveEmotion);
    }
}
