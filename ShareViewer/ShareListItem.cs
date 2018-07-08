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
        }

        private int number;
        private String name;
        internal int Number { get => number; set => number = value; }
        internal string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return $"{name.PadRight(30,' ')} {number}";
        }
    }
}
