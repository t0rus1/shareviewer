using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{

    public enum Comparison
    {
        LessThan = -1,
        GreaterThan = 1
    }

    public class OverviewFilter
    {
        public OverviewFilter(string propName, Comparison kind, string header)
        {
            this.propName = propName;
            compareKind = kind;
            comparand = 0;
            columnHeader = header;
            apply = false;
       }

        private string propName;
        private bool apply;
        private Comparison compareKind;
        private double comparand;
        private string columnHeader;

        public string PropName { get => propName; set => propName = value; }
        public bool Apply { get => apply; set => apply = value; }
        public Comparison CompareKind { get => compareKind; set => compareKind = value; }
        public double Comparand { get => comparand; set => comparand = value; }
        public string ColumnHeader { get => columnHeader; set => columnHeader = value; }


        public static void ApplyFilterToList(OverviewFilter filter, ref List<Overview> sharesOverview)
        {
            //we have to cater for 27 cases
            switch (filter.PropName)
            {
                case "LastDayVol":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDayVol >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDayVol < filter.Comparand).ToList();
                    }
                    break;
                case "LastPrice":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastPrice >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastPrice < filter.Comparand).ToList();
                    }
                    break;
                case "DayBeforePrice":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.DayBeforePrice >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.DayBeforePrice < filter.Comparand).ToList();
                    }
                    break;
                case "PriceFactor":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.PriceFactor >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.PriceFactor < filter.Comparand).ToList();
                    }
                    break;
                case "LastPGc":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastPGc >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastPGc < filter.Comparand).ToList();
                    }
                    break;
                case "LastPGd":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastPGd >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastPGd < filter.Comparand).ToList();
                    }
                    break;
                case "BigLastDayPGa":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.BigLastDayPGa >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.BigLastDayPGa < filter.Comparand).ToList();
                    }
                    break;
                case "BigLastDayPGF":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.BigLastDayPGF >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.BigLastDayPGF < filter.Comparand).ToList();
                    }
                    break;
                case "LastDHLFPc":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDHLFPc >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDHLFPc < filter.Comparand).ToList();
                    }
                    break;
                case "LastDHLFPd":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDHLFPd >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDHLFPd < filter.Comparand).ToList();
                    }
                    break;
                case "LastDLLFPc":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDLLFPc >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDLLFPc < filter.Comparand).ToList();
                    }
                    break;
                case "LastDLLFPd":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDLLFPd >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDLLFPd < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPGa":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPGa >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPGa < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPGb":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPGb >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPGb < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPGc":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPGc >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPGc < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPtsVola":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVola >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVola < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPtsVolb":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVolb >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVolb < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPtsVolc":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVolc >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVolc < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPtsVold":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVold >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVold < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPtsHLc":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsHLc >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsHLc < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPtsHLd":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsHLd >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsHLd < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPtsVolaa":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVolaa >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVolaa < filter.Comparand).ToList();
                    }
                    break;
                case "LastDaySumOfPtsVolbb":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVolbb >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.LastDaySumOfPtsVolbb < filter.Comparand).ToList();
                    }
                    break;
                case "SumOfSumCols64_79":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.SumOfSumCols64_79 >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.SumOfSumCols64_79 < filter.Comparand).ToList();
                    }
                    break;
                case "SumOfSumCols64_79trf":
                    if (filter.CompareKind == Comparison.GreaterThan)
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.SumOfSumCols64_79trf >= filter.Comparand).ToList();
                    }
                    else
                    {
                        sharesOverview = sharesOverview.Where(ov => ov.SumOfSumCols64_79trf < filter.Comparand).ToList();
                    }
                    break;

                default:
                    break;
            }
        }



    }
}
