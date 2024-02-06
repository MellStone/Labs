using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_SpecColor", Color.red);
        rend.material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
