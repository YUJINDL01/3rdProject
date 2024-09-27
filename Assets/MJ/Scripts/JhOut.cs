using System;
using Newtonsoft.Json;
using UnityEngine;

public class JHOut : MonoBehaviour
{
    private string json;
    /*private void Start()
    {
        JHdata data = new JHdata();
        data.questions.question = "문제";
        data.questions.optons.A = "문항 1";
        data.questions.optons.B = "문항 2";
        data.questions.optons.C = "문항 3";
        data.questions.optons.D = "문항 4";
        json = JsonConvert.SerializeObject(data);

        test();
    }
    */

    /*private void test()
    {
        JHdata data = new JHdata();
        data = JsonConvert.DeserializeObject<JHdata>(json);
        foreach (var a in data.questions)
        {
            Debug.Log(a.question);
            Debug.Log(a.options.A);
            Debug.Log(a.options.B);
            Debug.Log(a.options.C);
            Debug.Log(a.options.D);
        }
    }*/
}
