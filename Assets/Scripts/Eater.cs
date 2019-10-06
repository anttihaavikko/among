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
    public Cinemachine.CinemachineImpulseSource impulseSource;
    public EffectCamera cam;
    public Dimmer dimmer;

    private ColorObject stackTop;

    public GameObject root;
    private int currentColor = -1;

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
            Die();
    }

    void Colorize(int color)
    {
        impulseSource.GenerateImpulseAt(transform.position, Vector3.one * 0.5f);
        cam.BaseEffect(2);

        currentColor = color;

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

            impulseSource.GenerateImpulseAt(transform.position, Vector3.one * 0.25f);
            cam.BaseEffect(1f);

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

        if(stackTop)
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

    public void Die()
    {
        impulseSource.GenerateImpulseAt(transform.position, Vector3.one * 2f);
        cam.BaseEffect(4);
        root.SetActive(false);
        var gore = EffectManager.Instance.AddEffect(7, transform.position);
        EffectManager.Instance.AddEffect(8, transform.position + Vector3.up * 0.5f);
        EffectManager.Instance.AddEffect(8, transform.position + Vector3.down);
        EffectManager.Instance.AddEffect(9, transform.position + Vector3.up * 0.5f);
        EffectManager.Instance.AddEffect(9, transform.position + Vector3.down);

        if(currentColor > -1)
        {
            var g = gore.GetComponent<Gore>();
            g.ColorizeSprites(currentColor);
        }

        AudioManager.Instance.PlayEffectAt(0, transform.position, 1.263f);
        AudioManager.Instance.PlayEffectAt(1, transform.position, 0.802f);
        AudioManager.Instance.PlayEffectAt(2, transform.position, 0.534f);
        AudioManager.Instance.PlayEffectAt(4, transform.position, 0.575f);
        AudioManager.Instance.PlayEffectAt(9, transform.position, 0.462f);

        Invoke("DoFade", 1.5f);

        Invoke("Respawn", 3f);
    }

    void DoFade()
    {
        dimmer.FadeIn(1.5f);
    }

    void Respawn()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude > 5f)
        {
            impulseSource.GenerateImpulseAt(transform.position, Vector3.one * collision.relativeVelocity.magnitude * 0.025f);
            cam.BaseEffect(1f * collision.relativeVelocity.magnitude * 0.025f);
        }
    }
}
