using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursesListUI : MonoBehaviour
{
    public GameObject courseListEntryPrefab;
    public Transform parentToAttach;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable()
    {
        //Load all the courses available.
        foreach(FacultyCourseAssignment fca in DataManager.Instance.facultyCourseAssignmentList)
        {
            GameObject g = Instantiate(courseListEntryPrefab, parentToAttach);
            g.GetComponent<FacultyClassEntry>().UpdateText(fca.faculty.lastName + " " + fca.faculty.firstName[0] + ".", 
                fca.coursePopulation.course.name, fca.coursePopulation.totalStudents);
        }
        
    }
}
