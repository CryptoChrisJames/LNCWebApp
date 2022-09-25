using System;
using System.Collections.Generic;
using System.Text;

namespace LNCLibrary2.Models
{
    public enum Sizes
    {
        XS,
        S,
        M,
        L,
        XL,
        XXL,
        XXXL,
        XXXXL
    }
    public class Size
    {
        public int ID { get; set; }
        public Sizes ThisSize { get; set; }
    }
}
