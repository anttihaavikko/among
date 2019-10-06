using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglerTrigger : MonoBehaviour
{
    public GameObject toggledObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Soul")
            toggledObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "Soul")
			toggledObject.SetActive(false);
    }
}
