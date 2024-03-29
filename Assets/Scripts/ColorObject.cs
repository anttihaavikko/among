﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorObject : MonoBehaviour
{
    public int colorIndex = 0;
    public SpriteRenderer sprite, shineSprite;
    public bool changesLayer = true;

    // Start is called before the first frame update
    void Start()
    {
        UpdateColor(colorIndex);
    }

    public void UpdateColor(int c)
    {
        if(changesLayer)
            gameObject.layer = 10 + c;

        colorIndex = c;

        if(Manager.Instance)
		{
			if (sprite)
				sprite.color = Manager.Instance.colors[colorIndex];

			if (shineSprite)
				shineSprite.color = Manager.Instance.shineColors[colorIndex];
		}
    }
}
