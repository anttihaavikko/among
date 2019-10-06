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
    private List<GameObject> gores;

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

        gores = new List<GameObject>();
        DontDestroyOnLoad(instance.gameObject);
    }

    public void AddGore(GameObject g)
    {
        gores.Add(g);

        if(gores.Count > 20)
        {
            var first = gores[0];
            gores.RemoveAt(0);
            Destroy(first);
        }
    }
}
