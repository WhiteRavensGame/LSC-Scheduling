using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<Course> gadCourses;
    public List<Course> bgpCourses;
    public List<Room> rooms;

    public static DataManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if( Instance == null)
        {
            Instance = this;
        }
    }

}
