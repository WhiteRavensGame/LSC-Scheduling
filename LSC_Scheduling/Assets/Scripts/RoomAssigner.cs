using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAssigner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LateStart", 1.0f);
    }

    void LateStart()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class ClassBlock
{
    public Room room;
    public Schedule sched;
    public Course c;
    public ClassBlock()
    {
        
    }
}