using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{

    internal static class Calculations
    {

        internal static void InitializeAllShareCalculationParameters(Properties.Settings aus)
        {
            bool shouldSave = false;
            if (aus.ParamsLazyShare == null)
            {
                //not been set yet. set it to some defaults and save.
                aus.ParamsLazyShare = new LazyShareParam(1000, 1000000, 50000);
                shouldSave = true;
            }
            if (aus.ParamsSlowPrice == null)
            {
                //not been set yet. set it to some defaults and save.
                aus.ParamsSlowPrice = new SlowPriceParam(100, 99999, 0, 0.9999, 100);
                shouldSave = true;
            }
            if (shouldSave)
            {
                aus.Save();
            }
            

        }


        internal static List<String> CalculationNames = new List<String> {
            "*** Choose a Calculation ***",
            "Identify Lazy Shares",
            "Make Slow (Five minutes) Prices SP",
            "Make Five minutes Price Gradients PG",
            "Find direction and Turning",
            "Find Five minutes Gradients Figure PGF",
            "Related volume Figure (RPGFV) of biggest PGF",
            "Make High Line HL",
            "Make Low Line LL",
            "Make Slow Volumes SV",
            "Slow Volume Figure SVFac",
            "Slow Volume Figure SVFbd",
            };

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
        internal static bool LazyShare(AllTable[] bands, LazyShareParam Z, int startRow, int endRow, out string[] auditSummary)
        {
            bool isLazy = false;
            int numBands = endRow - startRow + 1; // normally 1040 bands = 10 days

            if (numBands > 0)
            {
                //we must skip startRow rows because 'Row' starts at -1
                double totalFV = bands.Skip(startRow).Take(numBands).Sum(atRec => atRec.FV);
                double numDays = numBands / 104; // there are 104 bands per day
                double avgDailyVolume = totalFV / numDays;
                double effectivePrice = bands[10401].FP;
                double VP = avgDailyVolume * effectivePrice;
                isLazy = VP < Z.Setting;
                auditSummary =
$@"
CALCULATION AUDIT

Period                       = {numDays} last days
Total Five-Minutes-Volume FV = {totalFV}
Average Daily Volume         = {avgDailyVolume}
Last price (FP1041)          = {effectivePrice}
VP (Avg Daily Vol x Price)   = {VP} 
Z                            = {Z.Setting}
Result:
(VP < Z)?               LAZY = {isLazy}".Split('\n');
            }
            else
            {
                throw new Exception("LazyShare: number of bands must be > 0");
            }
            return isLazy;
        }

        // Compute Slow prices per Gunther's notes
        internal static void MakeSlowPrices(ref AllTable[] bands, SlowPriceParam spp, int startRow, int endRow, out string[] auditSummary)
        {
            auditSummary = new string[] { };

            int numBands = endRow - startRow + 1; // normally 1040 bands = 10 days

            if (numBands > 0)
            {
                //init slow prices to 1 in the starting row
                bands[startRow].SPa = 1; bands[startRow].SPb = 1; bands[startRow].SPc = 1; bands[startRow].SPd = 1;

                for (int f = startRow+1; f <= endRow; f++)
                {
                    //SPa,Ya
                    if (bands[f].FP >= bands[f-1].SPa )
                    {
                        bands[f].SPa = bands[f-1].SPa + Math.Pow(spp.Z*(bands[f].FP-bands[f-1].SPa),spp.Ya)/spp.Z; //Z wont be zero
                    }
                    else
                    {
                        bands[f].SPa = bands[f-1].SPa - Math.Pow(spp.Z*(bands[f-1].SPa-bands[f].FP),spp.Ya)/spp.Z; 
                    }
                    //SPb,Yb
                    if (bands[f].FP >= bands[f-1].SPb)
                    {
                        bands[f].SPb = bands[f-1].SPb + Math.Pow(spp.Z*(bands[f].FP-bands[f-1].SPb), spp.Yb)/spp.Z; //Z wont be zero
                    }
                    else
                    {
                        bands[f].SPb = bands[f-1].SPb - Math.Pow(spp.Z*(bands[f-1].SPb-bands[f].FP), spp.Yb)/spp.Z;
                    }
                    //SPc,Yc
                    if (bands[f].FP >= bands[f-1].SPc)
                    {
                        bands[f].SPc = bands[f-1].SPc + Math.Pow(spp.Z*(bands[f].FP - bands[f-1].SPc), spp.Yc)/spp.Z; //Z wont be zero
                    }
                    else
                    {
                        bands[f].SPc = bands[f-1].SPc - Math.Pow(spp.Z*(bands[f-1].SPc - bands[f].FP), spp.Yc)/spp.Z;
                    }
                    //SPd,Yd
                    if (bands[f].FP >= bands[f-1].SPd)
                    {
                        bands[f].SPd = bands[f-1].SPd + Math.Pow(spp.Z*(bands[f].FP - bands[f-1].SPd), spp.Yd)/spp.Z; //Z wont be zero
                    }
                    else
                    {
                        bands[f].SPd = bands[f-1].SPd - Math.Pow(spp.Z*(bands[f-1].SPd - bands[f].FP), spp.Yd)/spp.Z;
                    }

                }
                auditSummary = $@"{numBands} processed.\nPlease inspect the view for results.".Split('\n');
            }
            else
            {
                auditSummary = $@"No bands to work with".Split('\n');
            }

        }
    }

}
