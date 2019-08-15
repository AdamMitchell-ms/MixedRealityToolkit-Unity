using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        //button.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Log()
    {
        Debug.Log("here");
    }
}
