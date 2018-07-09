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
    internal struct AllTable
    {
        //cols 1-10
        internal int    Row; // XXXXXX   (runs from row 0 (headings), row 1(special) ... to row 10401) 
        internal double col_2;
        internal string Date; //XXXXXX
        internal double col_4;
        internal string Day; //XXX
        internal double col_6;
        internal string TimeFrom; //XX:XX:XX (from 09:00:00 to 17:35:00 ie 104 5 min ranges per day for 100 days)
        internal string TimeTo; //XX:XX:XX (from 09:04:49 to 17:24:59...ie 104 5 min ranges per day for 100 days)
        internal int    F; //X (Five-minutes section no.) its value will be 1 less than Row, so it runs from -1 to 10400
        internal double col_10;
        //cols 11-20
        internal double FP; // XXX,XXX (Five-minutes last price)
        internal double SPa; //XXX,XXXX (Slow price a)
        internal double PGa; //X,XXXXX (Price Gradient a)
        internal double APpg; //X,XXXXXX (Self adjusting parameter)
        internal double PGFrowx15; //X,XXXXX
        internal double PGFrowx16; //X,XXXXX
        internal double PGFrowx17; //X,XXXXX
        internal double BigPGF; //X,XXXXX  ????? not in section 5 of Gunther's notes
        internal double SPb; //XXX,XXXX (Slow price b)
        internal double PGb; //X,XXXXX (Price Gradient b)
        //cols 21-30
        internal double col_21;
        internal double SPc; //XXX,XXXX (Slow price c)
        internal double PGc; //X,XXXXX (Price Gradient c)
        internal double col_24;
        internal double SPd; //XXX,XXXX (Slow price d)
        internal double PGd; //X,XXXXX (Price Gradient d)
        internal double col_27;
        internal double col_28;
        internal double HLc; //XXX,XXX (Highline c)
        internal double DHLFPc; //X,XXX (Distance HL to FP)
        //cols 31-40
        internal double col_31;
        internal double col_32;
        internal double HLd; //XXX,XXX (Highline d)
        internal double DHLFPd; //X,XXX (Distance HL to FP)
        internal double col_35;
        internal double col_36;
        internal double col_37;
        internal double col_38;
        internal double LLc; //XXX,XXX (Lowline c)
        internal double DLLFPc;// X,XXX (Distance LL to FP)
        //cols 41-50
        internal double col_41;
        internal double col_42;
        internal double LLd; // XXX,XXX (Lowline d)
        internal double DLLFPd; //X,XXX (Distance LL to FP)
        internal double col_45;
        internal double col_46;
        internal double col_47;
        internal double col_48;
        internal double FV; // XXXXXXX (Sum of the Volume of the Five minutes section: Five minutes volume)
        internal double col_50;
        //cols 51-60
        internal double SVa; //XXXXXXX (Slow Volume a)
        internal double APSVac; //XXX,X (Self adjusting parameter c)
        internal double SVFac; //X,XXXXXX (Slow Volume Figure)
        internal double col_54; // 54 Do ignore: 
        internal double SVb; //XXXXXXX (Slow Volume b)
        internal double APSVbd; //XXX,X (Self adjusting parameter)
        internal double SVFbd; //X,XXXXXX (Slow Volume Figure)
        internal double col_58;
        internal double SVc; //XXXXXXX (Slow volume c)
        internal double RPGFV; //XXX,X (Related PGF volume Figure)
        //cols 61-70
        internal double col_61;
        internal double SVd; //XXXXXXX (Slow volume d)
        internal double col_63;
        internal double PtsGradA; // XXX,X (Points Gradient a)
        internal double col_65;
        internal double PtsGradB; // XXX,X (Points Gradient b)
        internal double col_67;
        internal double PtsGradC; // XXX,X (Points Gradient c)
        internal double col_69;
        internal double PtsHLc; //X (Points Highline c)
        //cols 71-80
        internal double PtsHLd; //X (Points Highline d)
        internal double col_72;
        internal double PtsLLc; //XXX (Points Lowline c)
        internal double PtsLLd; //XXX (Points Lowline d)
        internal double col_75;
        internal double PtsVola; //XXX,X (Points Volume a)
        internal double PtsVolb; //XXX,X (Points Volume b)
        internal double col_78;
        internal double col_79;
        internal double col_80;
        //cols 81-84
        internal double col_81;
        internal double col_82;
        internal double SumRow1Only;
        internal double SameAs83DiffTreatment;

    }

    internal static class AllTableFactory
    {
        internal AllTable InitialTable(int row, string date, string day, string timefrom, string timeto, int f)
        {
            AllTable at;

            //cols 1-10
            at.Row=row; // XXXXXX   (runs from row 0 (headings), row 1(special) ... to row 10401) 
            at.col_2 = 1;
            at.Date = date; //XXXXXX
            at.col_4 = 1;
            at.Day = day; //XXX
            at.col_6 = 1;
            at.TimeFrom = timefrom; //XX:XX:XX (from 09:00:00 to 17:35:00 ie 104 5 min ranges per day for 100 days)
            at.TimeTo = timeto;  //XX:XX:XX (from 09:04:49 to 17:24:59...ie 104 5 min ranges per day for 100 days)
            at.F = f;   //X (Five-minutes section no.) its value will be 1 less than Row, so it runs from -1 to 10400
            at.col_10 = 1;

            //cols 11-20
            //internal double FP; // XXX,XXX (Five-minutes last price)
            //internal double SPa; //XXX,XXXX (Slow price a)
            //internal double PGa; //X,XXXXX (Price Gradient a)
            //internal double APpg; //X,XXXXXX (Self adjusting parameter)
            //internal double PGFrowx15; //X,XXXXX
            //internal double PGFrowx16; //X,XXXXX
            //internal double PGFrowx17; //X,XXXXX
            //internal double BigPGF; //X,XXXXX  ????? not in section 5 of Gunther's notes
            //internal double SPb; //XXX,XXXX (Slow price b)
            //internal double PGb; //X,XXXXX (Price Gradient b)

            //cols 21-30
            at.col_21 = 1;
            //internal double SPc; //XXX,XXXX (Slow price c)
            //internal double PGc; //X,XXXXX (Price Gradient c)
            at.col_24 = 1;
            //internal double SPd; //XXX,XXXX (Slow price d)
            //internal double PGd; //X,XXXXX (Price Gradient d)
            at.col_27 = 1;
            at.col_28 = 1;
            //internal double HLc; //XXX,XXX (Highline c)
            //internal double DHLFPc; //X,XXX (Distance HL to FP)


            //cols 31-40
            at.col_31 = 1;
            at.col_32 = 1;
            //internal double HLd; //XXX,XXX (Highline d)
            //internal double DHLFPd; //X,XXX (Distance HL to FP)
            at.col_35 = 1;
            at.col_36 = 1;
            at.col_37 = 1;
            at.col_38 = 1;
            //internal double LLc; //XXX,XXX (Lowline c)
            //internal double DLLFPc;// X,XXX (Distance LL to FP)

            //cols 41-50
            at.col_41 = 1;
            at.col_42 = 1;
            //internal double LLd; // XXX,XXX (Lowline d)
            //internal double DLLFPd; //X,XXX (Distance LL to FP)
            at.col_45 = 1;
            at.col_46 = 1;
            at.col_47 = 1;
            at.col_48 = 1;
            //internal double FV; // XXXXXXX (Sum of the Volume of the Five minutes section: Five minutes volume)
            at.col_50 = 1;

            //cols 51-60
            //internal double SVa; //XXXXXXX (Slow Volume a)
            //internal double APSVac; //XXX,X (Self adjusting parameter c)
            //internal double SVFac; //X,XXXXXX (Slow Volume Figure)
            at.col_54 = 1; // 54 Do ignore: 
            //internal double SVb; //XXXXXXX (Slow Volume b)
            //internal double APSVbd; //XXX,X (Self adjusting parameter)
            //internal double SVFbd; //X,XXXXXX (Slow Volume Figure)
            at.col_58 = 1;
            //internal double SVc; //XXXXXXX (Slow volume c)
            //internal double RPGFV; //XXX,X (Related PGF volume Figure)

            //cols 61-70
            at.col_61 = 1;
            //internal double SVd; //XXXXXXX (Slow volume d)
            at.col_63 = 1;
            //internal double PtsGradA; // XXX,X (Points Gradient a)
            at.col_65 = 0; // <--------------------------------------------- prefilling changes to zero
            //internal double PtsGradB; // XXX,X (Points Gradient b)
            at.col_67 = 0;
            //internal double PtsGradC; // XXX,X (Points Gradient c)
            at.col_69 = 0;
            //internal double PtsHLc; //X (Points Highline c)
            
            //cols 71-80
            //internal double PtsHLd; //X (Points Highline d)
            at.col_72 = 0;
            //internal double PtsLLc; //XXX (Points Lowline c)
            //internal double PtsLLd; //XXX (Points Lowline d)
            at.col_75 = 0;
            //internal double PtsVola; //XXX,X (Points Volume a)
            //internal double PtsVolb; //XXX,X (Points Volume b)
            at.col_78 = 0;
            at.col_79 = 0;
            at.col_80 = 0;

            //cols 81-84
            at.col_81 = 0;
            at.col_82 = 0;
            //internal double SumRow1Only;
            //internal double SameAs83DiffTreatment;

            return at;
        }
    }
        


}
