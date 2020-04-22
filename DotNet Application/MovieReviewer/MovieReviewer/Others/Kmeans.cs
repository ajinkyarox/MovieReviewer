using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieReviewer.Others
{
    public class Kmeans
    {
        


        static int partition(List<int> arr, int low,
                                   int high)
        {
            int pivot = arr[high];

            // index of smaller element 
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                // If current element is smaller  
                // than the pivot 
                if (arr[j] < pivot)
                {
                    i++;

                    // swap arr[i] and arr[j] 
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // swap arr[i+1] and arr[high] (or pivot) 
            int temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;

            return i + 1;
        }


        /* The main function that implements QuickSort() 
        arr[] --> Array to be sorted, 
        low --> Starting index, 
        high --> Ending index */
        static void quickSort(List<int> arr, int low, int high)
        {

            
            if (low < high)
            {

                /* pi is partitioning index, arr[pi] is  
                now at right place */
                int pi = partition(arr, low, high);

                // Recursively sort elements before 
                // partition and after partition 
                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }

        public String kmeansscore(List<int> scores)
        {
            if (scores.Count < 10)
            {
                return "Not enough reviews to produce rating.";
            }
            float score = 0;
            quickSort(scores, 0, scores.Count - 1);
            int i1, i2, i3,t1,t2;
            i1 = i2 = i3 = 0;
            List<int> k0 = new List<int>(scores.Count);
            List<int> k1 = new List<int>(scores.Count);
            List<int> k2 = new List<int>(scores.Count);
            for(int i = 0; i < scores.Count; i++)
            {
                k0.Insert(i,(int)scores[i]);
                k1.Insert(i, (int)-100);
                k2.Insert(i, (int)-100);


            }

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
                    t1 = k0[i1] - m1;
                    if (t1 < 0) { t1 = -t1; }

                    t2 = k0[i1] - m2;
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
                if (i2 != 0) {
                    m1 = t2 / i2;
                }
                

                t2 = 0;
                for (t1 = 0; t1 < i3; t1++)
                {
                    t2 = t2 + k2[t1];
                }
                if (i3 != 0)
                {
                    m2 = t2 / i3;
                }
                

                //printing clusters
               
            } while (m1 != om1 && m2 != om2);
            int sc1, sc2, sc3;
            sc1 = sc2 = sc3 = 0;
            for(int i = 0; i < scores.Count; i++)
            {
                if (k0[i] != -100)
                {
                    sc1 = sc1 + 1;
                }
                if (k1[i] != -100)
                {
                    sc2 = sc2 + 1;
                }
                if (k2[i] !=-100)
                {
                    sc3 = sc3 + 1;
                }
            }
            score = (float)sc1/(sc1+sc2+sc3) + (float)sc2/(2*(sc1+sc2+sc3));
            score =score* 100;


            return score.ToString("0.##");
        }
    }
}