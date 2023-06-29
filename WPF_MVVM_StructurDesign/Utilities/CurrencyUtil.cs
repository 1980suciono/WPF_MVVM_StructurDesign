using System;
using System.Linq;
using System.Collections.Generic;

using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

namespace WPF_MVVM_StructurDesign.Utilities
{
    public class CurrencyUtil
    {   
    	string[] indexVal;
        public CurrencyUtil()
        {
            CultureInfo info = Thread.CurrentThread.CurrentCulture;
            DecimalSparator = Convert.ToChar(info.NumberFormat.CurrencyDecimalSeparator);
            GroupSparator = Convert.ToChar(info.NumberFormat.CurrencyGroupSeparator);
            indexVal = new string[10];
            indexVal[0] = "nol ";
            indexVal[1] = "satu ";
            indexVal[2] = "dua ";
            indexVal[3] = "tiga ";
            indexVal[4] = "empat ";
            indexVal[5] = "lima ";
            indexVal[6] = "enam ";
            indexVal[7] = "tujuh ";
            indexVal[8] = "delapan ";
            indexVal[9] = "sembilan ";
        }

        private char groupSparator;
        public char GroupSparator
        {
            get { return groupSparator; }
            set { groupSparator = value; }
        }

        private char decimalSparator;
        public char DecimalSparator
        {
            get { return decimalSparator; }
            set { decimalSparator = value; }
        }

        private string SparatorUnit(int pos, int grupLength, bool comma = false)
        {
            string groupUnit = "";
            int i = grupLength - 1;
            i -= pos;

            if (i == 0) { if (comma) groupUnit = "koma "; }
            else if (i == 1) groupUnit = "ribu ";
            else if (i == 2) groupUnit = "juta ";
            else if (i == 3) groupUnit = "milyar ";
            else if (i == 4) groupUnit = "trilyun ";
            return groupUnit;
        }

        private string NumberUnit(int pos, int strLength)
        {
            int i = strLength - 1;
            string numUnit = "";
            i -= pos;
            if (i == 2) numUnit = "ratus ";
            else if (i == 1) numUnit = "puluh ";
            else if (i == 0) numUnit = "";

            return numUnit;
        }

        private string CommaUnit(string strArr)
        {
            string strVal = "";
            int index = 0;
            if (strArr.Length > 1)
            {
                foreach (char c in strArr)
                {
                    int.TryParse(c.ToString(), out index);
                    strVal += indexVal[index];
                }
            }
            else
            {
                strVal += indexVal[index];
                strVal += indexVal[index];
            }
            return strVal;
        }

        public string RectifiedFormat(string strVal="0", bool currencyFormat = true)
        {
            if (strVal!= null)
            {
                double dblVal = 0;
                string pattern = "\\s+|\\" + groupSparator + "+";
                strVal = new Regex(pattern).Replace(strVal, String.Empty);
                if (Double.TryParse(strVal, out dblVal))
                {
                    return currencyFormat == true ? dblVal.ToString("#,###.##") : dblVal.ToString();
                }
                string[] strArr = strVal.Split(DecimalSparator);
                strVal = (strArr.Length < 2) ? strArr[0] : (long.TryParse(strArr[1], out long i)) ? strArr[0] + decimalSparator + strArr[1] : strArr[0];
              
            }

            return strVal;
        }

        public string CountRupiah(double dblVal, bool comma = false)
        {
            string strTmp = "";
            string strVal = "";
            string[] strArr = { };
            string[] strArr1 = new string[2] { "", "" };
            int index = 0;

            dblVal = (Math.Truncate(100 * dblVal) / 100);
            string strDbl = dblVal.ToString("#,###.##");

            strArr1 = strDbl.Split(DecimalSparator);
            strArr = strArr1[0].Split(GroupSparator);

            for (int i = 0; i < strArr.Length; i++)
            {
                for (int j = 0; j < strArr[i].Length; j++)
                {
                    int.TryParse(strArr[i][j].ToString(), out index);
                    int grupLength = strArr[i].Length;

                    //****modifable****
                    if (grupLength > 1)
                    {
                        if (index > 0)
                        {
                            if (j == grupLength - 2 && index == 1)
                            {
                                strTmp = "belas ";
                            }
                            else if (!string.IsNullOrEmpty(strTmp))
                            {
                                //definisi sebelas dll
                                strVal += ((index == 1) ? "se" : indexVal[index]) + strTmp;
                                strVal += NumberUnit(j, strArr[i].Length);
                                strTmp = "";
                            }
                            else
                            {
                                //definisi seratus
                                strVal += (index == 1 && j < grupLength - 1) ? "se" : indexVal[index];
                                strVal += NumberUnit(j, strArr[i].Length);
                            }
                        }
                        else if (j == grupLength - 1)
                        {
                            //definisi sepuluh
                            if (!String.IsNullOrEmpty(strTmp)) strVal += "sepuluh ";
                            strTmp = "";
                        }
                    }
                    else
                    {
                        //definisi seribu
                        strVal += (index == 1 && strArr.Length == 2) ? "se" : indexVal[index];
                    }
                    //*******

                }
                strVal += SparatorUnit(i, strArr.Length, comma);
            }

            if (comma)
            {
                if (strArr1.Length > 1)
                    strVal += CommaUnit(strArr1[1]);
                else strVal += CommaUnit("");
            }
            return strVal.Trim() + " rupiah";
        }
    }
}