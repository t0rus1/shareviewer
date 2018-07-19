﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{

    public class HintAttribute : Attribute
    {
        public string Hint { get; set; }

        public HintAttribute(string hint)
        {
            this.Hint = hint;
        }
    }

    //Every All-table has 84 columns and 10401 rows plus its head-row.
    //Empty columns are to have opportunities for addings.
    //Row 1 is a particular row to be transferred to the table named “Overview”. 
    //Cols 1,7,8,9 are prefilled as indicated. all remaining columns up to col 63 are prefilled with a 1
    //while cols 64 to 84 are prefilled with a 0
    [Serializable]
    public class AllTable
    {
        //cols 1-10
        private int row; // XXXXXX   (runs from row 0 (headings), row 1(special) ... to row 10401) 
        private double col_2;
        private string date; //XXXXXX
        private double col_4;
        private string day; //XXX
        private double col_6;
        private string timeFrom; //XX:XX:XX (from 09:00:00 to 17:35:00 ie 104 5 min ranges per day for 100 days)
        private string timeTo; //XX:XX:XX (from 09:04:49 to 17:24:59...ie 104 5 min ranges per day for 100 days)
        private int f; //X (Five-minutes section no.) its value will be 1 less than Row, so it runs from -1 to 10400
        private double col_10;
        //cols 11-20
        private double fP; // XXX,XXX (Five-minutes last price)
        private double sPa; //XXX,XXXX (Slow price a)
        private double pGa; //X,XXXXX (Price Gradient a)
        private double aPpg; //X,XXXXXX (Self adjusting parameter)
        private double pGFrowx15; //X,XXXXX
        private double pGFrowx16; //X,XXXXX
        private double pGFrowx17; //X,XXXXX
        private double bigPGF; //X,XXXXX  ????? not in section 5 of Gunther's notes
        private double sPb; //XXX,XXXX (Slow price b)
        private double pGb; //X,XXXXX (Price Gradient b)
        //cols 21-30
        private double col_21;
        private double sPc; //XXX,XXXX (Slow price c)
        private double pGc; //X,XXXXX (Price Gradient c)
        private double col_24;
        private double sPd; //XXX,XXXX (Slow price d)
        private double pGd; //X,XXXXX (Price Gradient d)
        private double col_27;
        private double col_28;
        private double hLc; //XXX,XXX (Highline c)
        private double dHLFPc; //X,XXX (Distance HL to FP)
        //cols 31-40
        private double col_31;
        private double col_32;
        private double hLd; //XXX,XXX (Highline d)
        private double dHLFPd; //X,XXX (Distance HL to FP)
        private double col_35;
        private double col_36;
        private double col_37;
        private double col_38;
        private double lLc; //XXX,XXX (Lowline c)
        private double dLLFPc;// X,XXX (Distance LL to FP)
        //cols 41-50
        private double col_41;
        private double col_42;
        private double lLd; // XXX,XXX (Lowline d)
        private double dLLFPd; //X,XXX (Distance LL to FP)
        private double col_45;
        private double col_46;
        private double col_47;
        private double col_48;
        private UInt32 fV; // XXXXXXX (Sum of the Volume of the Five minutes section: Five minutes volume)
        private double col_50;
        //cols 51-60
        private UInt32 sVa; //XXXXXXX (Slow Volume a)
        private double aPSVac; //XXX,X (Self adjusting parameter c)
        private double sVFac; //X,XXXXXX (Slow Volume Figure)
        private double col_54; // 54 Do ignore: 
        private UInt32 sVb; //XXXXXXX (Slow Volume b)
        private double aPSVbd; //XXX,X (Self adjusting parameter)
        private double sVFbd; //X,XXXXXX (Slow Volume Figure)
        private double col_58;
        private UInt32 sVc; //XXXXXXX (Slow volume c)
        private double rPGFV; //XXX,X (Related PGF volume Figure)
        //cols 61-70
        private double col_61;
        private UInt32 sVd; //XXXXXXX (Slow volume d)
        private double col_63;
        private double ptsGradA; // XXX,X (Points Gradient a)
        private double col_65;
        private double ptsGradB; // XXX,X (Points Gradient b)
        private double col_67;
        private double ptsGradC; // XXX,X (Points Gradient c)
        private double col_69;
        private double ptsHLc; //X (Points Highline c)
        //cols 71-80
        private double ptsHLd; //X (Points Highline d)
        private double col_72;
        private double ptsLLc; //XXX (Points Lowline c)
        private double ptsLLd; //XXX (Points Lowline d)
        private double col_75;
        private double ptsVola; //XXX,X (Points Volume a)
        private double ptsVolb; //XXX,X (Points Volume b)
        private double col_78;
        private double col_79;
        private double col_80;
        //cols 81-84
        private double col_81;
        private double col_82;
        private double sumRow1Only;
        private double sameAs83DiffTreatment;


        //properties
        [Hint("Row number")]
        public int Row { get => row; set => row = value; }              //0
        [Hint("")]
        public double Col_2 { get => col_2; set => col_2 = value; }     //
        [Hint("Date of trade")]
        public string Date { get => date; set => date = value; }        //2
        [Hint("")]
        public double Col_4 { get => col_4; set => col_4 = value; }     //
        [Hint("Day of trade")]
        public string Day { get => day; set => day = value; }           //4
        [Hint("")]
        public double Col_6 { get => col_6; set => col_6 = value; }     //
        [Hint("Band start time")]
        public string TimeFrom { get => timeFrom; set => timeFrom = value; }  //6
        [Hint("Band end time")]
        public string TimeTo { get => timeTo; set => timeTo = value; }        //7 
        [Hint("Five-minutes section no.")]
        public int F { get => f; set => f = value; }                          //8
        [Hint("")]
        public double Col_10 { get => col_10; set => col_10 = value; }

        //11-20
        [Hint("Five-minutes last price")]
        public double FP { get => fP; set => fP = value; }
        [Hint("Slow price a")]
        public double SPa { get => sPa; set => sPa = value; }
        [Hint("Price-Gradient a")]
        public double PGa { get => pGa; set => pGa = value; }
        [Hint("Self adjusting parameter")]
        public double APpg { get => aPpg; set => aPpg = value; }
        [Hint("")]
        public double PGFrowx15 { get => pGFrowx15; set => pGFrowx15 = value; }
        [Hint("")]
        public double PGFrowx16 { get => pGFrowx16; set => pGFrowx16 = value; }
        [Hint("")]
        public double PGFrowx17 { get => pGFrowx17; set => pGFrowx17 = value; }
        [Hint("The biggest PGF of PGFrowx15, PGFrowx16, PGFrowx16 row 10298 to 10401")]
        public double BigPGF { get => bigPGF; set => bigPGF = value; }
        [Hint("Slow price b")]
        public double SPb { get => sPb; set => sPb = value; }
        [Hint("Price Gradient b")]
        public double PGb { get => pGb; set => pGb = value; }

        //21-30
        [Hint("")]
        public double Col_21 { get => col_21; set => col_21 = value; }
        [Hint("Slow price c")]
        public double SPc { get => sPc; set => sPc = value; }
        [Hint("Proce Gradient c")]
        public double PGc { get => pGc; set => pGc = value; }
        [Hint("")]
        public double Col_24 { get => col_24; set => col_24 = value; }
        [Hint("Slow price d")]
        public double SPd { get => sPd; set => sPd = value; }
        [Hint("Price Gradient d")]
        public double PGd { get => pGd; set => pGd = value; }
        [Hint("")]
        public double Col_27 { get => col_27; set => col_27 = value; }
        [Hint("")]
        public double Col_28 { get => col_28; set => col_28 = value; }
        [Hint("Highline c")]
        public double HLc { get => hLc; set => hLc = value; }
        [Hint("Distance HL to FP")]
        public double DHLFPc { get => dHLFPc; set => dHLFPc = value; }
        
        //31-40
        [Hint("")]
        public double Col_31 { get => col_31; set => col_31 = value; }
        [Hint("")]
        public double Col_32 { get => col_32; set => col_32 = value; }
        [Hint("Highline d")]
        public double HLd { get => hLd; set => hLd = value; }
        [Hint("Distance HL to FP")]
        public double DHLFPd { get => dHLFPd; set => dHLFPd = value; }
        [Hint("")]
        public double Col_35 { get => col_35; set => col_35 = value; }
        [Hint("")]
        public double Col_36 { get => col_36; set => col_36 = value; }
        [Hint("")]
        public double Col_37 { get => col_37; set => col_37 = value; }
        [Hint("")]
        public double Col_38 { get => col_38; set => col_38 = value; }
        [Hint("Lowline c")]
        public double LLc { get => lLc; set => lLc = value; }
        [Hint("Distance LL to FP")]
        public double DLLFPc { get => dLLFPc; set => dLLFPc = value; }
        //41-50
        [Hint("")]
        public double Col_41 { get => col_41; set => col_41 = value; }
        [Hint("")]
        public double Col_42 { get => col_42; set => col_42 = value; }
        [Hint("Lowline d")]
        public double LLd { get => lLd; set => lLd = value; }
        [Hint("Distance LL to FP")]
        public double DLLFPd { get => dLLFPd; set => dLLFPd = value; }
        [Hint("")]
        public double Col_45 { get => col_45; set => col_45 = value; }
        [Hint("")]
        public double Col_46 { get => col_46; set => col_46 = value; }
        [Hint("")]
        public double Col_47 { get => col_47; set => col_47 = value; }
        [Hint("")]
        public double Col_48 { get => col_48; set => col_48 = value; }
        [Hint("Sum of the Volume of the Five minutes section")]
        public UInt32 FV { get => fV; set => fV = value; }
        [Hint("")]
        public double Col_50 { get => col_50; set => col_50 = value; }
        //51-60
        [Hint("Slow Volume a")]
        public UInt32 SVa { get => sVa; set => sVa = value; }
        [Hint("Self adjusting parameter")]
        public double APSVac { get => aPSVac; set => aPSVac = value; }
        [Hint("Slow Volume figure")]
        public double SVFac { get => sVFac; set => sVFac = value; }
        [Hint("Do ignore")]
        public double Col_54 { get => col_54; set => col_54 = value; }
        [Hint("Slow Volume b")]
        public UInt32 SVb { get => sVb; set => sVb = value; }
        [Hint("Self adjusting parameter")]
        public double APSVbd { get => aPSVbd; set => aPSVbd = value; }
        [Hint("Slow Volume figure")]
        public double SVFbd { get => sVFbd; set => sVFbd = value; }
        [Hint("")]
        public double Col_58 { get => col_58; set => col_58 = value; }
        [Hint("Slow Volume c")]
        public UInt32 SVc { get => sVc; set => sVc = value; }
        [Hint("Related PGF volume Figure")]
        public double RPGFV { get => rPGFV; set => rPGFV = value; }
        //61-70
        [Hint("")]
        public double Col_61 { get => col_61; set => col_61 = value; }
        [Hint("Slow Volume d")]
        public UInt32 SVd { get => sVd; set => sVd = value; }
        [Hint("")]
        public double Col_63 { get => col_63; set => col_63 = value; }
        [Hint("Points Gradient a")]
        public double PtsGradA { get => ptsGradA; set => ptsGradA = value; }
        [Hint("")]
        public double Col_65 { get => col_65; set => col_65 = value; }
        [Hint("Points Gradient b")]
        public double PtsGradB { get => ptsGradB; set => ptsGradB = value; }
        [Hint("")]
        public double Col_67 { get => col_67; set => col_67 = value; }
        [Hint("Points Gradient c")]
        public double PtsGradC { get => ptsGradC; set => ptsGradC = value; }
        [Hint("")]
        public double Col_69 { get => col_69; set => col_69 = value; }
        [Hint("Points High Line c")]
        public double PtsHLc { get => ptsHLc; set => ptsHLc = value; }
        //71-80
        [Hint("Points High Line d")]
        public double PtsHLd { get => ptsHLd; set => ptsHLd = value; }
        [Hint("")]
        public double Col_72 { get => col_72; set => col_72 = value; }
        [Hint("Points Low Line c")]
        public double PtsLLc { get => ptsLLc; set => ptsLLc = value; }
        [Hint("Points Low Line d")]
        public double PtsLLd { get => ptsLLd; set => ptsLLd = value; }
        [Hint("")]
        public double Col_75 { get => col_75; set => col_75 = value; }
        [Hint("Points Volume a")]
        public double PtsVola { get => ptsVola; set => ptsVola = value; }
        [Hint("Points Volume b")]
        public double PtsVolb { get => ptsVolb; set => ptsVolb = value; }
        [Hint("")]
        public double Col_78 { get => col_78; set => col_78 = value; }
        [Hint("")]
        public double Col_79 { get => col_79; set => col_79 = value; }
        [Hint("")]
        public double Col_80 { get => col_80; set => col_80 = value; }
        //81-84
        [Hint("")]
        public double Col_81 { get => col_81; set => col_81 = value; }
        [Hint("")]
        public double Col_82 { get => col_82; set => col_82 = value; }
        [Hint("Sum of points of columns 67 to 79 (Only for row 1)")]
        public double SumRow1Only { get => sumRow1Only; set => sumRow1Only = value; }
        [Hint("The same as column 83, but when transfer it to the Overview, it is another treatment.")]
        public double SameAs83DiffTreatment { get => sameAs83DiffTreatment; set => sameAs83DiffTreatment = value; }

        public override String ToString()
        {
            return $"{date},{f},{fP},{fV}";
        }

        public static List<int> InitialViewIndices()
        {
            return new List<int>() {
                NameToIndex("Row"),
                NameToIndex("Date"),
                NameToIndex("Day"),
                NameToIndex("TimeFrom"),
                NameToIndex("TimeTo"),
                NameToIndex("F"),
                NameToIndex("FP"),
                NameToIndex("FV"),
            };
        }

        public static int NameToIndex(string colName)
        {
            switch (colName)
            {
                case "Row": return 0;
                case "Col_2": return 1;
                case "Date":  return 2;
                case "Col_4": return 3;
                case "Day":   return 4;
                case "Col_6": return 5;
                case "TimeFrom": return 6;
                case "TimeTo":  return 7;
                case "F": return  8;
                case "Col_10": return  9;
                case "FP": return  10;
                case "SPa": return  11;
                case "PGa": return  12;
                case "APpg": return  13;
                case "PGFrowx15": return  14;
                case "PGFrowx16": return  15;
                case "PGFrowx17": return  16;
                case "BigPGF": return  17;
                case "SPb": return  18;
                case "PGb": return  19;
                case "Col_21": return  20;
                case "SPc": return  21;
                case "PGc": return  22;
                case "Col_24": return  23;
                case "SPd": return  24;
                case "PGd": return  25;
                case "Col_27": return  26;
                case "Col_28": return  27;
                case "HLc": return  28;
                case "DHLFPc": return  29;
                case "Col_31": return  30;
                case "Col_32": return  31;
                case "HLd": return  32;
                case "DHLFPd": return  33;
                case "Col_35": return  34;
                case "Col_36": return  35;
                case "Col_37": return  36;
                case "Col_38": return  37;
                case "LLc": return  38;
                case "DLLFPc": return  39;
                case "Col_41": return  40;
                case "Col_42": return  41;
                case "LLd": return  42;
                case "DLLFPd": return  43;
                case "Col_45": return  44;
                case "Col_46": return  45;
                case "Col_47": return  46;
                case "Col_48": return  47;
                case "FV": return  48;
                case "Col_50": return  49;
                case "SVa": return  50;
                case "APSVac": return  51;
                case "SVFac": return  52;
                case "Col_54": return  53;
                case "SVb": return  54;
                case "APSVbd": return  55;
                case "SVFbd": return  56;
                case "Col_58": return  57;
                case "SVc": return  58;
                case "RPGFV": return  59;
                case "Col_61": return  60;
                case "SVd": return  61;
                case "Col_63": return  62;
                case "PtsGradA": return  63;
                case "Col_65": return  64;
                case "PtsGradB": return  65;
                case "Col_67": return  66;
                case "PtsGradC": return  67;
                case "Col_69": return  68;
                case "PtsHLc": return  69;
                case "PtsHLd": return  70;
                case "Col_72": return  71;
                case "PtsLLc": return  72;
                case "PtsLLd": return  73;
                case "Col_75": return  74;
                case "PtsVola": return  75;
                case "PtsVolb": return  76;
                case "Col_78": return  77;
                case "Col_79": return  78;
                case "Col_80": return  79;
                case "Col_81": return  80;
                case "Col_82": return  81;
                case "SumRow1Only": return  82;
                case "SameAs83DiffTreatment": return  83;
                default: return  -1;
            }

        }

        // get the hint attribute from a property name
        public static string PropNameToHint(string colName)
        {
            return typeof(AllTable).GetProperty(colName).GetCustomAttribute<HintAttribute>().Hint;
        }

    }

    public static class AllTableFactory
    {
        public static AllTable InitialRow(int row, string date, string day, string timefrom, string timeto)
        {
            AllTable at = new AllTable();

            //cols 1-10
            at.Row = row; // XXXXXX   (runs from row 0 (headings), row 1(special) ... to row 10401) 
            at.Col_2 = 1;
            at.Date = date; //YYMMDD
            at.Col_4 = 1;
            at.Day = day; //XXX
            at.Col_6 = 1;
            at.TimeFrom = timefrom; //XX:XX:XX (from 09:00:00 to 17:35:00 ie 104 5 min ranges per day for 100 days)
            at.TimeTo = timeto;  //XX:XX:XX (from 09:04:49 to 17:24:59...ie 104 5 min ranges per day for 100 days)
            at.F = row-1;   //X (Five-minutes section no.) its value will be 1 less than Row, so it runs from -1 to 10400
            at.Col_10 = 1;

            //cols 11-20
            //public double FP; // XXX,XXX (Five-minutes last price)
            //public double SPa; //XXX,XXXX (Slow price a)
            //public double PGa; //X,XXXXX (Price Gradient a)
            //public double APpg; //X,XXXXXX (Self adjusting parameter)
            //public double PGFrowx15; //X,XXXXX
            //public double PGFrowx16; //X,XXXXX
            //public double PGFrowx17; //X,XXXXX
            //public double BigPGF; //X,XXXXX  ????? not in section 5 of Gunther's notes
            //public double SPb; //XXX,XXXX (Slow price b)
            //public double PGb; //X,XXXXX (Price Gradient b)

            //cols 21-30
            at.Col_21 = 1;
            //public double SPc; //XXX,XXXX (Slow price c)
            //public double PGc; //X,XXXXX (Price Gradient c)
            at.Col_24 = 1;
            //public double SPd; //XXX,XXXX (Slow price d)
            //public double PGd; //X,XXXXX (Price Gradient d)
            at.Col_27 = 1;
            at.Col_28 = 1;
            //public double HLc; //XXX,XXX (Highline c)
            //public double DHLFPc; //X,XXX (Distance HL to FP)


            //cols 31-40
            at.Col_31 = 1;
            at.Col_32 = 1;
            //public double HLd; //XXX,XXX (Highline d)
            //public double DHLFPd; //X,XXX (Distance HL to FP)
            at.Col_35 = 1;
            at.Col_36 = 1;
            at.Col_37 = 1;
            at.Col_38 = 1;
            //public double LLc; //XXX,XXX (Lowline c)
            //public double DLLFPc;// X,XXX (Distance LL to FP)

            //cols 41-50
            at.Col_41 = 1;
            at.Col_42 = 1;
            //public double LLd; // XXX,XXX (Lowline d)
            //public double DLLFPd; //X,XXX (Distance LL to FP)
            at.Col_45 = 1;
            at.Col_46 = 1;
            at.Col_47 = 1;
            at.Col_48 = 1;
            //public UInt32 FV; // XXXXXXX (Sum of the Volume of the Five minutes section: Five minutes volume)
            at.Col_50 = 1;

            //cols 51-60
            //public UInt32 SVa; //XXXXXXX (Slow Volume a)
            //public double APSVac; //XXX,X (Self adjusting parameter c)
            //public double SVFac; //X,XXXXXX (Slow Volume Figure)
            at.Col_54 = 1; // 54 Do ignore: 
            //public UInt32 SVb; //XXXXXXX (Slow Volume b)
            //public double APSVbd; //XXX,X (Self adjusting parameter)
            //public double SVFbd; //X,XXXXXX (Slow Volume Figure)
            at.Col_58 = 1;
            //public UInt32 SVc; //XXXXXXX (Slow volume c)
            //public double RPGFV; //XXX,X (Related PGF volume Figure)

            //cols 61-70
            at.Col_61 = 1;
            //public UInt32 SVd; //XXXXXXX (Slow volume d)
            at.Col_63 = 1;
            //public double PtsGradA; // XXX,X (Points Gradient a)
            at.Col_65 = 0; // <--------------------------------------------- prefilling changes to zero
            //public double PtsGradB; // XXX,X (Points Gradient b)
            at.Col_67 = 0;
            //public double PtsGradC; // XXX,X (Points Gradient c)
            at.Col_69 = 0;
            //public double PtsHLc; //X (Points Highline c)

            //cols 71-80
            //public double PtsHLd; //X (Points Highline d)
            at.Col_72 = 0;
            //public double PtsLLc; //XXX (Points Lowline c)
            //public double PtsLLd; //XXX (Points Lowline d)
            at.Col_75 = 0;
            //public double PtsVola; //XXX,X (Points Volume a)
            //public double PtsVolb; //XXX,X (Points Volume b)
            at.Col_78 = 0;
            at.Col_79 = 0;
            at.Col_80 = 0;

            //cols 81-84
            at.Col_81 = 0;
            at.Col_82 = 0;
            //public double SumRow1Only;
            //public double SameAs83DiffTreatment;

            return at;
        }
    }



}
