using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (var child in GetComponentsInChildren<MonoBehaviour>())
        {
            child.hideFlags = HideFlags.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
