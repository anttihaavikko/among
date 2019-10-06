using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleStack : MonoBehaviour
{
    public Transform target;
    public ColorObject[] apples;
    public Transform mouth;
    private int count = 0;
    private bool canDo = true;
    private Vector3 originalPos;
    private int inAirIndex;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }

    public void AddApple(int color)
    {
		AudioManager.Instance.PlayEffectAt(3, transform.position, 0.769f);
		AudioManager.Instance.PlayEffectAt(32, transform.position, 0.883f);
		AudioManager.Instance.PlayEffectAt(0, transform.position, 0.83f);

		apples[count].UpdateColor(color);
        apples[count].gameObject.SetActive(true);
        count++;

        EffectManager.Instance.AddEffectToParent(6, TopApple().transform.position, TopApple().transform);
    }

    public void ThrowTop()
    {
        if (count == 0 || !canDo) return;

        canDo = false;
        anim.SetInteger("eating", count);
        Invoke("ResetApple", 0.2f);
        count--;
    }

    void ResetApple()
    {
        anim.SetInteger("eating", 0);
        canDo = true;
    }

    void HideApple(int index)
    {
        apples[index].gameObject.SetActive(false);
    }

    public ColorObject TopApple()
    {
        return count > 0 ? apples[count - 1] : null;
    }

    public bool IsFull()
    {
        return count == 5;
    }
}
