﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {

	public TextMeshPro textArea;
    public SpriteRenderer helpImage;
    public Sprite[] helpSprites;

	private bool shown;
	private string message = "";
	private int messagePos = -1;
    private bool hidesWithAny = false;

    public bool done = false;

	private AudioSource audioSource;
	public AudioClip closeClip;

	public GameObject clickHelp;

	private List<string> messageQue;

	public Color hiliteColor;
    string hiliteColorHex;

    bool useColors = true;
    private bool canSkip = false;

    private string[] options;
    private string[] optionActions;
    private int optionSelection;


    // Use this for initialization
    void Start () {
		audioSource = GetComponent<AudioSource> ();

		messageQue = new List<string> ();

        Invoke("EnableSkip", 0.25f);
    }

    void EnableSkip()
    {
        canSkip = true;
    }

    // Update is called once per frame
    void Update () {

		if (Random.value < 0.1f) {
			return;
		}

		if (messagePos >= 0 && !done) {
			messagePos++;

			string msg = message.Substring (0, messagePos);

			int openCount = msg.Split('(').Length - 1;
			int closeCount = msg.Split(')').Length - 1;

            if (openCount > closeCount && useColors) {
				msg += ")";
			}

            textArea.text = useColors ? msg.Replace("(", "<color=" + hiliteColorHex + ">").Replace(")", "</color>") : msg;

			string letter = message.Substring (messagePos - 1, 1);

			if (messagePos == 1 || letter == " ") {
                //AudioManager.Instance.PlayEffectAt(25, transform.position, 0.5f);
                //AudioManager.Instance.PlayEffectAt(1, transform.position, 0.75f);
            }

			if (messagePos >= message.Length) {
				messagePos = -1;

				done = true;
			}
		}
	}

	public int QueCount() {
		return messageQue.Count;
	}

	public void SkipMessage() {
		done = true;
		messagePos = -1;
		textArea.text = message;
	}

    public void ShowMessage(string str, bool colors = true) {
        hidesWithAny = false;
        if(helpImage) helpImage.transform.localScale = Vector3.zero;
        canSkip = false;
        Invoke("EnableSkip", 0.25f);

        //AudioManager.Instance.PlayEffectAt(9, transform.position, 1f);
        //AudioManager.Instance.PlayEffectAt(27, transform.position, 0.7f);

        useColors = colors;

        //AudioManager.Instance.Highpass ();

		if (closeClip) {
			audioSource.PlayOneShot (closeClip, 1f);
		}

		done = false;
		shown = true;
		message = str;
		textArea.text = "";

		Invoke ("ShowText", 0.2f);
    }

	public void QueMessage(string str) {
		messageQue.Add (str);
	}

	public void CheckQueuedMessages() {
		if (messageQue.Count > 0 && !shown) {
			PopMessage ();
		}
	}

	private void PopMessage() {
		string msg = messageQue [0];
		messageQue.RemoveAt (0);
		ShowMessage (msg);
	}

	private void ShowText() {
		messagePos = 0;
	}

	public void HideAfter (float delay) {
		Invoke ("Hide", delay);
	}

	public void Hide() {


        //AudioManager.Instance.Highpass (false);

        //AudioManager.Instance.PlayEffectAt (9, transform.position, 1f);
        //AudioManager.Instance.PlayEffectAt(27, transform.position, 0.7f);

        if (closeClip) {
			audioSource.PlayOneShot (closeClip, 1f);
		}

		shown = false;
		textArea.text = "";
	}

	public bool IsShown() {
		return shown;
	}

	public void SetColor(Color color) {
        hiliteColorHex = "#" + ColorUtility.ToHtmlStringRGB (color);
	}
}