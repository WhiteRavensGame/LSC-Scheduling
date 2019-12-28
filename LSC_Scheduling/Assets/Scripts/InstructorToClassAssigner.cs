using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstructorToClassAssigner : MonoBehaviour
{
    List<Course> gadBgpCourses;

    public ReadFaculty facultyReader;
    List<Faculty> eligibleFaculties;

    List<FacultyCourseAssignment> facultyAssignedTracker;
    List<string> facultyAssignedTemp;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LateStart", 1.0f);
    }

    void LateStart()
    {
        gadBgpCourses = new List<Course>();
        facultyAssignedTracker = new List<FacultyCourseAssignment>();
        facultyAssignedTemp = new List<string>();
        gadBgpCourses.AddRange(DataManager.Instance.bgpCourses);
        gadBgpCourses.AddRange(DataManager.Instance.gadCourses);

        eligibleFaculties = facultyReader.faculties;

        bool facultyFoundForCourse = false;

        foreach (Course c in gadBgpCourses)
        {
            facultyFoundForCourse = false;

            foreach(Faculty f in eligibleFaculties)
            {
                //Make sure we don't assign more than 4 (tentative) for a faculty (TODO: Replace this with actual ID number)
                //int count = facultyAssignedTracker.Where(x => facultyAssignedTracker.Equals(x.faculty)).Count();
                int count = facultyAssignedTemp.Count(s => s == f.firstName);
                //print(count);

                if (count < 4 && f.courseExperiences.Contains(c))
                {
                    FacultyCourseAssignment facultyCourseCombo = new FacultyCourseAssignment(f, c);
                    facultyAssignedTracker.Add(facultyCourseCombo);
                    facultyAssignedTemp.Add(f.firstName);
                    facultyFoundForCourse = true;
                    break;
                }
            }

            if(!facultyFoundForCourse)
            {
                print("WARNING. No instructor found to teach " + c.code + " - " + c.courseName);
            }
        }

        //Print all faculty and class assignments afterwards:
        foreach(FacultyCourseAssignment fc in facultyAssignedTracker)
        {
            print(fc.course.code + " " + fc.course.courseName + " will be taught by " + fc.faculty.firstName + " " + fc.faculty.lastName);
        }
    }


}

public class FacultyCourseAssignment
{
    public Faculty faculty;
    public Course course;
    public FacultyCourseAssignment(Faculty f, Course c)
    {
        this.faculty = f;
        this.course = c;
    }
}
