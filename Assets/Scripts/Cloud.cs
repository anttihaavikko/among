using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	private float speed;

	private float min = -100f;
	private float max = 100f;

    private float dir = 1f;

	// Use this for initialization
	void Awake () {

        dir = Random.value < 0.5f ? 1f : -1f;

		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();

		float r = Random.value;

		float depth = r * 50f + 5;

		float xdir = (Random.value < 0.5f) ? 1f : -1f;
		float ydir = (Random.value < 0.5f) ? 1f : -1f;

		transform.localPosition = new Vector3 (transform.localPosition.x + Random.Range(-20, 20), transform.localPosition.y + Random.Range(-5, 5), 0);
		transform.localScale = new Vector3 (xdir * (1f + r), ydir * (1f + r), 1f);

		sprite.color = new Color (1, 1, 1, 0.1f + Random.value / 2f);

		speed = 0.1f + Random.value * 2f * 0.1f;
	}

	void Update() {
		transform.Translate(new Vector3(dir, 0, 0) * Time.deltaTime * speed);

		if (transform.localPosition.x > max && dir > 0) {
			transform.localPosition = new Vector3 (min, transform.localPosition.y, transform.localPosition.z);
		}

        if (transform.localPosition.x < min && dir < 0)
        {
            transform.localPosition = new Vector3(max, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
