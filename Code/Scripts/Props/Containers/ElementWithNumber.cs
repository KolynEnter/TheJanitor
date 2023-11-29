#nullable enable

using System;
using System.Collections.Generic;

namespace CS576.Janitor.Process
{
    public struct ElementWithNumber<W> where W : class?
    {
        public W? element;
        public int number;
    }
}
