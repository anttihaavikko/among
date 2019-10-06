using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class EffectCamera : MonoBehaviour {

	private float cutoff = 1f, targetCutoff = 1f;
	private float prevCutoff = 1f;
	private float cutoffPos = 0f;
	private float transitionTime = 0.5f;

	private PostProcessingBehaviour filters;
	private float chromaAmount = 0f;
	private float chromaSpeed = 0.1f;

	private float shakeAmount = 0f, shakeTime = 0f;

	private Vector3 originalPos;

    public Cinemachine.CinemachineBrain brain;

	void Start() {
		filters = GetComponent<PostProcessingBehaviour>();
		originalPos = transform.position;
	}

	void Update() {
		// chromatic aberration update
		if (filters) {
			chromaAmount = Mathf.MoveTowards (chromaAmount, 0, Time.deltaTime * chromaSpeed);
			ChromaticAberrationModel.Settings g = filters.profile.chromaticAberration.settings;
			g.intensity = chromaAmount;
			filters.profile.chromaticAberration.settings = g;
		}

		if (shakeTime > 0f) {
			shakeTime -= Time.deltaTime;
			transform.position = transform.position + new Vector3 (Random.Range (-shakeAmount, shakeAmount), Random.Range (-shakeAmount, shakeAmount), 0);
            brain.enabled = false;
        } else {
			transform.position = originalPos;
            brain.enabled = true;
		}
	}

	public void Chromate(float amount, float speed) {
		chromaAmount = amount;
		chromaSpeed = speed;
	}

	public void Shake(float amount, float time) {
        shakeAmount = amount;
		shakeTime = time;
	}

	public void BaseEffect(float mod = 1f) {
		//Shake (0.04f * mod, 0.075f * mod);
		Chromate (0.25f * mod, 0.1f * mod);

	}
}
