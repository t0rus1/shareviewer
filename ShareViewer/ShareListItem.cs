using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    internal class ShareListItem
    {
        internal ShareListItem(int shareNumber, string shareName)
        {
            number = shareNumber;
            name = shareName;
            code = ""; // later
            exchange = ""; // later
        }

        internal string Name { get => name; set => name = value; }
        private String name;

        internal int Number { get => number; set => number = value; }
        private int number;

        internal string Code { get => code; set => code = value; }
        private string code;

        internal string Exchange { get => exchange; set => exchange = value; }
        private string exchange;

        public override string ToString()
        {
            return $"{name.PadRight(30,' ')} {number}";
        }
    }
}
