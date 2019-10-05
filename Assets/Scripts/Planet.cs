using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private float speed;
    private float angle;

    private void Start()
    {
        angle = Random.value * 360f;
        var s = Random.Range(0.01f, 0.02f);
        speed = Random.value < 0.5f ? s : -s;
    }

    // Update is called once per frame
    void Update()
    {
        angle += speed * Time.deltaTime * 60f;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void ChangeSpeed(float s)
    {
        speed = s;
    }
}
