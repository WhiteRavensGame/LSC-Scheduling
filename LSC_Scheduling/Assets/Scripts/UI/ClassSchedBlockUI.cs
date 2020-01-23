using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassSchedBlockUI : MonoBehaviour
{
    public Image fillImage;
    public Text instructorText;
    public Text classText;

    public Schedule schedule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInformation(FacultyCourseAssignment fca, Color col)
    {
        if (fca == null)
        {
            instructorText.text = "";
            classText.text = "";
            fillImage.color = Color.clear;
        }
        else
        {
            instructorText.text = fca.faculty.lastName + ", " + fca.faculty.firstName[0];
            classText.text = fca.coursePopulation.course.code + "\n " + fca.coursePopulation.course.courseName;
            fillImage.color = col;
        }

        
    }


}
