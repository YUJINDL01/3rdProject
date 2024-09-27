using System.Collections.Generic;
using UnityEngine;


public class JHdata
{

    public List<Questions> questions { get; set; }

    public class Questions
    {
        public string question;

        public List<string> options { get; set; }
    }


}


