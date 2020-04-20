using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieReviewer.Others
{
    public class Kmeans
    {
        int score = 0;
        public int kmeansscore(List<int> scores)
        {
            int i1, i2, i3,t1,t2;
            i1 = i2 = i3 = 0;
            List<int> k0 = new List<int>();
            List<int> k1 = new List<int>();
            List<int> k2 = new List<int>();
            int m1=scores.Last();
            int m2=scores.First();

            int om1, om2;
            do
            {

                //saving old means
                om1 = m1;
                om2 = m2;

                //creating clusters
                i1 = i2 = i3 = 0;
                for (i1 = 0; i1 < scores.Count; i1++)
                {
                    //calculating distance to means
                    t1 = k0.ElementAt(i1) - m1;
                    if (t1 < 0) { t1 = -t1; }

                    t2 = k0.ElementAt(i1) - m2;
                    if (t2 < 0) { t2 = -t2; }

                    if (t1 < t2)
                    {
                        //near to first mean
                        k1[i2] = k0[i1];
                        i2++;
                    }
                    else
                    {
                        //near to second mean
                        k2[i3] = k0[i1];
                        i3++;
                    }

                }

                t2 = 0;
                //calculating new mean
                for (t1 = 0; t1 < i2; t1++)
                {
                    t2 = t2 + k1[t1];
                }
                m1 = t2 / i2;

                t2 = 0;
                for (t1 = 0; t1 < i3; t1++)
                {
                    t2 = t2 + k2[t1];
                }
                m2 = t2 / i3;

                //printing clusters
               
            } while (m1 != om1 && m2 != om2);
            score = k2.Count/(k1.Count+k2.Count+k0.Count);
            score *= 100;

            return score;
        }
    }
}