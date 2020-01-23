using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomAssigner : MonoBehaviour
{
    [Header("UI")]
    public Text roomNumberText;
    public ClassSchedBlockUI[] schedBlocks;
    public InputField qtrField;
    public Toggle gadToggle;
    public Toggle vgpToggle;

    int index = 0;
    
    List<Classroom> classrooms;

    List<Schedule> scheduleSlotAssignedTemp;
    int currentQuarter = 0;
    string currentProgram = "GAD";
    int classBlocksLeft = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LateStart", 2f);
    }

    void LateStart()
    {
        classrooms = new List<Classroom>();
        scheduleSlotAssignedTemp = new List<Schedule>();

        foreach(Room room in DataManager.Instance.rooms)
        {
            classrooms.Add(new Classroom(room));
        }

        bool classroomAssignSuccess = false;

        foreach(FacultyCourseAssignment fca in DataManager.Instance.facultyCourseAssignmentList)
        {
            //Ignore classes that are online only (80000 and above). All physical classes are Section 10000
            print(fca.coursePopulation.section);
            if (fca.coursePopulation.section >= 20000)
            {
                continue;
            }

            //Make sure that students in the same quarter are not facing a schedule conflict (same timeslot).
            if (currentProgram.Contains(fca.coursePopulation.course.code) 
                || currentQuarter != fca.coursePopulation.course.quarter)
            {
                currentQuarter = fca.coursePopulation.course.quarter;
                currentProgram = fca.coursePopulation.course.program;
                scheduleSlotAssignedTemp.Clear();
            }

            if (fca.coursePopulation.course.isDoubleClass)
                classBlocksLeft = 2;
            else
                classBlocksLeft = 1;

            classroomAssignSuccess = false;

            //Assign that faculty into one classroom.
            foreach (Classroom classroom in classrooms)
            {
                foreach (Schedule sched in fca.faculty.freeTimes)
                {
                    //Prevents the same Quarter Class from being assigned on the same slot.
                    if ( classroom.ClassroomIsOccupied(sched) && !scheduleSlotAssignedTemp.Contains(sched) )
                    {
                        //SUCCESS
                        classroom.AssignToClassroom(sched, fca);
                        classroomAssignSuccess = true;
                        classBlocksLeft--;
                        if (classBlocksLeft == 0)
                        {
                            scheduleSlotAssignedTemp.Add(sched);
                            break;
                        }
                            
                    }
                    else if(scheduleSlotAssignedTemp.Contains(sched))
                    {
                        print(sched + " is not available for Quarter " + fca.coursePopulation.course.quarter);
                    }
                }

                if (classroomAssignSuccess)
                    break;                

            }

            if(!classroomAssignSuccess)
            {
                print("ROOM WARNING. No room for: " + fca.faculty.lastName + " " + fca.faculty.firstName + " to teach " + fca.coursePopulation.course.code);
            }
            else if(classBlocksLeft == 1 && fca.coursePopulation.course.isDoubleClass)
            {
                print("WARNING. Second+ class for " + fca.coursePopulation.course.code + " not assigned!");
            }

        }

        //Print out all the classrooms and whoever teaches there:
        PrintAllClassroomSchedules();

    }

    // Update is called once per frame
    void PrintAllClassroomSchedules()
    {
        foreach (Classroom c in classrooms)
        {
            c.PrintClassroomSched();
        }
    }

    public void LoadClassroom(Classroom classroom)
    {
        roomNumberText.text = classroom.room.number.ToString();
        for(int i = 0; i < 18; i++)
        {
            if(classroom.sched[i] == null)
                schedBlocks[i].UpdateInformation(classroom.sched[i], Color.clear);
            else if( classroom.sched[i].coursePopulation.course.program.Contains("GAD") && gadToggle.isOn )
                schedBlocks[i].UpdateInformation(classroom.sched[i], Color.green);
            else if(classroom.sched[i].coursePopulation.course.program.Contains("VGP") && vgpToggle.isOn )
                schedBlocks[i].UpdateInformation(classroom.sched[i], Color.cyan);
            else if (classroom.sched[i].coursePopulation.course.program.Contains("BGP") && vgpToggle.isOn)
                schedBlocks[i].UpdateInformation(classroom.sched[i], Color.blue);
            else
                schedBlocks[i].UpdateInformation(classroom.sched[i], Color.white);
        }
    }

    public void LoadPrevClassroom()
    {
        index = Mathf.Max(0, index - 1);
        LoadClassroom(classrooms[index]);
    }

    public void LoadNextClassroom()
    {
        index = Mathf.Min(index + 1, classrooms.Count-1);
        LoadClassroom(classrooms[index]);
    }

    public void SchedulePressed(Schedule s)
    {

    }
}

public class Classroom
{
    public Room room;
    public FacultyCourseAssignment[] sched;

    public Classroom(Room room)
    {
        this.room = room;
        sched = new FacultyCourseAssignment[18];
    }

    public void PrintClassroomSched()
    {
        Debug.Log("----- ROOM " + room.number + " SCHEDULE -----");
        for(int i = 0; i < 18; i++)
        {
            if (sched[i] == null)
                continue;

            Debug.Log( sched[i].faculty.lastName + " " + sched[i].faculty.firstName[0] + 
                " Course: " + sched[i].coursePopulation.course.code + " at " + (Schedule)(i+1) );
        }
    }

    public bool ClassroomIsOccupied(Schedule timeslot)
    {
        if (sched[(int)timeslot - 1] == null)
            return true;
        else
            return false;
    }

    public void AssignToClassroom(Schedule timeslot, FacultyCourseAssignment fca)
    {
        sched[(int)timeslot - 1] = fca;
    }

    public int FindCourse(Course c)
    {
        for (int i = 0; i < 18; i++)
        {
            if (sched[i] == null)
                continue;
            
            if(sched[i].coursePopulation.course == c)
            {
                return (i+1);
            }
        }

        return -1;
    }
}