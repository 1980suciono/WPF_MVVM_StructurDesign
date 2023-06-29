using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_StructurDesign.Utilities
{
    class StringUtil
    {
        public StringUtil() 
        { 
        }

        public string SpaceRight(string strVal, int maxOutputLength, int space)
        {
            return ((strVal.Length > maxOutputLength) ? strVal.Substring(0, maxOutputLength - 1) : strVal) + new String(' ', (space + maxOutputLength) - (((strVal.Length > maxOutputLength) ? strVal.Substring(0, maxOutputLength - 1) : strVal).Length));
        }
        public string SpaceLeft(string strVal, int maxOutputLength, int space)
        {
            return new String(' ', (space + maxOutputLength) - (((strVal.Length > maxOutputLength) ? strVal.Substring(0, maxOutputLength - 1) : strVal).Length)) + ((strVal.Length > maxOutputLength) ? strVal.Substring(0, maxOutputLength - 1) : strVal);

        }
    }
}
