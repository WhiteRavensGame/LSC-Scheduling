using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacultyClassEntry : MonoBehaviour
{
    public Text facultyNameText;
    public Text courseCodeText;
    public Text populationText;

    public void UpdateText(string facultyName, string courseCode, int studentCount)
    {
        facultyNameText.text = facultyName;
        courseCodeText.text = courseCode;
        populationText.text = studentCount.ToString();
    }
}
