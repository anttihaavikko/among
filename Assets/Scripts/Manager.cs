using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public Vector3 lightPosition;
    public Color[] colors;
    public Color[] shineColors;
    public Color[] messageColors;
    public LayerMask[] masks;
    public Vector3 checkPoint = Vector3.zero;

    private static Manager instance = null;
	public static Manager Instance {
		get { return instance; }
	}

    void Awake() {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}

        DontDestroyOnLoad(instance.gameObject);
    }
}
