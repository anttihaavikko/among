using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeParallax : MonoBehaviour
{
    private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var v = cam.position * (1f - transform.position.z / 50f);
        transform.position = new Vector3(v.x, v.y, transform.position.z);
    }
}
