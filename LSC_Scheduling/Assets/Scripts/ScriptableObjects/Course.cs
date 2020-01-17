using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Course", menuName = "ScriptableObjects/Course", order = 1)]
public class Course : ScriptableObject
{
    public string courseName;
    public string code;
    public string program;
    //public Course[] prereqs;
    public List<Course> coursePrereqs;
    public int quarter;
    public bool isDoubleClass;
    public bool isElective;
}

public enum Schedule
{
    D1_830_1230 = 1,
    D1_1330_1730,
    D1_1830_2230,
    D2_830_1230,
    D2_1330_1730,
    D2_1830_2230,
    D3_830_1230,
    D3_1330_1730,
    D3_1830_2230,
    D4_830_1230,
    D4_1330_1730,
    D4_1830_2230,
    D5_830_1230,
    D5_1330_1730,
    D5_1830_2230,
    D6_830_1230,
    D6_1330_1730,
    D6_1830_2230
};