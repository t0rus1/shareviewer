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
            if (aus.ParamsDirectionAndTurning == null)
            {
                //not been set yet. set some defaults and save.
                aus.ParamsDirectionAndTurning = new DirectionAndTurningParam(0.99999, 1.01000, 100000, 1.0);
                shouldSave = true;
            }
            if (aus.ParamsFiveMinsGradientFigure == null)
            {
                //not yet set
                aus.ParamsFiveMinsGradientFigure = new FiveMinsGradientFigureParam(104, 999, 104, 1.0, 5.0, 1.0, 0, 0.0050, 0);
                shouldSave = true;
            }
            if (aus.ParamsMakeHighLine == null)
            {
                //not yet set
                aus.ParamsMakeHighLine = new MakeHighLineParam(0, 0.001, 0.0005);
                shouldSave = true;
            }
            if (aus.ParamsMakeLowLine == null)
            {
                //not yet set
                aus.ParamsMakeLowLine = new MakeLowLineParam(0, 0.01, 0.005);
                shouldSave = true;
            }
            if (aus.ParamsMakeSlowVolume == null)
            {
                //not yet set
                aus.ParamsMakeSlowVolume = new MakeSlowVolumeParam(0, 0.9999, 0.1, 0.1, 0.1, 0.1);
                shouldSave = true;
            }
            if (shouldSave)
            {
                aus.Save();
            }


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
        internal static bool LazyShare(AllTable[] bands, LazyShareParam Z, int startRow, int endRow, out string[] auditSummary)
        {
            bool isLazy = false;
            auditSummary = "".Split('\n');
            int numBands = endRow - startRow + 1; // normally 1040 bands = 10 days

            if (bands.Count() > 0 && numBands > 2)
            {
                //we must skip startRow rows because 'Row' starts at -1
                double totalFV = bands.Skip(startRow).Take(numBands).Sum(atRec => atRec.FV);
                double numDays = numBands / 104; // there are 104 bands per day
                double avgDailyVolume = totalFV / numDays;
                double effectivePrice = bands[numBands - 1].FP;
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
                auditSummary = $"{numBands} processed.Please inspect the view for results.".Split('\n');
            }
            else
            {
                auditSummary = $"No bands to work with".Split('\n');
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
                auditSummary = $"{numBands} processed.\nPlease inspect the view for results.".Split('\n');
            }
            else
            {
                auditSummary = $"No bands to work with".Split('\n');
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
                    bands[endRow].PtsGradC += 0.1; // Direction
                    //if any PGc from row 10298 till row 10400 is smaller than PGcThreshold
                    for (int i = startRow; i < endRow; i++)
                    {
                        if (bands[i].PGc < dtp.PGcThreshold)
                        {
                            jackPot = true;
                            bands[i].PtsGradC += 3;
                            auditSummary = $"The PGc value in row {endRow} ({bands[endRow].PGc}) DOES exceed Z ({dtp.Z})\nplus PGc value in row {i} ({bands[i].PGc}) IS < threshold ({dtp.PGcThreshold})\nPlease inspect the view for results.".Split('\n');
                            break;
                        }
                    }
                    if (!jackPot)
                    {
                        auditSummary = $"The PGc value in row {endRow} ({bands[endRow].PGc}) does exceed Z ({dtp.Z})\nBUT no PGc value in rows {startRow} to {endRow}  IS < threshold ({dtp.PGcThreshold})".Split('\n');
                    }
                }
                else
                {
                    auditSummary = $"Last PGc value ({bands[endRow].PGc}) does NOT exceed Z ({dtp.Z})\nNo changes made.".Split('\n');
                }
            }
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
            for (int i = startRow; i <= endRow - 2; i++)  // 1029 -> 10399
            {
                atRows[i].PGFrowx17 = (atRows[i].PGa * atRows[i + 1].PGa * atRows[i + 2].PGa) / atRows[i].APpg;
            }
            auditSegment = $"Columns APpg, PGFrowx15, PGFrowx16, PGFrowx17 filled from row {startRow}-{endRow}.\nPlease inspect the view";

            // from Gunther
            //2) look in the fields of columns 15 to17 and the last Z rows for the biggest figure. Z = 104 … 999  	
            //   Z is a variable the user can choose.

            //find biggest figure in last Z rows amongst PGFrowx15, PGFrowx16, PGFrowx17
            double biggest = 0;
            int rowBig = -1;
            for (int i = endRow-(calcFiveMinsGradientFigureParam.Z-1); i <= endRow; i++)
            {
                if (atRows[i].PGFrowx15 > biggest)
                {
                    biggest = atRows[i].PGFrowx15;
                    rowBig = i;
                }
                if (atRows[i].PGFrowx16 > biggest)
                {
                    biggest = atRows[i].PGFrowx16;
                    rowBig = i;
                }
                if (atRows[i].PGFrowx17 > biggest)
                {
                    biggest = atRows[i].PGFrowx17;
                    rowBig = i;
                }
            }
            if (rowBig != -1) {
                if (biggest > 1.0)
                {
                    for (int i = rowBig; i <= endRow; i++)
                    {
                        atRows[i].APpg *= (1 + calcFiveMinsGradientFigureParam.Y);
                    }
                }
                else
                {
                    for (int i = rowBig; i <= endRow; i++)
                    {
                        atRows[i].APpg *= Math.Pow(1 - calcFiveMinsGradientFigureParam.Y, calcFiveMinsGradientFigureParam.X);
                    }
                }
            }
            else
            {
                auditSegment += $"\nWarning: Unable to locate biggest figure in the last {calcFiveMinsGradientFigureParam.Z} rows amongst PGFrowx15, PGFrowx16, PGFrowx17";
                auditSummary = auditSegment.Split('\n');
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
                atRows[i].HLc = atRows[i - 1].HLc * atRows[i].PGc * (1 - calcHighLineParam.Z);
                if (atRows[i].HLc < atRows[i].FP)
                {
                    // 2) and 3)
                    atRows[i].HLc = atRows[i].FP;
                    atRows[i].PtsHLc += 5;
                }
                atRows[i].HLd = atRows[i - 1].HLd * atRows[i].PGd * (1 - calcHighLineParam.Z);
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

            auditSummary = $"HLc, HLd, PtsHLc, PtsHLd, DHLFPc and DHLFPd computed from row {startRow}-{endRow}.\nPlease inspect the view".Split('\n');

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
                atRows[i].LLc = atRows[i - 1].LLc * atRows[i].PGc * (1 + calcLowLineParam.Z);
                if (atRows[i].LLc > atRows[i].FP)
                {
                    // 2) and 3)
                    atRows[i].LLc = atRows[i].FP;
                    atRows[i].PtsLLc += 5;
                }
                atRows[i].LLd = atRows[i - 1].LLd * atRows[i].PGd * (1 + calcLowLineParam.Z);
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

            auditSummary = $"LLc, LLd, PtsLLc, PtsLLd, DLLFPc and DLLFPd computed from row {startRow}-{endRow}.\nPlease inspect the view".Split('\n');

        }

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
            for (int i = startRow+1; i <= endRow; i++)  // 2 -> 10401
            {
                if (atRows[i-1].SVa < atRows[i].FV)
                {
                    if (i < 10401)
                    {
                        //case 1, SVa
                        diffTerm = atRows[i].FV - atRows[i - 1].SVa;
                        powTerm = Math.Pow(diffTerm, svp.Ya);
                        atRows[i].SVa = atRows[i - 1].SVa + Convert.ToUInt32(powTerm);
                        //       SVb
                        diffTerm = atRows[i].FV - atRows[i - 1].SVb;
                        powTerm = Math.Pow(diffTerm, svp.Yb);
                        atRows[i].SVb = atRows[i - 1].SVb + Convert.ToUInt32(powTerm);
                        //       SVc
                        diffTerm = atRows[i].FV - atRows[i - 1].SVc;
                        powTerm = Math.Pow(diffTerm, svp.Yc);
                        atRows[i].SVc = atRows[i - 1].SVc + Convert.ToUInt32(powTerm);
                        //       SVd
                        diffTerm = atRows[i].FV - atRows[i - 1].SVd;
                        powTerm = Math.Pow(diffTerm, svp.Yd);
                        atRows[i].SVd = atRows[i - 1].SVd + Convert.ToUInt32(powTerm);
                    }
                    else if (i == 10401)
                    {
                        //special case i = 10401, use row 10400's FV
                        //case 1, SVa
                        diffTerm = atRows[10400].FV - atRows[i - 1].SVa;
                        powTerm = Math.Pow(diffTerm, svp.Ya);
                        atRows[i].SVa = atRows[i - 1].SVa + Convert.ToUInt32(powTerm);
                        //       SVb
                        diffTerm = atRows[10400].FV - atRows[i - 1].SVb;
                        powTerm = Math.Pow(diffTerm, svp.Yb);
                        atRows[i].SVb = atRows[i - 1].SVb + Convert.ToUInt32(powTerm);
                        //       SVc
                        diffTerm = atRows[10400].FV - atRows[i - 1].SVc;
                        powTerm = Math.Pow(diffTerm, svp.Yc);
                        atRows[i].SVc = atRows[i - 1].SVc + Convert.ToUInt32(powTerm);
                        //       SVd
                        diffTerm = atRows[10400].FV - atRows[i - 1].SVd;
                        powTerm = Math.Pow(diffTerm, svp.Yd);
                        atRows[i].SVd = atRows[i - 1].SVd + Convert.ToUInt32(powTerm);
                    }
                }
                else if (atRows[i - 1].SVa > atRows[i].FV)
                {
                    if (i < 10401)
                    {
                        //case 2, SVa
                        diffTerm = atRows[i - 1].SVa - atRows[i].FV;
                        powTerm = Math.Pow(diffTerm, svp.Ya);
                        atRows[i].SVa = atRows[i - 1].SVa - Convert.ToUInt32(powTerm);
                        //case 2, SVb
                        diffTerm = atRows[i - 1].SVb - atRows[i].FV;
                        powTerm = Math.Pow(diffTerm, svp.Yb);
                        atRows[i].SVb = atRows[i - 1].SVb - Convert.ToUInt32(powTerm);
                        //case 2, SVc
                        diffTerm = atRows[i - 1].SVc - atRows[i].FV;
                        powTerm = Math.Pow(diffTerm, svp.Yc);
                        atRows[i].SVc = atRows[i - 1].SVc - Convert.ToUInt32(powTerm);
                        //case 2, SVd
                        diffTerm = atRows[i - 1].SVd - atRows[i].FV;
                        powTerm = Math.Pow(diffTerm, svp.Yd);
                        atRows[i].SVd = atRows[i - 1].SVd - Convert.ToUInt32(powTerm);
                    }
                    else if (i == 10401)
                    {
                        //case 2, SVa
                        diffTerm = atRows[i - 1].SVa - atRows[10400].FV;
                        powTerm = Math.Pow(diffTerm, svp.Ya);
                        atRows[i].SVa = atRows[i - 1].SVa - Convert.ToUInt32(powTerm);
                        //case 2, SVb
                        diffTerm = atRows[i - 1].SVb - atRows[10400].FV;
                        powTerm = Math.Pow(diffTerm, svp.Yb);
                        atRows[i].SVb = atRows[i - 1].SVb - Convert.ToUInt32(powTerm);
                        //case 2, SVc
                        diffTerm = atRows[i - 1].SVc - atRows[10400].FV;
                        powTerm = Math.Pow(diffTerm, svp.Yc);
                        atRows[i].SVc = atRows[i - 1].SVc - Convert.ToUInt32(powTerm);
                        //case 2, SVd
                        diffTerm = atRows[i - 1].SVd - atRows[10400].FV;
                        powTerm = Math.Pow(diffTerm, svp.Yd);
                        atRows[i].SVd = atRows[i - 1].SVd - Convert.ToUInt32(powTerm);
                    }
                }

            }
            auditSummary = $"SVa,SVb,SVc,SVd computed from row {startRow}-{endRow}.\nPlease inspect the view".Split('\n');
        }








        // Performs the full series of Calculations
        internal static void PerformShareCalculations(Share share, AllTable[] atSegment)
        {
            var slowPriceParams = Helper.GetAppUserSettings().ParamsSlowPrice;
            Calculations.MakeSlowPrices(ref atSegment, slowPriceParams, 2, 10401, out string[] auditSummary);

            Calculations.MakeFiveMinutesPriceGradients(ref atSegment, 2, 10401, out auditSummary);

            var directionAndTurningParams = Helper.GetAppUserSettings().ParamsDirectionAndTurning;
            Calculations.FindDirectionAndTurning(ref atSegment, directionAndTurningParams, 10298, 10401, out auditSummary);

            var fiveMinsGradientFigParam = Helper.GetAppUserSettings().ParamsFiveMinsGradientFigure;
            Calculations.FindFiveMinsGradientsFigurePGF(ref atSegment, fiveMinsGradientFigParam, 10298, 10401, out auditSummary);

            Calculations.RelatedVolumeFigureOfBiggestPGF(ref atSegment, 10298, 10401, out auditSummary);

            var highLineParam = Helper.GetAppUserSettings().ParamsMakeHighLine;
            Calculations.MakeHighLineHL(ref atSegment, highLineParam, 2, 10401, out auditSummary);

            var lowLineParam = Helper.GetAppUserSettings().ParamsMakeLowLine;
            Calculations.MakeLowLineLL(ref atSegment, lowLineParam, 2, 10401, out auditSummary);

            var slowVolumeParam = Helper.GetAppUserSettings().ParamsMakeSlowVolume;
            Calculations.MakeSlowVolume(ref atSegment, slowVolumeParam, 2, 10401, out auditSummary);

        }




    }

}