using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<Course> gadCourses;
    public List<Course> bgpCourses;
    public List<Room> rooms;

    public List<CoursePopulation> coursePopulation;
    public List<FacultyCourseAssignment> facultyCourseAssignmentList;
    List<Classroom> classroomsList;

    public static DataManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if( Instance == null)
        {
            Instance = this;
            coursePopulation = new List<CoursePopulation>();
            facultyCourseAssignmentList = new List<FacultyCourseAssignment>();
            classroomsList = new List<Classroom>();
        }
    }

    
    public void UpdateFacultyCourseAssignment(List<FacultyCourseAssignment> fcAssignmentList)
    {
        facultyCourseAssignmentList.Clear();
        facultyCourseAssignmentList = fcAssignmentList;
    }

    public void UpdateCoursePopulations(List<CoursePopulation> coursePops)
    {
        coursePopulation.Clear();
        coursePopulation = coursePops;
    }

}