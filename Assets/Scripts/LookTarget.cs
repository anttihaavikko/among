using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LookTarget : MonoBehaviour
{
    private List<Transform> pois;
    public Face face;
   
    // Start is called before the first frame update
    void Start()
    {
        pois = new List<Transform>();
        var points = FindObjectsOfType<SpeechPoint>().Select(p => p.transform);
        var apples = GameObject.FindGameObjectsWithTag("Apple").Select(a => a.transform);
        pois.AddRange(points);
        pois.AddRange(apples);

        FindNearest();
    }

    void FindNearest()
    {
        var sorted = pois.FindAll(p => p.gameObject.activeInHierarchy);
        Debug.Log(sorted.Count);
        sorted.Sort((a, b) => {
            var adiff = (a.position - face.transform.position).magnitude;
            var bdiff = (b.position - face.transform.position).magnitude;
            //Debug.Log("Comparing " + adiff + " to " + bdiff);
            return adiff.CompareTo(bdiff);
        });

        var nearest = sorted.FirstOrDefault();
        Debug.Log("First " + sorted.FirstOrDefault().name + " " + DebugDiff(sorted.FirstOrDefault()));
        Debug.Log("Last " + sorted.LastOrDefault().name + " " + DebugDiff(sorted.LastOrDefault()));
        if (nearest)
        {
            transform.position = nearest.position;
            face.lookTarget = nearest;
        }
            

        Invoke("FindNearest", 1);
    }

    float DebugDiff(Transform t)
    {
        return (t.position - face.transform.position).magnitude;
    }
}
