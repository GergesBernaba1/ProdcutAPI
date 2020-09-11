using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSportAPI.Classes
{
    public class QueryParameters
    {
        const int _MaxSize = 100;
        private int _Size = 50;
        public int Page { get; set; }
        public int Size
        {
            get { return _Size; }
            set { _Size = Math.Min(_MaxSize, value); }
        }
    }
}
