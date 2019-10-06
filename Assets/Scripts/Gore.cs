using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gore : MonoBehaviour {

    public Rigidbody2D[] parts;
    public SpriteRenderer[] sprites;
    public SpriteRenderer[] shineSprites;
    public int goreColorIndex;

	// Use this for initialization
	void Start () {
        foreach(var p in parts) {
            Manager.Instance.AddGore(p.gameObject);
            var amount = 10f;
            p.AddForce(new Vector2(Random.Range(-amount, amount), Random.Range(-amount, amount)), ForceMode2D.Impulse);
            p.AddTorque(Random.Range(-100f, 100f));
        }
	}
	
	public void ColorizeSprites(int color)
    {
        var c = Manager.Instance.colors[color];
        var sc = Manager.Instance.shineColors[color];
        sprites.ToList().ForEach(sprite => sprite.color = c);
        shineSprites.ToList().ForEach(sprite => sprite.color = sc);
    }
}
