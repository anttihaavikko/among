using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldEscToQuit : MonoBehaviour
{
    public Vector3 hiddenSize = Vector3.zero;
    public float speed = 0.3f;

    private Vector3 targetSize;
    private float escHeldFor;

    // Start is called before the first frame update
    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = hiddenSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputMagic.Instance.GetButtonDown(InputMagic.B))
        {
            Tweener.Instance.ScaleTo(transform, targetSize, speed, 0f, TweenEasings.BounceEaseOut);
        }

        if (InputMagic.Instance.GetButtonUp(InputMagic.B))
        {
            escHeldFor = 0f;
        }

        if (InputMagic.Instance.GetButton(InputMagic.B))
        {
            escHeldFor += Time.deltaTime;
            CancelInvoke("HideText");
            Invoke("HideText", 2f);
        }

        if(escHeldFor > 1.5f)
        {
            Application.Quit();
        }
    }

    void HideText()
    {
        Tweener.Instance.ScaleTo(transform, hiddenSize, speed, 0f, TweenEasings.QuarticEaseIn);
    }
}
