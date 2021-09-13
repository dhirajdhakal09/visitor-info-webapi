using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Core
{
    public class DataTable<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
    }
}
