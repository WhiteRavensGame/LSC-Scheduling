using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NextQuarterPredictor : MonoBehaviour
{
    //This quarter's courses
    List<CoursePopulation> courses;

    //Next quarter's courses
    List<CoursePopulation> nextQuarterCourses;


    // Start is called before the first frame update
    void Start()
    {
        //Invoke("PredictNextQuarterData", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PredictNextQuarterData()
    {
        //Check players by quarter.
        courses = DataManager.Instance.coursePopulation;
        //courses.AddRange(DataManager.Instance.bgpCourses);
        
        List<Course> courseGAD = DataManager.Instance.gadCourses.OrderBy(o => o.quarter).ToList();
        int highestQuarter = courses[0].course.quarter;
        List<Course> q1 = (from x in courseGAD where x.quarter == 1 select x).ToList();
        List<Course> q2 = (from x in courseGAD where x.quarter == 2 select x).ToList();
        List<Course> q3 = (from x in courseGAD where x.quarter == 3 select x).ToList();
        List<Course> q4 = (from x in courseGAD where x.quarter == 4 select x).ToList();
        List<Course> q5 = (from x in courseGAD where x.quarter == 5 select x).ToList();

        courses = courses.OrderBy(o => o.course.quarter).ToList();

        int qtr2StudentsGAD = 0;
        int qtr3StudentsGAD = 0;
        int qtr4StudentsGAD = 0;
        int qtr5StudentsGAD = 0;
        int qtr6StudentsGAD = 0;
        int qtr7StudentsGAD = 0;

        int qtr2StudentsBGP = 0;
        int qtr3StudentsBGP = 0;
        int qtr4StudentsBGP = 0;
        int qtr5StudentsBGP = 0;
        int qtr6StudentsBGP = 0;
        int qtr7StudentsBGP = 0;

        nextQuarterCourses = new List<CoursePopulation>();

        //GAD
        //Quarter 1 (Scripting 1)
        //Quarter 2 (Minigames and Prototyping/Scripting 2)
        //Quarter 3 (Level Design 2, and many others)
        //Quarter 4 (Game Research) (MUST ALWAYS RUN)
        //Quarter 5 (Senior Project) (MUST ALWAYS RUN)
        //Quarter 6 (Capstone) (MUST ALWAYS RUN)
        //Classes that run forever (Quarter 1 classes, Capstones, Production Classes)
        //List<CoursePopulation> gadCoursesWithPrereqs = (from x in courses where x.course.coursePrereqs.Count() >= 1 select x).ToList();

        //BGP
        //Quarter 1 (VGP101) Intro to C
        //Quarter 2 (VGP102) C++ 1
        //Quarter 3 (VGP130) C++ 2 
        //Quarter 4 (VGP230) 2D Games Programming (VGP) / (VGP125) C# Programming (BGP)
        //Quarter 5 (VGP125) Programming for Game Engines (VGP) / (VGP230) 2D Games Programming (BGP)
        //Quarter 6 (VGP135) Intro to Mobile Programming
        // VGP242 (3D Graphics Applications) -> VGP330 (Real Time GPU Programming)
        //Other classes past Quarter 7. 
        
        foreach (CoursePopulation cp in courses)
        {
            // =================== GAD =========================
            if(cp.course.code == "GAD120")
            {
                qtr2StudentsGAD = cp.totalStudents;
                print(qtr2StudentsGAD + " to Qtr " + cp.course.quarter + "from " + cp.course.name);
                foreach(CoursePopulation toUpdate in courses)
                {
                    if(toUpdate.course.quarter == 2 && toUpdate.course.program == "GAD" )
                    {
                        nextQuarterCourses.Add(new CoursePopulation(toUpdate.course, qtr2StudentsGAD, toUpdate.section));
                    }
                }
            }
            else if(cp.course.code == "GAD121")
            {
                qtr3StudentsGAD = cp.totalStudents;
                print(qtr3StudentsGAD + " to Qtr " + cp.course.quarter + "from " + cp.course.name);
                foreach (CoursePopulation toUpdate in courses)
                {
                    if (toUpdate.course.quarter == 3 && toUpdate.course.program == "GAD")
                    {
                        nextQuarterCourses.Add(new CoursePopulation(toUpdate.course, qtr3StudentsGAD, toUpdate.section));
                    }
                }
            }
            else if(cp.course.code == "GAD230")
            {
                qtr4StudentsGAD = cp.totalStudents;
                print(qtr4StudentsGAD + " to Qtr " + cp.course.quarter + "from " + cp.course.name);
                foreach (CoursePopulation toUpdate in courses)
                {
                    if (toUpdate.course.quarter == 4 && toUpdate.course.program == "GAD")
                    {
                        nextQuarterCourses.Add(new CoursePopulation(toUpdate.course, qtr4StudentsGAD, toUpdate.section));
                    }
                }
            }

            //Advanced Game Design Exception
            else if (cp.course.code == "GAD262")
            {
                foreach (CoursePopulation toUpdate in courses)
                {
                    if (toUpdate.course.code == "GAD330" && toUpdate.course.program == "GAD")
                    {
                        nextQuarterCourses.Add(new CoursePopulation(toUpdate.course, cp.totalStudents, toUpdate.section));
                    }
                }
            }

            else if(cp.course.code == "GAD222")
            {
                qtr5StudentsGAD = cp.totalStudents;
                print(qtr5StudentsGAD + " to Qtr " + cp.course.quarter + "from " + cp.course.name);
                foreach (CoursePopulation toUpdate in courses)
                {
                    if (toUpdate.course.quarter == 5 && toUpdate.course.program == "GAD")
                    {
                        nextQuarterCourses.Add(new CoursePopulation(toUpdate.course, qtr5StudentsGAD, toUpdate.section));
                    }
                }
            }
            else if (cp.course.code == "GAD320")
            {
                qtr6StudentsGAD = cp.totalStudents;
                print(qtr6StudentsGAD + " to Qtr " + cp.course.quarter + "from " + cp.course.name);
                foreach (CoursePopulation toUpdate in courses)
                {
                    if (toUpdate.course.quarter == 6 && toUpdate.course.program == "GAD")
                    {
                        nextQuarterCourses.Add(new CoursePopulation(toUpdate.course, qtr6StudentsGAD, toUpdate.section));
                    }
                }
            }
            else if (cp.course.code == "GAD322")
            {
                qtr7StudentsGAD = cp.totalStudents;
                print(qtr7StudentsGAD + " to Qtr " + cp.course.quarter + "from " + cp.course.name);
                foreach (CoursePopulation toUpdate in courses)
                {
                    if (toUpdate.course.quarter == 7 && toUpdate.course.program == "GAD")
                    {
                        nextQuarterCourses.Add(new CoursePopulation(toUpdate.course, qtr7StudentsGAD, toUpdate.section));
                    }
                }
            }

            // ========================= BGP ==========================


            else if(cp.course.code == "VGP101")
            {
                AddNextQuarterCourse(cp, cp.totalStudents, cp.section);
            }
            else if(cp.course.code == "VGP102")
            {
                AddNextQuarterCourse(cp, cp.totalStudents, cp.section);
            }
            else if(cp.course.code == "VGP130")
            {
                AddNextQuarterCourse(cp, cp.totalStudents, cp.section);
            }
            else if (cp.course.code == "VGP125")
            {
                AddNextQuarterCourse(cp, cp.totalStudents, cp.section);
            }
            else if (cp.course.code == "VGP230")
            {
                AddNextQuarterCourse(cp, cp.totalStudents, cp.section);
            }
            else if(cp.course.code == "VGP135")
            {
                AddNextQuarterCourse(cp, cp.totalStudents, cp.section);
            }


            // =============== FIRST QUARTER CLASS ==================

            else if (cp.course.quarter == 1)
            {
                //Automatically run them with 10 people tentatively.
                nextQuarterCourses.Add(new CoursePopulation(cp.course, 10, cp.section));
            }
            
            

            //print("[" + cp.course.quarter + "]" + cp.totalStudents + " - " + cp.course.courseName);
            //Algorithm: 
            //1. Get all graduates to 0 in the last quarter. 
            //2. Move grads one quarter before to the new classes.
            //3. Do the same until you reach the end.
            //4. Assume first quarter classes will always run.
        }

        print("PRINTING NEXT QUARTER PREDICTIONS");
        foreach(CoursePopulation nextQuarterCP in nextQuarterCourses)
        {
            print("[" + nextQuarterCP.course.quarter + "]" + nextQuarterCP.course + " will have " + nextQuarterCP.totalStudents + " students");
        }
        
    }

    private void AddNextQuarterCourse(CoursePopulation cp, int studentCount, int section)
    {
        print(cp.totalStudents + " to Qtr " + cp.course.quarter + "from " + cp.course.name);
        foreach (CoursePopulation toUpdate in courses)
        {
            if (toUpdate.course.quarter == (cp.course.quarter + 1) && toUpdate.course.program == cp.course.program)
            {
                nextQuarterCourses.Add(new CoursePopulation(toUpdate.course, cp.totalStudents, toUpdate.section));
            }
        }
    }




}

