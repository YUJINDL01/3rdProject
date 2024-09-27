using System.Collections.Generic;
using UnityEngine;

public class JHDATA
{
    public List<Questions> questions { get; set; }
    public class Questions
    {
        public string question;
        public class Options
        {
            public string A;
            public string B;
            public string C;
            public string D;
        }

        public Options options;
    }
}
