using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    public static class RegimeTranformer
    {

        public static Dictionary<int, string> regime = new Dictionary<int, string>()
        {
            { 0 , "File mask search" },
            { 1 ,  "Match File Search"},
            { 2, "All File Search" }
        };

        static public int TransformToInt(string regimeName)
        {
            foreach(var name in regime)
            {
                if(name.Value == regimeName)
                {
                    return name.Key;
                }
            }
            return -1;
        }
    }
}
