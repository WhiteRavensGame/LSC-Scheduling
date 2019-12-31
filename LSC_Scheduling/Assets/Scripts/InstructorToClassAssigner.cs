using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstructorToClassAssigner : MonoBehaviour
{
    List<CoursePopulation> gadBgpCourses;

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
        gadBgpCourses = new List<CoursePopulation>();
        facultyAssignedTracker = new List<FacultyCourseAssignment>();
        facultyAssignedTemp = new List<string>();
        gadBgpCourses.AddRange(DataManager.Instance.coursePopulation);
        //gadBgpCourses.AddRange(DataManager.Instance.gadCourses);

        eligibleFaculties = facultyReader.faculties;

        bool facultyFoundForCourse = false;

        //SORT codes
        //List<Order> SortedList = objListOrder.OrderBy(o=>o.OrderDate).ToList();
        //List<Order> SortedList = objListOrder.OrderByDescending(o=>o.OrderDate).ToList();


        foreach (CoursePopulation c in gadBgpCourses)
        {
            facultyFoundForCourse = false;

            foreach(Faculty f in eligibleFaculties)
            {
                //Make sure we don't assign more than 4 (tentative) for a faculty (TODO: Replace this with actual number)
                //int count = facultyAssignedTracker.Where(x => facultyAssignedTracker.Equals(x.faculty)).Count();
                int count = facultyAssignedTemp.Count(s => s == f.firstName);
                //print(count);

                if (count < 4 && f.courseExperiences.Contains(c.course))
                //if (count < f.courseCountLimit && f.courseExperiences.Contains(c))
                {
                    FacultyCourseAssignment facultyCourseCombo = new FacultyCourseAssignment(f, c);
                    facultyAssignedTracker.Add(facultyCourseCombo);
                    facultyAssignedTemp.Add(f.firstName);
                    facultyFoundForCourse = true;
                    if(c.totalStudents <= 3)
                    {
                        print("CLASS COLLAPSE WARNING. " + c.course.code + " only has " + c.totalStudents + " students");
                    }

                    break;
                }
            }

            if(!facultyFoundForCourse)
            {
                print("WARNING. No instructor found to teach " + c.course.code + " - " + c.course.courseName);
            }
        }

        //PrintFacultyClassAssigned();

        //Save the results into the DataManager
        DataManager.Instance.UpdateFacultyCourseAssignment(facultyAssignedTracker);
        
    }

    void DisplayHowManyFacultyCanTeach()
    {
        //eligibleFaculties = eligibleFaculties.OrderByDescending(o => o.courseExperiences.Count).ToList();
        //Print all faculty and class assignments afterwards:
        int teachersCanTeach = 0;
        foreach(CoursePopulation c in gadBgpCourses)
        {
            foreach (Faculty f in eligibleFaculties)
            {
                if(f.courseExperiences.Contains(c.course))
                {
                    teachersCanTeach++;
                }
            }

            print(c.course.code + c.course.courseName + " can be taught by this many teachers: " + teachersCanTeach);
            teachersCanTeach = 0;
        }
        
    }

    void PrintFacultyClassAssigned()
    {
        //Print all faculty and class assignments afterwards:
        foreach (FacultyCourseAssignment fc in facultyAssignedTracker)
        {
            print(fc.coursePopulation.course.code + " " + fc.coursePopulation.course.courseName + " will be taught by " + fc.faculty.firstName + " " + fc.faculty.lastName);
        }
    }

}

public class FacultyCourseAssignment
{
    public Faculty faculty;
    public CoursePopulation coursePopulation;
    public FacultyCourseAssignment(Faculty f, CoursePopulation cp)
    {
        this.faculty = f;
        this.coursePopulation = cp;
    }
}
