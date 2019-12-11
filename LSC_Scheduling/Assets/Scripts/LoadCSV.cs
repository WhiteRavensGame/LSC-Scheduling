using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCSV : MonoBehaviour
{
    public TextAsset csvClasses;
    

    // Start is called before the first frame update
    void Start()
    {
        string[] data = csvClasses.text.Split(new char[] { '\n' });
        for(int i = 1; i < data.Length-1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            for(int j = 0; j < row.Length; j++)
            {
                print(row[j]);
            }
            //if(row[5] != "")
            //{
            //    print(row[5]);
            //}
        }

        print(data.Length);
    }
}
