using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAssigner : MonoBehaviour
{
    List<Classroom> classrooms;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LateStart", 2f);
    }

    void LateStart()
    {
        classrooms = new List<Classroom>();
        foreach(Room room in DataManager.Instance.rooms)
        {
            classrooms.Add(new Classroom(room));
        }

        bool classroomAssignSuccess = false;

        foreach(FacultyCourseAssignment fca in DataManager.Instance.facultyCourseAssignmentList)
        {
            //Assign that faculty into one classroom.
            foreach (Classroom classroom in classrooms)
            {
                foreach (Schedule sched in fca.faculty.freeTimes)
                {
                    if ( classroom.AssignToClassroom(sched, fca) )
                    {
                        //SUCCESS
                        classroomAssignSuccess = true;
                        break;
                    }
                }

                if(classroomAssignSuccess) break;
                
            }

            if(!classroomAssignSuccess)
            {
                print("ROOM WARNING. No room for: " + fca.faculty.lastName + " " + fca.faculty.firstName + " to teach " + fca.coursePopulation.course.code);
            }

            classroomAssignSuccess = false;
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

    public bool AssignToClassroom(Schedule timeslot, FacultyCourseAssignment fca)
    {
        if(sched[(int)timeslot-1] == null)
        {
            sched[(int)timeslot-1] = fca;
            return true;
        }
        else
        {
            return false;
        }
    }

}