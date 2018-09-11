using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    class OverviewCalcs
    {
        //find biggest PGa in a column segment
        internal static double BiggestPGa(AllTable[] bands, int rowFrom, int rowTo)
        {
            double big = double.MinValue;
            for (int row = rowFrom; row < rowTo; row++)
            {
                if (bands[row].PGa > big) big = bands[row].PGa;
            }
            return big;
        }

        internal static double BiggestSVFac(AllTable[] bands, int rowFrom, int rowTo)
        {
            double big = double.MinValue;
            for (int row = rowFrom; row < rowTo; row++)
            {
                if (bands[row].SVFac > big) big = bands[row].SVFac;
            }
            return big;
        }

        internal static double BiggestSVFbd(AllTable[] bands, int rowFrom, int rowTo)
        {
            double big = double.MinValue;
            for (int row = rowFrom; row < rowTo; row++)
            {
                if (bands[row].SVFbd > big) big = bands[row].SVFbd;
            }
            return big;
        }

        internal static double BiggestPGFinBlock(AllTable[] bands, int rowFrom, int rowTo)
        {
            double big = double.MinValue;
            //col15
            for (int row = rowFrom; row < rowTo; row++)
            {
                if (bands[row].PGFrowx15 > big) big = bands[row].PGFrowx15;
            }
            //col16
            for (int row = rowFrom; row < rowTo; row++)
            {
                if (bands[row].PGFrowx16 > big) big = bands[row].PGFrowx16;
            }
            //col17
            for (int row = rowFrom; row < rowTo; row++)
            {
                if (bands[row].PGFrowx17 > big) big = bands[row].PGFrowx17;
            }
            return big;
        }

        internal static double SumOfPointsGradA(AllTable[] atSegment, int skip, int take)
        {
            return atSegment.Skip(skip).Take(take).Sum(at => at.PtsGradA);
        }
        internal static double SumOfPointsGradB(AllTable[] atSegment, int skip, int take)
        {
            return atSegment.Skip(skip).Take(take).Sum(at => at.PtsGradB);
        }
        internal static double SumOfPointsGradC(AllTable[] atSegment, int skip, int take)
        {
            return atSegment.Skip(skip).Take(take).Sum(at => at.PtsGradC);
        }

        internal static double SumOfPointsVolA(AllTable[] atSegment, int skip, int take)
        {
            return atSegment.Skip(skip).Take(take).Sum(at => at.PtsVola);
        }
        internal static double SumOfPointsVolB(AllTable[] atSegment, int skip, int take)
        {
            return atSegment.Skip(skip).Take(take).Sum(at => at.PtsVolb);
        }
        internal static double SumOfPointsVolC(AllTable[] atSegment, int skip, int take)
        {
            return atSegment.Skip(skip).Take(take).Sum(at => at.PtsVolc);
        }
        internal static double SumOfPointsVolD(AllTable[] atSegment, int skip, int take)
        {
            return atSegment.Skip(skip).Take(take).Sum(at => at.PtsVold);
        }

        internal static double SumOfPointsHLc(AllTable[] atSegment, int skip, int take)
        {
            return atSegment.Skip(skip).Take(take).Sum(at => at.PtsHLc);
        }
        internal static double SumOfPointsHLd(AllTable[] atSegment, int skip, int take)
        {
            return atSegment.Skip(skip).Take(take).Sum(at => at.PtsHLd);
        }


        internal static bool isLazyLast10Days(AllTable[] bands, LazyShareParam Z)
        {
            int numBands = bands.Count();

            if (numBands == 10402)
            {
                double totalFV = bands.Skip(9362).Take(1040).Sum(atRec => Math.Sqrt(atRec.FV));
                double avgDailyRootVolume = totalFV / 10; //10 days
                double effectivePrice = bands[10401].FP;
                double VP = avgDailyRootVolume * effectivePrice;
                return VP < Z.Setting;
            }
            else
            {
                throw new ArgumentException($"isLazyLast10Days needs 10402 FP bands, got {numBands}");
            }
        }

        internal static uint SumOfVolumes(AllTable[] atSegment, int skip, int take)
        {
            return (uint)atSegment.Skip(skip).Take(take).Sum(at => at.FV);
        }


        //Here we instantiate an initial Overview object for a share and determine its Laziness
        internal static Overview CreateInitialOverviewForShare(Share share, AllTable atRow1)
        {
            //var lazyShareParams = Helper.UserSettings().ParamsLazyShare;
            // col 2. Instantiate an Overview object and assign Name of share
            Overview oview = new Overview(share.Name, share.Number);
            // Lazy flag - we now assume the Laziness has been precalculated and can be found in passed in Row1
            oview.Lazy = atRow1.Col_2 > 0;
            return oview;
        }

        //perform calculations based on passed in array of All-Table objects
        internal static void PerformOverviewCalcs(Share share, ref Overview oview, AllTable[] atSegment)
        {
            //col 3: Sum of volumes (LastDayVolume) (49) 
            oview.LastDayVol = SumOfVolumes(atSegment, 10298, 104);  
            //col 4: Price of row 10401 (11) 
            oview.LastPrice = atSegment[10401].FP;
            //col 5: Price of row 1040-104-1
            oview.DayBeforePrice = atSegment[10401 - 104].FP;
            //col 6: Price of row 10401 / Price of row 10297
            if (oview.DayBeforePrice > 0) { oview.PriceFactor = oview.LastPrice / oview.DayBeforePrice; }
            //Helper.Log("DEBUG", $"{share.Name} oview.LastPrice / oview.DayBeforePrice = {oview.LastPrice} / {oview.DayBeforePrice} = {oview.PriceFactor}");
            // col 7: Price-Gradient PGc of row 1040 (23)
            oview.LastPGc = atSegment[10401].PGc;
            // col 8: 26 Price - Gradient PGd of row 10401
            oview.LastPGd = atSegment[10401].PGd;
            // col 9: 13 The biggest PGa of row 10298 to 10401
            oview.BigLastDayPGa = BiggestPGa(atSegment, 10298, 10401);
            // col 10: 18 The biggest PGF of All-table column 15 to 17, row 10298 to 10401
            oview.BigLastDayPGF = BiggestPGFinBlock(atSegment, 10298, 10401);
            // col 11: 30 The DHLFPc of row 10401
            oview.LastDHLFPc = atSegment[10401].DHLFPc;
            //col 12: 34 The DHLFPd of row 10401
            oview.LastDHLFPd = atSegment[10401].DHLFPd;
            //col 13: empty
            
            //col 14: 40 The DLLFPc of row 10401
            oview.LastDLLFPc = atSegment[10401].DLLFPc;
            //col 15: 44 The DLLFPd of row 10401
            oview.LastDLLFPd = atSegment[10401].DLLFPd;
            
            //col 16: 64 Sum of Points Gradient a of row 10298 to 10401
            oview.LastDaySumOfPGa = SumOfPointsGradA(atSegment, 10298, 104);
            //col 17: 66 Sum of Points Gradient b   of row 10298 to 10401
            oview.LastDaySumOfPGb = SumOfPointsGradB(atSegment, 10298, 104);
            //col 18: 68 Sum of Points Gradient c of row 10298 to 10401
            oview.LastDaySumOfPGc = SumOfPointsGradC(atSegment, 10298, 104);

            //col 19: 73 Sum of Points Volume a of row 10298 to 10401
            oview.LastDaySumOfPtsVola = SumOfPointsVolA(atSegment, 10298, 104);
            //col 20: 75 Sum of Points Volume b of row 10298 to 10401
            oview.LastDaySumOfPtsVolb = SumOfPointsVolB(atSegment, 10298, 104);
            //col 21: 77 Sum of Points Volume c of row 10298 to 10401
            oview.LastDaySumOfPtsVolc = SumOfPointsVolC(atSegment, 10298, 104);
            //col 22: 79 Sum of Points Volume d of row 10298 to 10401
            oview.LastDaySumOfPtsVold = SumOfPointsVolD(atSegment, 10298, 104);

            //col 23: 70 Sum of Points Points High Line HLc of row 10298 to 10401
            oview.LastDaySumOfPtsHLc = SumOfPointsHLc(atSegment, 10298, 104);
            //col 24: 71 Sum of Points Points High Line HLd of row 10298 to 10401
            oview.LastDaySumOfPtsHLd = SumOfPointsHLd(atSegment, 10298, 104);
            //col 25: duplicate of col 19???
            //col 26: duplicate of col 20???
            //col 27: ask Gunther
            //col 28: ask Gunther
            oview.BigLastDaySVFac = BiggestSVFac(atSegment, 10298, 10401);
            oview.BigLastDaySVFbd = BiggestSVFbd(atSegment, 10298, 10401);

        }


        internal static void OverviewFromLastRow(Share share, ref Overview oview, AllTable atLast)
        {
            //col 3: Sum of volumes (LastDayVolume) (49) 
            oview.LastDayVol = atLast.FV;
            //col 4: Price of row 10401 (11) 
            oview.LastPrice = atLast.FP;
            //col 5: Price of row 1040-104-1
            oview.DayBeforePrice = atLast.Col_10; // atSegment[10401 - 104].FP;  ============> PROBLEM !!!!
            //col 6: Price of row 10401 / Price of row 10297
            if (oview.DayBeforePrice > 0) {
                oview.PriceFactor = oview.LastPrice / oview.DayBeforePrice;
            }
            // col 7: Price-Gradient PGc of row 1040 (23)
            oview.LastPGc = atLast.PGc;
            // col 8: Price-Gradient PGd of row 10401
            oview.LastPGd = atLast.PGd;
            // col 9: 13 The biggest PGa of row 10298 to 10401
            oview.BigLastDayPGa = atLast.PGa;
            // col 10: 18 The biggest PGF of All-table column 15 to 17, row 10298 to 10401
            oview.BigLastDayPGF = atLast.BigPGF;
            // col 11: 30 The DHLFPc of row 10401
            oview.LastDHLFPc = atLast.DHLFPc;
            //col 12: 34 The DHLFPd of row 10401
            oview.LastDHLFPd = atLast.DHLFPd;
            //col 13 unused
            //col 14: 40 The DLLFPc of row 10401
            oview.LastDLLFPc = atLast.DLLFPc;
            //col 15: 44 The DLLFPd of row 10401
            oview.LastDLLFPd = atLast.DLLFPd;
            //col 16: 64 Sum of Points Gradient a of row 10298 to 10401
            oview.LastDaySumOfPGa = atLast.PtsGradA;
            //col 17: 66 Sum of Points Gradient b   of row 10298 to 10401
            oview.LastDaySumOfPGb = atLast.PtsGradB;
            //col 18: 68 Sum of Points Gradient c of row 10298 to 10401
            oview.LastDaySumOfPGc = atLast.PtsGradC;
            //col 19: 73 Sum of Points Volume a of row 10298 to 10401
            oview.LastDaySumOfPtsVola = atLast.PtsVola;
            //col 20: 75 Sum of Points Volume b of row 10298 to 10401
            oview.LastDaySumOfPtsVolb = atLast.PtsVolb;
            //col 21: 77 Sum of Points Volume c of row 10298 to 10401
            oview.LastDaySumOfPtsVolc = atLast.PtsVolc;
            //col 22: 79 Sum of Points Volume d of row 10298 to 10401
            oview.LastDaySumOfPtsVold = atLast.PtsVold;
            //col 23: 70 Sum of Points Points High Line HLc of row 10298 to 10401
            oview.LastDaySumOfPtsHLc = atLast.PtsHLc;
            //col 24: 71 Sum of Points Points High Line HLd of row 10298 to 10401
            oview.LastDaySumOfPtsHLd = atLast.PtsHLd;
            //col 25: duplicate of col 19???
            //col 26: duplicate of col 20???
            //col 27: ask Gunther
            //col 28: ask Gunther

            oview.BigLastDaySVFac = atLast.SVFac;
            oview.BigLastDaySVFbd = atLast.SVFbd;

        }



    }
}
