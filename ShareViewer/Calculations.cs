using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{

    internal static class Calculations
    {

        internal static void InitializeCalculationParameters(Properties.Settings aus)
        {
            if (aus.ParamsLazyShare == null)
            {
                aus.ParamsLazyShare = new LazyShareParam(50000);
            }
            else
            {
                aus.ParamsLazyShare.ForceValid();
            }

            if (aus.ParamsSlowPrice == null)
            {
                aus.ParamsSlowPrice = new SlowPriceParam(100, 0, 0, 0, 0);
            }
            else
            {
                aus.ParamsSlowPrice.ForceValid();
            }

            if (aus.ParamsDirectionAndTurning == null)
            {
                aus.ParamsDirectionAndTurning = new DirectionAndTurningParam(0.99999);
            }
            else
            {
                aus.ParamsDirectionAndTurning.ForceValid();
            }

            if (aus.ParamsFiveMinsGradientFigure == null)
            {
                aus.ParamsFiveMinsGradientFigure = new FiveMinsGradientFigureParam(104, 1.0, 0);
            }
            else
            {
                aus.ParamsFiveMinsGradientFigure.ForceValid();
            }

            if (aus.ParamsMakeHighLine == null)
            {
                aus.ParamsMakeHighLine = new MakeHighLineParam(0.0005);
            }
            else
            {
                aus.ParamsMakeHighLine.ForceValid();
            }

            if (aus.ParamsMakeLowLine == null)
            {
                aus.ParamsMakeLowLine = new MakeLowLineParam(0.005);
            }
            else
            {
                aus.ParamsMakeLowLine.ForceValid();
            }

            if (aus.ParamsMakeSlowVolume == null)
            {
                aus.ParamsMakeSlowVolume = new MakeSlowVolumeParam(0, 0.9999, 0.1, 0.1, 0.1, 0.1, 0.4);
            }
            else
            {
                aus.ParamsMakeSlowVolume.ForceValid();
            }

            if (aus.ParamsSlowVolFigSVFac == null)
            {
                aus.ParamsSlowVolFigSVFac = new SlowVolFigSVFacParam(1, 0, 104, 1.000);
            }
            else
            {
                aus.ParamsSlowVolFigSVFac.ForceValid();
            }

            if (aus.ParamsSlowVolFigSVFbd == null)
            {
                aus.ParamsSlowVolFigSVFbd = new SlowVolFigSVFbdParam(104, 0, 1.000);
            }
            else
            {
                aus.ParamsSlowVolFigSVFbd.ForceValid();
            }

            //save all force-validated settings for good measure settings regardless
            aus.Save();

        }


        internal static List<String> CalculationNames = new List<String> {
            "*** Choose a Calculation ***",
            "Identify Lazy Shares",                         // 3. Delete lazy shares
            "Make Slow (Five minutes) Prices SP",           // 4. Make Slow (Five minutes) Prices SP:
            "Make Five minutes Price Gradients PG",         // 5. Make Five minutes Price Gradients PG:
            "Find direction and Turning",                   // 6. Find direction and Turning: 
            "Find Five minutes Gradients Figure PGF",       // 7. Find Five minutes Gradients Figure PGF:
            "Related volume Figure (RPGFV) of biggest PGF", // 8. Related volume Figure (RPGFV) of biggest PGF
            "Make High Line HL",                            // 9. Make High Line HL
            "Make Low Line LL",                             // 10. Make Low Line LL
            "Make Slow Volumes SV",                         // 11. Make Slow Volumes SV: 
            "Slow Volume Figure SVFac",                     // 12. Slow Volume Figure SVFac: 
            "Slow Volume Figure SVFbd",                     // 13. Slow Volume Figure SVFbd: 
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
        // NOTE.. this must compute the same result as OverviewCalcs.isLazyLast10Days !!!!
        internal static bool LazyShare(AllTable[] bands, LazyShareParam Z, int startRow, int endRow, out string[] auditSummary)
        {
            bool isLazy = false;
            auditSummary = "".Split('\n');
            int numBands = endRow - startRow + 1; // normally 1040 bands = 10 days

            if (bands.Count() > 0 && numBands > 2)
            {
                //we must skip startRow rows because 'Row' starts at -1
                double totalFV = bands.Skip(startRow).Take(numBands).Sum(atRec => Math.Sqrt(atRec.FV));
                double numDays = numBands / 104; // there are 104 bands per day
                double avgDailyVolume = totalFV / numDays;
                double effectivePrice = bands[endRow].FP;
                double VP = avgDailyVolume * effectivePrice;
                isLazy = VP < Z.Setting;
                auditSummary =
$@"
LazyShare calculation: 

Period                         = {numDays} last days
Sum of roots of Volume FV      = {totalFV}
Avg Daily Sum of RootsVolume   = {avgDailyVolume}
Last price (FP1041)            = {effectivePrice}
VP (Avg DailyRootsVol x Price) = {VP} 
Z                              = {Z.Setting}
Result:
(VP < Z)?                 LAZY = {isLazy}".Split('\n');
            }
            else
            {
                //throw new Exception("LazyShare: number of bands must be > 0");
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

                for (int f = startRow + 1; f <= endRow; f++)
                {
                    //SPa,Ya
                    if (bands[f].FP >= bands[f - 1].SPa)
                    {
                        bands[f].SPa = bands[f - 1].SPa + Math.Pow(spp.Z * (bands[f].FP - bands[f - 1].SPa), spp.Ya) / spp.Z; //Z wont be zero
                    }
                    else
                    {
                        bands[f].SPa = bands[f - 1].SPa - Math.Pow(spp.Z * (bands[f - 1].SPa - bands[f].FP), spp.Ya) / spp.Z;
                    }
                    //SPb,Yb
                    if (bands[f].FP >= bands[f - 1].SPb)
                    {
                        bands[f].SPb = bands[f - 1].SPb + Math.Pow(spp.Z * (bands[f].FP - bands[f - 1].SPb), spp.Yb) / spp.Z; //Z wont be zero
                    }
                    else
                    {
                        bands[f].SPb = bands[f - 1].SPb - Math.Pow(spp.Z * (bands[f - 1].SPb - bands[f].FP), spp.Yb) / spp.Z;
                    }
                    //SPc,Yc
                    if (bands[f].FP >= bands[f - 1].SPc)
                    {
                        bands[f].SPc = bands[f - 1].SPc + Math.Pow(spp.Z * (bands[f].FP - bands[f - 1].SPc), spp.Yc) / spp.Z; //Z wont be zero
                    }
                    else
                    {
                        bands[f].SPc = bands[f - 1].SPc - Math.Pow(spp.Z * (bands[f - 1].SPc - bands[f].FP), spp.Yc) / spp.Z;
                    }
                    //SPd,Yd
                    if (bands[f].FP >= bands[f - 1].SPd)
                    {
                        bands[f].SPd = bands[f - 1].SPd + Math.Pow(spp.Z * (bands[f].FP - bands[f - 1].SPd), spp.Yd) / spp.Z; //Z wont be zero
                    }
                    else
                    {
                        bands[f].SPd = bands[f - 1].SPd - Math.Pow(spp.Z * (bands[f - 1].SPd - bands[f].FP), spp.Yd) / spp.Z;
                    }

                }
                auditSummary = $"MakeSlowPrices results:\n{numBands} processed\nInspect the view.".Split('\n');
            }
            else
            {
                auditSummary = $"MakeSlowPrices: No bands to work with".Split('\n');
            }

        }


        // Compute price gradients per Gunther's notes
        internal static void MakeFiveMinutesPriceGradients(ref AllTable[] bands, int startRow, int endRow, out string[] auditSummary)
        {
            auditSummary = new string[] { };
            int numBands = endRow - startRow + 1;
            if (numBands > 0)
            {
                for (int f = startRow+1; f <= endRow; f++)
                {
                    if (bands[f - 1].SPa != 0)
                    {
                        bands[f].PGa = bands[f].SPa / bands[f - 1].SPa;
                    }
                    if (bands[f - 1].SPb != 0)
                    {
                        bands[f].PGb = bands[f].SPb / bands[f - 1].SPb;
                    }
                    if (bands[f - 1].SPc != 0)
                    {
                        bands[f].PGc = bands[f].SPc / bands[f - 1].SPc;
                    }
                    if (bands[f - 1].SPd != 0)
                    {
                        bands[f].PGd = bands[f].SPd / bands[f - 1].SPd;
                    }
                }
                auditSummary = $"MakeFiveMinutesPriceGradients results:\n{numBands} processed.\nInspect the view.".Split('\n');
            }
            else
            {
                auditSummary = $"MakeFiveMinutesPriceGradients: No bands to work with".Split('\n');
            }

        }

        // Compute DirectionAndTurning per Gunther's notes
        // startRow is normally 10298, endRow 10401
        internal static void FindDirectionAndTurning(ref AllTable[] bands, DirectionAndTurningParam dtp, int startRow, int endRow, out string[] auditSummary)
        {
            auditSummary = new string[] { };
            int numBands = endRow - startRow + 1;
            if (numBands > 0)
            {
                if (bands[endRow].PGc > dtp.Z)
                {
                    bool jackPot = false;
                    bands[endRow].PtsGradC += 0.01; // 0.1; // Direction
                    //if any PGc from row 10298 till row 10400 is smaller than PGcThreshold
                    for (int i = startRow; i < endRow; i++)
                    {
                        if (bands[i].PGc < dtp.PGcThreshold)
                        {
                            jackPot = true;
                            bands[i].PtsGradC += 3;
                            auditSummary = $"FindDirectionAndTurning:\nThe PGc value in row {endRow} ({bands[endRow].PGc}) DOES exceed Z ({dtp.Z})\nplus PGc value in row {i} ({bands[i].PGc}) IS < threshold ({dtp.PGcThreshold})\nInspect the view.".Split('\n');
                            break;
                        }
                    }
                    if (!jackPot)
                    {
                        auditSummary = $"FindDirectionAndTurning:\nThe PGc value in row {endRow} ({bands[endRow].PGc}) does exceed Z ({dtp.Z})\nBUT no PGc value in rows {startRow} to {endRow}  IS < threshold ({dtp.PGcThreshold})".Split('\n');
                    }
                }
                else
                {
                    auditSummary = $"FindDirectionAndTurning:\nLast PGc value ({bands[endRow].PGc}) does NOT exceed Z ({dtp.Z})\nNo changes made".Split('\n');
                }
            }
        }

        //looks through PGFrowx15,PGFrowx16,PGFrowx17 values in atRows array from curRow back daysback rows
        //and returns biggest of these values
        private static double BiggestPrecedingPGF(ref AllTable[] atRows, int curRow, int daysBack)
        {
            double biggest = -1;
            int downTo = curRow - daysBack;
            if (downTo < 2) downTo = 2;
            for (int i = curRow-1; i >= downTo; i--)
            {
                if (atRows[i].PGFrowx15 > biggest)
                {
                    biggest = atRows[i].PGFrowx15;
                }
                if (atRows[i].PGFrowx16 > biggest)
                {
                    biggest = atRows[i].PGFrowx16;
                }
                if (atRows[i].PGFrowx17 > biggest)
                {
                    biggest = atRows[i].PGFrowx17;
                }
            }
            return biggest;
        }

        //Compute per Gunther's notes 
        internal static void FindFiveMinsGradientsFigurePGF(ref AllTable[] atRows, FiveMinsGradientFigureParam calcFiveMinsGradientFigureParam, int startRow, int endRow, out string[] auditSummary)
        {
            string auditSegment;
            auditSummary = new string[] { };

            //prefill APpg column 13 with 1's
            for (int i = startRow; i <= endRow; i++)  // 10298 -> 10401
            {
                atRows[i].APpg = 1;
            }
            //col 15
            for (int i = startRow; i <= endRow; i++)  // 10298 -> 10401
            {
                atRows[i].PGFrowx15 = atRows[i].PGa / atRows[i].APpg;
            }
            //col16
            for (int i = startRow; i <= endRow - 1; i++)  // 10298 -> 10400
            {
                atRows[i].PGFrowx16 = (atRows[i].PGa * atRows[i + 1].PGa) / atRows[i].APpg;
            }
            //col17
            for (int i = startRow; i <= endRow - 2; i++)  // 10298 -> 10399
            {
                atRows[i].PGFrowx17 = (atRows[i].PGa * atRows[i + 1].PGa * atRows[i + 2].PGa) / atRows[i].APpg;
            }
            auditSegment = $"FindFiveMinsGradientsFigurePGF:\nColumns APpg, PGFrowx15, PGFrowx16, PGFrowx17 filled from row {startRow}-{endRow}.\nPlease inspect the view.";

            // from Gunther
            //2) look in the fields of columns 15 to17 and the PRECEDING Z rows for the biggest figure. Z = 104 … 999  	
            //   Z is a variable the user can choose.

            for (int i = startRow; i <= endRow; i++)
            {
                //find biggest figure in the preceding Z rows amongst PGFrowx15, PGFrowx16, PGFrowx17
                double biggest;
                biggest = BiggestPrecedingPGF(ref atRows, i, calcFiveMinsGradientFigureParam.Z);
                if (biggest > 1.0)
                {
                    //atRows[i].APpg *= (1 + calcFiveMinsGradientFigureParam.Y);
                    //continue increasing APpg value onwards to the last row
                    for (int j = i; j <= endRow-2; j++)
                    {
                        atRows[j].APpg *= (1 + calcFiveMinsGradientFigureParam.Y);

                        atRows[j].PGFrowx15 = atRows[j].PGa / atRows[j].APpg;
                        atRows[j].PGFrowx16 = (atRows[j].PGa * atRows[j + 1].PGa) / atRows[j].APpg;
                        atRows[j].PGFrowx17 = (atRows[i].PGa * atRows[j + 1].PGa * atRows[j + 2].PGa) / atRows[j].APpg;
                    }
                }
                else
                {
                    //atRows[i].APpg *= Math.Pow(1 - calcFiveMinsGradientFigureParam.Y, calcFiveMinsGradientFigureParam.X);
                    //continue declining APpg value onwards to the last row
                    for (int j = i; j <= endRow-2; j++)
                    {
                        atRows[j].APpg *= Math.Pow(1 - calcFiveMinsGradientFigureParam.Y, calcFiveMinsGradientFigureParam.X);

                        atRows[j].PGFrowx15 = atRows[j].PGa / atRows[j].APpg;
                        atRows[j].PGFrowx16 = (atRows[j].PGa * atRows[j + 1].PGa) / atRows[j].APpg;
                        atRows[j].PGFrowx17 = (atRows[i].PGa * atRows[j + 1].PGa * atRows[j + 2].PGa) / atRows[j].APpg;
                    }
                }

            }

        }

        internal static void RelatedVolumeFigureOfBiggestPGF(ref AllTable[] atRows, int startRow, int endRow, out string[] auditSummary)
        {
            auditSummary = new string[] { };
            auditSummary = "LEON has questions about interpreting Gunther's notes... No calculations performed yet".Split('\n');
        }

        internal static void MakeHighLineHL(ref AllTable[] atRows, MakeHighLineParam calcHighLineParam, int startRow, int endRow, out string[] auditSummary)
        {
            auditSummary = new string[] { };

            //1) prefill HLc and HLd columns 29 & 33 with 1's
            for (int i = startRow; i <= endRow; i++)  // 10298 -> 10401
            {
                atRows[i].HLc = 1;
                atRows[i].HLd = 1;
                atRows[i].PtsHLc = 0;
                atRows[i].PtsHLd = 0;
            }

            //1) make two HLs
            for (int i = startRow + 1; i <= endRow; i++)  // 10298 -> 10401
            {
                atRows[i].HLc = atRows[i - 1].HLc * atRows[i].PGc * (1 - 0.01*calcHighLineParam.Z);
                if (atRows[i].HLc < atRows[i].FP)
                {
                    // 2) and 3)
                    atRows[i].HLc = atRows[i].FP;
                    atRows[i].PtsHLc += 5;
                }
                atRows[i].HLd = atRows[i - 1].HLd * atRows[i].PGd * (1 - 0.01*calcHighLineParam.Z);
                if (atRows[i].HLd < atRows[i].FP)
                {
                    // 2) and 3)
                    atRows[i].HLd = atRows[i].FP;
                    atRows[i].PtsHLd += 5;
                }
                //4) Distance HL to FP
                if (atRows[i].FP > 0)
                {
                    atRows[i].DHLFPc = atRows[i].HLc / atRows[i].FP;
                    atRows[i].DHLFPd = atRows[i].HLd / atRows[i].FP;
                }
            }

            auditSummary = $"MakeHighLineHL:\nHLc, HLd, PtsHLc, PtsHLd, DHLFPc and DHLFPd computed from row {startRow}-{endRow}.\nPlease inspect the view".Split('\n');

        }


        internal static void MakeLowLineLL(ref AllTable[] atRows, MakeLowLineParam calcLowLineParam, int startRow, int endRow, out string[] auditSummary)
        {
            auditSummary = new string[] { };

            //1) prefill LLc and LLd columns 39 & 43 with 1's
            for (int i = startRow; i <= endRow; i++)  // 10298 -> 10401
            {
                atRows[i].LLc = 1;
                atRows[i].LLd = 1;
                atRows[i].PtsLLc = 0;
                atRows[i].PtsLLd = 0;
            }

            //1) make two LLs
            for (int i = startRow + 1; i <= endRow; i++)  // 10298 -> 10401
            {
                atRows[i].LLc = atRows[i - 1].LLc * atRows[i].PGc * (1 + 0.01*calcLowLineParam.Z);
                if (atRows[i].LLc > atRows[i].FP)
                {
                    // 2) and 3)
                    atRows[i].LLc = atRows[i].FP;
                    atRows[i].PtsLLc += 5;
                }
                atRows[i].LLd = atRows[i - 1].LLd * atRows[i].PGd * (1 + 0.01*calcLowLineParam.Z);
                if (atRows[i].LLd > atRows[i].FP)
                {
                    // 2) and 3)
                    atRows[i].LLd = atRows[i].FP;
                    atRows[i].PtsLLd += 5;
                }
                //4) Distance HL to FP
                if (atRows[i].FP > 0)
                {
                    atRows[i].DLLFPc = atRows[i].LLc / atRows[i].FP;
                    atRows[i].DLLFPd = atRows[i].LLd / atRows[i].FP;
                }
            }

            auditSummary = $"MakeLowLineLL:\nLLc, LLd, PtsLLc, PtsLLd, DLLFPc and DLLFPd computed from row {startRow}-{endRow}.\nPlease inspect the view".Split('\n');

        }

        //ensure two UINT operands can produce a negative result
        //internal static int SubtractUints(uint a, uint b)
        //{
        //    return (int)a - (int)b;
        //}

        //add a uint to a double and prevent a too large double from causing overflow by clamping the result
        //internal static uint SafeAdd(uint a, double b)
        //{
        //    uint result;
        //    if (Double.IsNaN(b)) return a;

        //    try
        //    {
        //        result = a + Convert.ToUInt32(b);
        //    }
        //    catch (OverflowException ex)
        //    {
        //        result = UInt32.MaxValue;
        //    }
        //    return result;
        //}

        //subtracts b from a, wanting an unsigned (positive) result
        //if result aint positive, clamp result to zero
        //internal static uint SafeSubtract(uint a, double b)
        //{
        //    if (Double.IsNaN(b)) return a;
        //    double posRes = Convert.ToDouble(a) - b;
        //    if (posRes >=0)
        //    {
        //        return SafeAdd(0, posRes);
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}


        internal static void MakeSlowVolume(ref AllTable[] atRows, MakeSlowVolumeParam svp, int startRow, int endRow, out string[] auditSummary)
        {
            double diffTerm;
            double powTerm;

            auditSummary = new string[] { };

            //1) prefill SVa,SVb,SVc,SVd with 1
            for (int i = startRow; i <= endRow; i++)  // 2 -> 10401
            {
                atRows[i].SVa = 1;
                atRows[i].SVb = 1;
                atRows[i].SVc = 1;
                atRows[i].SVd = 1;
            }
            //SVa
            for (int i = startRow + 1; i <= endRow; i++)  // 2 -> 10401
            {
                if (atRows[i - 1].SVa < atRows[i].FV)
                {
                    if (i < 10401)
                    {
                        atRows[i].SVa = atRows[i - 1].SVa + Math.Pow(atRows[i].FV - atRows[i - 1].SVa, svp.Ya);
                    }
                    else if (i == 10401)
                    {
                        //special treatment for the 17:35 band volume (FV) raise it to a settable power before using it
                        atRows[i].SVa = atRows[i - 1].SVa + Math.Pow(Math.Pow(atRows[i].FV,svp.X) - atRows[i - 1].SVa, svp.Ya);
                    }
                }
                else if (atRows[i - 1].SVa > atRows[i].FV)
                {
                    if (i < 10401)
                    {
                        atRows[i].SVa = atRows[i - 1].SVa - Math.Pow(atRows[i - 1].SVa - atRows[i].FV, svp.Ya);
                    }
                    else if (i == 10401)
                    {
                        //special treatment for the 17:35 band volume (FV) raise it to a settable power before using it
                        atRows[i].SVa = atRows[i - 1].SVa - Math.Pow(atRows[i - 1].SVa - Math.Pow(atRows[i].FV,svp.X), svp.Ya);
                    }
                }
            }
            //SVb
            for (int i = startRow + 1; i <= endRow; i++)  // 2 -> 10401
            {
                if (atRows[i - 1].SVb < atRows[i].FV)
                {
                    if (i < 10401)
                    {
                        atRows[i].SVb = atRows[i - 1].SVb + Math.Pow(atRows[i].FV - atRows[i - 1].SVb, svp.Yb);
                    }
                    else if (i == 10401)
                    {
                        //special treatment for the 17:35 band volume (FV) raise it to a settable power before using it
                        atRows[i].SVb = atRows[i - 1].SVb + Math.Pow(Math.Pow(atRows[i].FV, svp.X) - atRows[i - 1].SVb, svp.Yb);
                    }
                }
                else if (atRows[i - 1].SVb > atRows[i].FV)
                {
                    if (i < 10401)
                    {
                        atRows[i].SVb = atRows[i - 1].SVb - Math.Pow(atRows[i - 1].SVb - atRows[i].FV, svp.Yb);
                    }
                    else if (i == 10401)
                    {
                        //special treatment for the 17:35 band volume (FV) raise it to a settable power before using it
                        atRows[i].SVb = atRows[i - 1].SVb - Math.Pow(atRows[i - 1].SVb - Math.Pow(atRows[i].FV, svp.X), svp.Yb);
                    }
                }
            }
            //SVc
            for (int i = startRow + 1; i <= endRow; i++)  // 2 -> 10401
            {
                if (atRows[i - 1].SVc < atRows[i].FV)
                {
                    if (i < 10401)
                    {
                        atRows[i].SVc = atRows[i - 1].SVc + Math.Pow(atRows[i].FV - atRows[i - 1].SVc, svp.Yc);
                    }
                    else if (i == 10401)
                    {
                        //special treatment for the 17:35 band volume (FV) raise it to a settable power before using it
                        atRows[i].SVc = atRows[i - 1].SVc + Math.Pow(Math.Pow(atRows[i].FV, svp.X) - atRows[i - 1].SVc, svp.Yc);
                    }
                }
                else if (atRows[i - 1].SVc > atRows[i].FV)
                {
                    if (i < 10401)
                    {
                        atRows[i].SVc = atRows[i - 1].SVc - Math.Pow(atRows[i - 1].SVc - atRows[i].FV, svp.Yc);
                    }
                    else if (i == 10401)
                    {
                        //special treatment for the 17:35 band volume (FV) raise it to a settable power before using it
                        atRows[i].SVc = atRows[i - 1].SVc - Math.Pow(atRows[i - 1].SVc - Math.Pow(atRows[i].FV, svp.X), svp.Yc);
                    }
                }
            }
            //SVd
            for (int i = startRow + 1; i <= endRow; i++)  // 2 -> 10401
            {
                if (atRows[i - 1].SVd < atRows[i].FV)
                {
                    if (i < 10401)
                    {
                        atRows[i].SVd = atRows[i - 1].SVd + Math.Pow(atRows[i].FV - atRows[i - 1].SVd, svp.Yd);
                    }
                    else if (i == 10401)
                    {
                        //special treatment for the 17:35 band volume (FV) raise it to a settable power before using it
                        atRows[i].SVd = atRows[i - 1].SVd + Math.Pow(Math.Pow(atRows[i].FV, svp.X) - atRows[i - 1].SVd, svp.Yd);
                    }
                }
                else if (atRows[i - 1].SVd > atRows[i].FV)
                {
                    if (i < 10401)
                    {
                        atRows[i].SVd = atRows[i - 1].SVd - Math.Pow(atRows[i - 1].SVd - atRows[i].FV, svp.Yd);
                    }
                    else if (i == 10401)
                    {
                        //special treatment for the 17:35 band volume (FV) raise it to a settable power before using it
                        atRows[i].SVd = atRows[i - 1].SVd - Math.Pow(atRows[i - 1].SVd - Math.Pow(atRows[i].FV, svp.X), svp.Yd);
                    }
                }
            }

            auditSummary = $"MakeSlowVolume:\nSVa,SVb,SVc,SVd computed from row {startRow}-{endRow}.\nInspect the view".Split('\n');
        }

        private static double BiggestPrecedingSVFac(ref AllTable[] atRows, int curRow, int daysBack)
        {
            double biggest = -1;
            int downTo = curRow - daysBack;
            if (downTo < 2) downTo = 2;
            for (int i = curRow-1; i >= downTo; i--)
            {
                if (atRows[i].SVFac > biggest)
                {
                    biggest = atRows[i].SVFac;
                }
            }
            return biggest;
        }

        internal static void SlowVolumeFigureSVFac(ref AllTable[] atRows, SlowVolFigSVFacParam svf, int startRow, int endRow, out string[] auditSummary)
        {
            double denom;
            auditSummary = new string[] { };

            //1) prefill
            for (int i = startRow; i <= endRow; i++)  // 2 -> 10401
            {
                atRows[i].APSVac = 1;
            }

            // calculate SVFac
            for (int i = startRow; i <= endRow; i++)  // 2 -> 10401
            {
                denom = (atRows[i].SVc * atRows[i].APSVac);
                if (denom > 0)
                {
                    atRows[i].SVFac = atRows[i].SVa / denom;
                }
            //}
            // look for biggest SVFac
            //for (int i = startRow; i <= endRow; i++)
            //{
                double biggest = BiggestPrecedingSVFac(ref atRows, i, svf.Z);
                if (biggest > 1)
                {
                    atRows[i].APSVac *= (1 + svf.Y);
                }
                else
                {
                    atRows[i].APSVac *= Math.Pow(1 - svf.Y, svf.X);
                }
                //also carry this value onwards to last row
                for (int j = i+1; j <= endRow; j++)
                {
                    atRows[j].APSVac = atRows[i].APSVac;
                }
                //add to PtsVol
                if (biggest > svf.W)
                {
                    atRows[i].PtsVola += biggest;
                }
            }

            auditSummary = $"SlowVolumeFigureSVFac:\nSVFac computed for {startRow}-{endRow}.\nInspect the view".Split('\n');
        }

        private static double BiggestPrecedingSVFbd(ref AllTable[] atRows, int curRow, int daysBack)
        {
            double biggest = -1;
            int downTo = curRow - daysBack;
            if (downTo < 2) downTo = 2;
            for (int i = curRow-1; i >= downTo; i--)
            {
                if (atRows[i].SVFbd > biggest)
                {
                    biggest = atRows[i].SVFbd;
                }
            }
            return biggest;
        }


        internal static void SlowVolumeFigureSVFbd(ref AllTable[] atRows, SlowVolFigSVFbdParam svf, int startRow, int endRow, out string[] auditSummary)
        {
            double denom;
            auditSummary = new string[] { };

            //1) prefill
            for (int i = startRow; i <= endRow; i++)  // 2 -> 10401
            {
                atRows[i].APSVbd = 1;
            }

            // calculate SVFbd
            for (int i = startRow; i <= endRow; i++)  // 2 -> 10401
            {
                denom = (atRows[i].SVd * atRows[i].APSVbd);
                if (denom > 0)
                {
                    atRows[i].SVFbd = atRows[i].SVb / denom;
                }
            //}
            //for (int i = startRow; i <= endRow; i++)
            //{
                double biggest = BiggestPrecedingSVFbd(ref atRows, i, svf.Z);
                if (biggest > 1)
                {
                    atRows[i].APSVbd *= (1 + svf.Y);
                }
                else
                {
                    atRows[i].APSVbd *= (1 - svf.Y);
                }
                //also carry this value onwards to last row
                for (int j = i + 1; j <= endRow; j++)
                {
                    atRows[j].APSVbd = atRows[i].APSVbd;
                }
                // add to PtsVol
                if (biggest > svf.W)
                {
                    atRows[i].PtsVolb += biggest;
                }
            }
            auditSummary = $"SlowVolumeFigureSVFbd:\nSVFbd computed for {startRow}-{endRow}.\nInspect the view".Split('\n');
        }

        // Performs the full series of Calculations
        internal static void PerformShareCalculations(Share share, ref AllTable[] atSegment)
        {
            var slowPriceParams = Helper.UserSettings().ParamsSlowPrice;
            Calculations.MakeSlowPrices(ref atSegment, slowPriceParams, 2, 10401, out string[] auditSummary);

            Calculations.MakeFiveMinutesPriceGradients(ref atSegment, 2, 10401, out auditSummary);

            var directionAndTurningParams = Helper.UserSettings().ParamsDirectionAndTurning;
            Calculations.FindDirectionAndTurning(ref atSegment, directionAndTurningParams, 10298, 10401, out auditSummary);

            var fiveMinsGradientFigParam = Helper.UserSettings().ParamsFiveMinsGradientFigure;
            Calculations.FindFiveMinsGradientsFigurePGF(ref atSegment, fiveMinsGradientFigParam, 2, 10401, out auditSummary);

            Calculations.RelatedVolumeFigureOfBiggestPGF(ref atSegment, 10298, 10401, out auditSummary);

            var highLineParam = Helper.UserSettings().ParamsMakeHighLine;
            Calculations.MakeHighLineHL(ref atSegment, highLineParam, 2, 10401, out auditSummary);

            var lowLineParam = Helper.UserSettings().ParamsMakeLowLine;
            Calculations.MakeLowLineLL(ref atSegment, lowLineParam, 2, 10401, out auditSummary);

            var slowVolumeParam = Helper.UserSettings().ParamsMakeSlowVolume;
            Calculations.MakeSlowVolume(ref atSegment, slowVolumeParam, 2, 10401, out auditSummary);

            var slowVolFigSVFacParam = Helper.UserSettings().ParamsSlowVolFigSVFac;
            Calculations.SlowVolumeFigureSVFac(ref atSegment, slowVolFigSVFacParam, 2, 10401, out auditSummary);

            var slowVolFigSVFbdParam = Helper.UserSettings().ParamsSlowVolFigSVFbd;
            Calculations.SlowVolumeFigureSVFbd(ref atSegment, slowVolFigSVFbdParam, 2, 10401, out auditSummary);

            // we store the results of our Laziness determination in Col_2 row 1 so that the Overview
            // can initially be compiled without having to recalculate it
            var lazyShareParam = Helper.UserSettings().ParamsLazyShare;
            bool isLazy = OverviewCalcs.isLazyLast10Days(atSegment, lazyShareParam);

            //finally, copy row 10401 to row 1
            AllTable.CopySourceToTargetAllTableRow(ref atSegment, 10401, 1);
            //fix Row number
            atSegment[1].Row = 1;
            //and slot in the islazy flag 
            atSegment[1].Col_2 = isLazy ? 1.0 : 0;

        }

        //perform calculations based on passed in array of All-Table objects
        //MUST be called AFTER PerformShareCalculations
        internal static void Row1Calcs(Share share, ref AllTable[] atSegment)
        {
            //col 3: Sum of volumes (LastDayVolume) (49) 
            atSegment[1].FV = OverviewCalcs.SumOfVolumes(atSegment, 10298, 104);
            ////col 4: Price of row 10401 (11) 
            //oview.LastPrice = atSegment[10401].FP;
            ////col 5: Store the DayBeforePrice in Col_10 (Price of row 1040-104-1)
            atSegment[1].Col_10 = atSegment[10401 - 104].FP;
            ////col 6: Price of row 10401 / Price of row 10297
            //if (oview.DayBeforePrice > 0) { oview.PriceFactor = oview.LastPrice / oview.DayBeforePrice; }
            //// col 7: Price-Gradient PGc of row 1040 (23)
            //oview.LastPGc = atSegment[10401].PGc;
            //// col 8: 26 Price - Gradient PGd of row 10401
            //oview.LastPGd = atSegment[10401].PGd;
            // col 9: 13 The biggest PGa of row 10298 to 10401
            atSegment[1].PGa = OverviewCalcs.BiggestPGa(atSegment, 10298, 10401);
            // col 10: 18 The biggest PGF of All-table column 15 to 17, row 10298 to 10401
            atSegment[1].BigPGF = OverviewCalcs.BiggestPGFinBlock(atSegment, 10298, 10401);
            //// col 11: 30 The DHLFPc of row 10401
            //oview.LastDHLFPc = atSegment[10401].DHLFPc;
            ////col 12: 34 The DHLFPd of row 10401
            //oview.LastDHLFPd = atSegment[10401].DHLFPd;
            ////col 13: empty

            ////col 14: 40 The DLLFPc of row 10401
            //oview.LastDLLFPc = atSegment[10401].DLLFPc;
            ////col 15: 44 The DLLFPd of row 10401
            //oview.LastDLLFPd = atSegment[10401].DLLFPd;

            //col 16: 64 Sum of Points Gradient a of row 10298 to 10401
            atSegment[1].PGa = OverviewCalcs.SumOfPointsGradA(atSegment, 10298, 104);
            //col 17: 66 Sum of Points Gradient b   of row 10298 to 10401
            atSegment[1].PGb = OverviewCalcs.SumOfPointsGradB(atSegment, 10298, 104);
            //col 18: 68 Sum of Points Gradient c of row 10298 to 10401
            atSegment[1].PGc = OverviewCalcs.SumOfPointsGradC(atSegment, 10298, 104);

            ////col 19: 73 Sum of Points Volume a of row 10298 to 10401
            //oview.LastDaySumOfPtsVola = SumOfPointsVolA(atSegment, 10298, 104);
            ////col 20: 75 Sum of Points Volume b of row 10298 to 10401
            //oview.LastDaySumOfPtsVolb = SumOfPointsVolB(atSegment, 10298, 104);
            ////col 21: 77 Sum of Points Volume c of row 10298 to 10401
            //oview.LastDaySumOfPtsVolc = SumOfPointsVolC(atSegment, 10298, 104);
            ////col 22: 79 Sum of Points Volume d of row 10298 to 10401
            //oview.LastDaySumOfPtsVold = SumOfPointsVolD(atSegment, 10298, 104);

            //col 23: 70 Sum of Points Points High Line HLc of row 10298 to 10401
            atSegment[1].PtsHLc = OverviewCalcs.SumOfPointsHLc(atSegment, 10298, 104);
            //col 24: 71 Sum of Points Points High Line HLd of row 10298 to 10401
            atSegment[1].PtsHLd = OverviewCalcs.SumOfPointsHLd(atSegment, 10298, 104);
            ////col 25: duplicate of col 19???
            ////col 26: duplicate of col 20???
            ////col 27: ask Gunther
            ////col 28: ask Gunther
            atSegment[1].SVFac = OverviewCalcs.BiggestSVFac(atSegment, 10298, 10401);
            atSegment[1].SVFbd = OverviewCalcs.BiggestSVFbd(atSegment, 10298, 10401);

        }



    }

}