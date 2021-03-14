using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Mono : MonoBehaviour
{
    public int GridIndex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Onclick() 
    {
        SendMessageUpwards("ShowInfoPanel",GridIndex,SendMessageOptions.RequireReceiver);
    }
}
