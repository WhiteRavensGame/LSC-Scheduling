using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReadDemo : MonoBehaviour
{
    public TextAsset csv;

    public string[,] results;

    private List<Course> gadCourses;
    public List<CoursePopulation> gadCoursePopulation;
    private List<Course> bgpCourses;
    public List<CoursePopulation> bgpCoursePopulation;

    void Start()
    {
        gadCoursePopulation = new List<CoursePopulation>();
        bgpCoursePopulation = new List<CoursePopulation>();
        gadCourses = DataManager.Instance.gadCourses;
        bgpCourses = DataManager.Instance.bgpCourses;
        //CSVReader.DebugOutputGrid(CSVReader.SplitCsvGrid(csv.text));
        results = CSVReader.SplitCsvGrid(csv.text);

        string courseId = "";
        int studentsCount = -1;

        //Get the course codes for GAD
        //print(results.GetLength(1));
        for(int x = 0; x < results.GetLength(1); x++)
        {
            string courseCode = results[0, x];
            if (courseCode == null)
                break;

            

            if (courseCode.Contains("GAD") || courseCode.Contains("CCM121") || courseCode.Contains("CCM131") || courseCode.Contains("CC310") 
                || courseCode.Contains("CC449") || courseCode.Contains("CC451") || courseCode.Contains("CC453"))
            {
                //print(results[0, x] + "-" + results[3, x] + " : " + results[1, x] + " students. Section " + results[2, x]);

                //Process the running classes for GAD right now.
                courseId = results[0, x];
                studentsCount = int.Parse(results[1, x]);
                foreach (Course course in gadCourses)
                {
                    if (course.code == courseId)
                    {
                        gadCoursePopulation.Add(new CoursePopulation(course, studentsCount));
                    }
                }
            }
            else if(courseCode.Contains("VGP") || courseCode.Contains("CAP499"))
            {
                //print(results[0, x] + "-" + results[3, x] + " : " + results[1, x] + " students. Section " + results[2, x]);

                //Process the running classes for BGP right now.
                courseId = results[0, x];
                studentsCount = int.Parse(results[1, x]);
                foreach (Course course in bgpCourses)
                {
                    if (course.code == courseId)
                    {
                        bgpCoursePopulation.Add(new CoursePopulation(course, studentsCount));
                    }
                }
            }
        }

        // 0 - Course Number
        // 1 - # of students
        // 2 - Section
        // 3 - Course Title
        // 4 - Schedule
        // 5 - Teacher/s
        // 6 - Program
        // 7 - Status (NO NEED)
        // 8 - Semester (NO NEED)
        // 9 - Min Students
        // 10 - Max Students
        // 11 - Max Students (duplicate)
        // 12 - Hours

        foreach(CoursePopulation cp in bgpCoursePopulation)
        {
            //print(cp.c.code + " has " + cp.totalStudents + " students");
        }
    }
}

public class CoursePopulation
{
    public Course c;
    public int totalStudents;

    public CoursePopulation(Course c, int totalStudents)
    {
        this.c = c;
        this.totalStudents = totalStudents;
    }
}