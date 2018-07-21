using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    internal static class Calculations
    {

        /* from Gunther's notes
            3. Delete lazy shares:
            1) In the All-table, sum up the volume in column 49 from row 9362 to row 10401, multiply  it by 0,1 and then multiply it with the Five-minutes-price(FP) of row 10401 column 11.
	            (∑V9362-10401) * 0,1 * FP10401 11 = 
	            (V9362 + V9363 + V9364 + …  ... + V10400 + V10401) * 0,1 * FP10401 11 =: VP
            2) Then compare the result with the Z chosen by user: 
                VP<Z?       Z = 1000 … 1000.000
                In case the inequality is fulfilled, do not use this share for further calculations.
        */

        // Returns a boolean decision as to whether a share is 'Lazy' or not.
        // startRow is meant to normally be 9362 and endRow 10401 - covering a 10 trading day span
        // An average daily price is computed and is then compared to a threshold. If this falls below the threshold
        // the share is considered 'Lazy'
        internal static bool LazyShare(AllTable[] bands, int startRow=9362, int endRow=10401, double Z_threshold=50000)
        {
            bool isLazy = false;
            int numBands = endRow - startRow + 1; // normally 1040 bands = 10 days

            if (numBands > 0)
            {
                //we must skip startRow rows because 'Row' starts at -1
                double totalFV = bands.Skip(startRow).Take(numBands).Sum(atRec => atRec.FV);
                double numDays = numBands / 104; // there are 104 bands per day
                double avgDailyVolume = totalFV / numDays;

                isLazy = avgDailyVolume < Z_threshold;
            }
            else
            {
                throw new Exception("LazyShare: number of bands must be > 0");
            }
            return isLazy;
        }
    }
}
