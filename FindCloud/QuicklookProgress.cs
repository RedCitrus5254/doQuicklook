using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindCloud
{
    class QuicklookProgress
    {
        public QuicklookProgress(string name, int num)
        {
            NameOfImage = name;
            NumOfImages = num;
        }
        public QuicklookProgress()
        {

        }
        public string NameOfImage { get; set; }
        public int NumOfImages { get; set; }
        
    }
}
