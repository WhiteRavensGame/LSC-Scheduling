﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReadCourses : MonoBehaviour
{
    public TextAsset csv;

    public string[,] results;

    private List<Course> gadCourses;
    public List<CoursePopulation> gadCoursePopulation;
    private List<Course> bgpCourses;
    public List<CoursePopulation> bgpCoursePopulation;

    void Start()
    {
        Invoke("LateStart", 0.5f);
    }

    void LateStart()
    {
        gadCoursePopulation = new List<CoursePopulation>();
        bgpCoursePopulation = new List<CoursePopulation>();
        gadCourses = DataManager.Instance.gadCourses;
        bgpCourses = DataManager.Instance.bgpCourses;
        //CSVReader.DebugOutputGrid(CSVReader.SplitCsvGrid(csv.text));
        results = CSVReader.SplitCsvGrid(csv.text);

        string courseId = "";
        int studentsCount = -1;
        int section = -1;

        //Get the course codes for GAD
        //print(results.GetLength(1));
        for (int x = 0; x < results.GetLength(1); x++)
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
                section = int.Parse(results[2, x]);

                foreach (Course course in gadCourses)
                {
                    if (course.code == courseId)
                    {
                        gadCoursePopulation.Add(new CoursePopulation(course, studentsCount, section));
                    }
                }
            }
            else if (courseCode.Contains("VGP") || courseCode.Contains("CAP499"))
            {
                //print(results[0, x] + "-" + results[3, x] + " : " + results[1, x] + " students. Section " + results[2, x]);

                //Process the running classes for BGP right now.
                courseId = results[0, x];
                studentsCount = int.Parse(results[1, x]);
                section = int.Parse(results[2, x]);

                foreach (Course course in bgpCourses)
                {
                    if (course.code == courseId)
                    {
                        bgpCoursePopulation.Add(new CoursePopulation(course, studentsCount, section));
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

        foreach (CoursePopulation cp in bgpCoursePopulation)
        {
            //print(cp.c.code + " has " + cp.totalStudents + " students");
        }

        //TODO: Adjust the course population predictor to predict the classes running next quarter (based from current quarter numbers)

        List<CoursePopulation> gadBgpPopuations = new List<CoursePopulation>();
        gadBgpPopuations.AddRange(gadCoursePopulation);
        gadBgpPopuations.AddRange(bgpCoursePopulation);

        //Save to DataManager
        DataManager.Instance.UpdateCoursePopulations(gadBgpPopuations);
    }
}

public class CoursePopulation
{
    public Course course;
    public int totalStudents;
    public int section;

    public CoursePopulation(Course c, int totalStudents, int section)
    {
        this.course = c;
        this.totalStudents = totalStudents;
        this.section = section;
    }
}