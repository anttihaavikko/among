using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartView : MonoBehaviour
{
    public Dimmer dimmer;
    public Transform logo, startHelp;

    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && !started)
        {
            started = true;
            dimmer.FadeIn(3f);
            Invoke("ChangeScene", 3f);

            Tweener.Instance.ScaleTo(logo, Vector3.zero, 0.5f, 0.3f, TweenEasings.QuadraticEaseIn);
            Tweener.Instance.ScaleTo(startHelp, new Vector3(3f, 0f, 1f), 0.5f, 0f, TweenEasings.QuadraticEaseIn);

            AudioManager.Instance.PlayEffectAt(1, Vector3.zero, 0.502f);
            AudioManager.Instance.PlayEffectAt(3, Vector3.zero, 1f);
            AudioManager.Instance.PlayEffectAt(5, Vector3.zero, 1f);
            AudioManager.Instance.PlayEffectAt(12, Vector3.zero, 1f);
            AudioManager.Instance.PlayEffectAt(14, Vector3.zero, 1f);

            Invoke("DelayedSound", 0.3f);
        }
    }

    void DelayedSound()
    {
        AudioManager.Instance.PlayEffectAt(4, Vector3.zero, 1f);
        AudioManager.Instance.PlayEffectAt(5, Vector3.zero, 1f);
        AudioManager.Instance.PlayEffectAt(17, Vector3.zero, 1f);
    }

    void ChangeScene()
    {
        SceneManager.LoadSceneAsync("Main");
    }
}
