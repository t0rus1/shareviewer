using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
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
        public int Row { get => row; set => row = value; }              //0
        public double Col_2 { get => col_2; set => col_2 = value; }     //
        public string Date { get => date; set => date = value; }        //2
        public double Col_4 { get => col_4; set => col_4 = value; }     //
        public string Day { get => day; set => day = value; }           //4
        public double Col_6 { get => col_6; set => col_6 = value; }     //
        public string TimeFrom { get => timeFrom; set => timeFrom = value; }  //6
        public string TimeTo { get => timeTo; set => timeTo = value; }        //7 
        public int F { get => f; set => f = value; }                          //8
        public double Col_10 { get => col_10; set => col_10 = value; }
        //11-20
        public double FP { get => fP; set => fP = value; }
        public double SPa { get => sPa; set => sPa = value; }
        public double PGa { get => pGa; set => pGa = value; }
        public double APpg { get => aPpg; set => aPpg = value; }
        public double PGFrowx15 { get => pGFrowx15; set => pGFrowx15 = value; }
        public double PGFrowx16 { get => pGFrowx16; set => pGFrowx16 = value; }
        public double PGFrowx17 { get => pGFrowx17; set => pGFrowx17 = value; }
        public double BigPGF { get => bigPGF; set => bigPGF = value; }
        public double SPb { get => sPb; set => sPb = value; }
        public double PGb { get => pGb; set => pGb = value; }
        //21-30
        public double Col_21 { get => col_21; set => col_21 = value; }
        public double SPc { get => sPc; set => sPc = value; }
        public double PGc { get => pGc; set => pGc = value; }
        public double Col_24 { get => col_24; set => col_24 = value; }
        public double SPd { get => sPd; set => sPd = value; }
        public double PGd { get => pGd; set => pGd = value; }
        public double Col_27 { get => col_27; set => col_27 = value; }
        public double Col_28 { get => col_28; set => col_28 = value; }
        public double HLc { get => hLc; set => hLc = value; }
        public double DHLFPc { get => dHLFPc; set => dHLFPc = value; }
        //31-40
        public double Col_31 { get => col_31; set => col_31 = value; }
        public double Col_32 { get => col_32; set => col_32 = value; }
        public double HLd { get => hLd; set => hLd = value; }
        public double DHLFPd { get => dHLFPd; set => dHLFPd = value; }
        public double Col_35 { get => col_35; set => col_35 = value; }
        public double Col_36 { get => col_36; set => col_36 = value; }
        public double Col_37 { get => col_37; set => col_37 = value; }
        public double Col_38 { get => col_38; set => col_38 = value; }
        public double LLc { get => lLc; set => lLc = value; }
        public double DLLFPc { get => dLLFPc; set => dLLFPc = value; }
        //41-50
        public double Col_41 { get => col_41; set => col_41 = value; }
        public double Col_42 { get => col_42; set => col_42 = value; }
        public double LLd { get => lLd; set => lLd = value; }
        public double DLLFPd { get => dLLFPd; set => dLLFPd = value; }
        public double Col_45 { get => col_45; set => col_45 = value; }
        public double Col_46 { get => col_46; set => col_46 = value; }
        public double Col_47 { get => col_47; set => col_47 = value; }
        public double Col_48 { get => col_48; set => col_48 = value; }
        public UInt32 FV { get => fV; set => fV = value; }
        public double Col_50 { get => col_50; set => col_50 = value; }
        //51-60
        public UInt32 SVa { get => sVa; set => sVa = value; }
        public double APSVac { get => aPSVac; set => aPSVac = value; }
        public double SVFac { get => sVFac; set => sVFac = value; }
        public double Col_54 { get => col_54; set => col_54 = value; }
        public UInt32 SVb { get => sVb; set => sVb = value; }
        public double APSVbd { get => aPSVbd; set => aPSVbd = value; }
        public double SVFbd { get => sVFbd; set => sVFbd = value; }
        public double Col_58 { get => col_58; set => col_58 = value; }
        public UInt32 SVc { get => sVc; set => sVc = value; }
        public double RPGFV { get => rPGFV; set => rPGFV = value; }
        //61-70
        public double Col_61 { get => col_61; set => col_61 = value; }
        public UInt32 SVd { get => sVd; set => sVd = value; }
        public double Col_63 { get => col_63; set => col_63 = value; }
        public double PtsGradA { get => ptsGradA; set => ptsGradA = value; }
        public double Col_65 { get => col_65; set => col_65 = value; }
        public double PtsGradB { get => ptsGradB; set => ptsGradB = value; }
        public double Col_67 { get => col_67; set => col_67 = value; }
        public double PtsGradC { get => ptsGradC; set => ptsGradC = value; }
        public double Col_69 { get => col_69; set => col_69 = value; }
        public double PtsHLc { get => ptsHLc; set => ptsHLc = value; }
        //71-80
        public double PtsHLd { get => ptsHLd; set => ptsHLd = value; }
        public double Col_72 { get => col_72; set => col_72 = value; }
        public double PtsLLc { get => ptsLLc; set => ptsLLc = value; }
        public double PtsLLd { get => ptsLLd; set => ptsLLd = value; }
        public double Col_75 { get => col_75; set => col_75 = value; }
        public double PtsVola { get => ptsVola; set => ptsVola = value; }
        public double PtsVolb { get => ptsVolb; set => ptsVolb = value; }
        public double Col_78 { get => col_78; set => col_78 = value; }
        public double Col_79 { get => col_79; set => col_79 = value; }
        public double Col_80 { get => col_80; set => col_80 = value; }
        //81-84
        public double Col_81 { get => col_81; set => col_81 = value; }
        public double Col_82 { get => col_82; set => col_82 = value; }
        public double SumRow1Only { get => sumRow1Only; set => sumRow1Only = value; }
        public double SameAs83DiffTreatment { get => sameAs83DiffTreatment; set => sameAs83DiffTreatment = value; }


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
