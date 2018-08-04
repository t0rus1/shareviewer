using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    class OverviewCalcs
    {
        //This version of the LazyShare calculation expects to be passed ONLY a ten day
        //batch of bands
        internal static bool isLazyLast10Days(AllTable[] bands, LazyShareParam Z)
        {
            int numBands = bands.Count();
            if (numBands == 1040)
            {
                double totalFV = bands.Sum(atRec => atRec.FV);
                double numDays = numBands / 104; // should be 10
                double avgDailyVolume = totalFV / numDays;
                double effectivePrice = bands[numBands - 1].FP;
                double VP = avgDailyVolume * effectivePrice;
                return VP < Z.Setting;
            }
            else
            {
                throw new ArgumentException($"isLazyLast10Days needs 1040 FP bands, got {numBands}");
            }
        }

        internal static uint SumOfVolumes(AllTable[] atSegment, int v1, int v2)
        {
            return (uint)atSegment.Sum(at => at.FV);
        }
    }
}
