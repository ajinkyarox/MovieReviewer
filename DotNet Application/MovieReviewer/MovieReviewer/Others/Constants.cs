using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieReviewer.Others
{
    public class Constants
    {
        public static string[] nWords = { "bad","worse","worst","worsens","could have been better","fail","failed","failure"
        ,"bomb","bombed"
        };
        public List<string> negativeWords = new List<string>(nWords);
        public static string[] pWords = { "good","better","best","excellent","mind-blowing","mind blowing","fantastic",
        "superb","hit","favorite"};
        public List<string> positiveWords = new List<string>(pWords);
    }
}