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
        public static string[] pWords = { "good","well","better","best","excellent","mind-blowing","fantastic",
        "superb","hit","favorite"};
        public List<string> positiveWords = new List<string>(pWords);
        public static string[] positiveNeutral = {"lived upto the","lives upto the","not that bad","wasn't that bad","wasn't that worse","doesn't fail","is not a failure"};
        public List<string> pnList = new List<string>(positiveNeutral);
        public static string[] negativeNeutral = { "not that good","wasn't that good","wasn't that better","didn't live upto the","doesn't live upto the"};
        public List<string> nnList = new List<string>(negativeNeutral);
    }
}