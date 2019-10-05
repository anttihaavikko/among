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

    private ColorObject stackTop;

    // Start is called before the first frame update
    void Start()
    {
        
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
        {
            ThrowApple();
        }

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadSceneAsync("Main");
    }

    void Colorize(int color)
    {
        pc.groundLayer = Manager.Instance.masks[color];
        pc.canJumpLayers = Manager.Instance.masks[color];

        gameObject.layer = 13 + color;
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
    }

    void ChangeColor()
    {
        Colorize(stackTop.colorIndex);
    }

    void ThrowApple()
    {
        stackTop = stack.TopApple();
        anim.SetTrigger("eat");
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
}
