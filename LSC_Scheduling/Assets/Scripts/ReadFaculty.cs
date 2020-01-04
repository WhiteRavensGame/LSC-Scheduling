using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReadFaculty : MonoBehaviour
{
    public TextAsset csv;
    public string[,] results;

    public List<Faculty> faculties;
    private List<Course> gadBgpCourses;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LateStart", 0.5f);
    }

    void LateStart()
    {
        faculties = new List<Faculty>();
        gadBgpCourses = new List<Course>();
        gadBgpCourses.AddRange(DataManager.Instance.bgpCourses);
        gadBgpCourses.AddRange(DataManager.Instance.gadCourses);

        CSVReader.DebugOutputGrid(CSVReader.SplitCsvGrid(csv.text));
        results = CSVReader.SplitCsvGrid(csv.text);

        List<Course> currentCourseExperiences = new List<Course>();
        List<Course> currentCoursePreference = new List<Course>();
        List<Schedule> timeAvailable = new List<Schedule>();

        for (int x = 1; x < results.GetLength(1); x++)
        {
            currentCourseExperiences.Clear();
            currentCoursePreference.Clear();
            timeAvailable.Clear();

            string testEOF = results[0, x];
            if (testEOF == null)
                break;

            if (true)
            {
                //print(results[0, x] + ", " + results[1, x] + " teaches: " + results[4, x] + " classes. Available at: " + results[5, x]);

                //Parse the faculty id
                int facultyId = 0;
                bool validId = int.TryParse(results[2, x], out facultyId);
                if (!validId) facultyId = 0;

                int courseCountLimit = 5;
                bool validCourseCount = int.TryParse(results[6, x], out courseCountLimit);
                if (!validCourseCount) courseCountLimit = 5;
                Faculty f = new Faculty(results[1, x], results[0, x], facultyId, courseCountLimit);


                //Parse the class experience
                string[] classExperiences = results[4, x].Split(',');
                for (int j = 0; j < classExperiences.Length; j++)
                {
                    foreach (Course course in gadBgpCourses)
                    {
                        if (classExperiences[j].Contains(course.code))
                        {
                            f.AddCourseExperience(course);
                            break;
                        }
                    }
                    //print(classExperiences[j] + "," + j);
                }

                //Parse the schedule availability
                results[5, x] = results[5, x].Trim('"');

                string[] strTimes = results[5, x].Split(',');
                int[] times = strTimes.Select(int.Parse).ToArray();
                for (int j = 0; j < strTimes.Length; j++)
                {
                    f.AddFreeTime((Schedule)times[j]);
                }

                //Add the final version of faculty to the master list.
                faculties.Add(f);

            }
        }

        //PrintFacultyFreeTimes();


        // 0 - First name
        // 1 - Last name
        // 2 - ID
        // 3 - Course Preference
        // 4 - Course Experience
        // 5 - Timeslots Available
        // 6 - Course Count Limit
    }

    

    void PrintFacultyFreeTimes()
    {
        for (int i = 1; i <= 18; i++)
        {
            print("Faculty available on " + (Schedule)i + ": ");
            foreach (Faculty f in faculties)
            {
                if (f.freeTimes.Contains((Schedule)i))
                {
                    print(f.firstName + " " + f.lastName);
                }
            }
        }
    }
    
}

public class Faculty
{
    public string firstName;
    public string lastName;
    public int id;
    public List<Course> coursePreferences;
    public List<Course> courseExperiences;
    public List<Schedule> freeTimes;
    public int courseCountLimit;

    public Faculty(string firstName, string lastName, int id, int courseCountLimit)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.id = id;
        this.courseCountLimit = courseCountLimit;
        this.coursePreferences = new List<Course>();
        this.courseExperiences = new List<Course>();
        this.freeTimes = new List<Schedule>();
    }

    public void AddCoursePreference(Course c)
    {
        this.coursePreferences.Add(c);
    }
    public void AddCourseExperience(Course c)
    {
        this.courseExperiences.Add(c);
    }
    public void AddFreeTime(Schedule s)
    {
        this.freeTimes.Add(s);
    }
}