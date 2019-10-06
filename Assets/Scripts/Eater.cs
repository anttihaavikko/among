using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Eater : MonoBehaviour
{
    public SpriteRenderer[] colorSprites;
    public Anima2D.SpriteMeshInstance[] limbSprites;
    public SpriteRenderer[] shineSprites;
    public AppleStack stack;
    public PlatformerController pc;
    public Animator anim;
    public Face face;
    public GameObject tail;

    private ColorObject stackTop;

    // Start is called before the first frame update
    void Start()
    {
        if(Manager.Instance.checkPoint != Vector3.zero)
            transform.parent.position = Manager.Instance.checkPoint;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //    Colorize(0);

        //if (Input.GetKeyDown(KeyCode.X))
        //    Colorize(1);

        //if (Input.GetKeyDown(KeyCode.C))
        //    Colorize(2);

        if(InputMagic.Instance.GetButtonDown(InputMagic.X))
            ThrowApple();

        if (InputMagic.Instance.GetButtonDown(InputMagic.B))
            SceneManager.LoadSceneAsync("Main");
    }

    void Colorize(int color)
    {
        pc.groundLayer = Manager.Instance.masks[color];
        pc.canJumpLayers = Manager.Instance.masks[color];

        AudioManager.Instance.PlayEffectAt(16, face.transform.position, 0.899f);
        AudioManager.Instance.PlayEffectAt(17, face.transform.position, 0.777f);
        AudioManager.Instance.PlayEffectAt(14, face.transform.position, 0.688f);
        AudioManager.Instance.PlayEffectAt(3, face.transform.position, 1f);


        EffectManager.Instance.AddEffectToParent(0, transform.position - Vector3.up * 0.75f, transform);

        gameObject.layer = 13 + color;
        tail.layer = 13 + color;

        var c = Manager.Instance.colors[color];
        var sc = Manager.Instance.shineColors[color];
        colorSprites.ToList().ForEach(sprite => sprite.color = c);
        shineSprites.ToList().ForEach(sprite => sprite.color = sc);
        limbSprites.ToList().ForEach(sprite => sprite.color = c);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Apple" && collision.gameObject.activeSelf)
        {
            if (stack.IsFull()) return;

            var a = collision.gameObject.GetComponent<ColorObject>();
            if(a)
            {
                stack.AddApple(a.colorIndex);
            }
            collision.gameObject.SetActive(false);
        }
    }

    void Munch()
    {
        face.Emote(Face.Emotion.Shocked, Face.Emotion.Default, 0.15f);
        EffectManager.Instance.AddEffect(2 + stackTop.colorIndex, face.transform.position);
        AudioManager.Instance.PlayEffectAt(Random.Range(25, 31), face.transform.position, 7f);
    }

    void ChangeColor()
    {
        Colorize(stackTop.colorIndex);
    }

    void ThrowApple()
    {
        stackTop = stack.TopApple();
        if(stackTop)
        {
            AudioManager.Instance.PlayEffectAt(17, transform.position, 0.081f);
            AudioManager.Instance.PlayEffectAt(5, transform.position, 0.073f);
            anim.SetTrigger("eat");
        }
    }

    void MoveApple()
    {
        anim.ResetTrigger("eat");
        stack.ThrowTop();
    }

    void ThrowEnded()
    {
        pc.canControl = true;
    }

    void FootStep()
    {
        AudioManager.Instance.PlayEffectAt(12, transform.position, 0.186f);
        AudioManager.Instance.PlayEffectAt(13, transform.position, 0.137f);
    }
}
