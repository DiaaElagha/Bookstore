using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Extensions
{
    public static class ConvertHtmlToString

    {
        public static string ConvertHtmlToText(string sHTML)
        {
            //--------< HTML_to_Text() >--------
            //< remove blocks >
            sHTML = remove_script(sHTML);

            sHTML = remove_Head(sHTML);

            //</ remove blocks >

            //< remove Html-Tags >

            sHTML = remove_Tags(sHTML);

            //</ remove Html-Tags >

            //< remove Charaters >
            sHTML = remove_Control_Characters(sHTML);

            sHTML = remove_HTML_Characters(sHTML);

            sHTML = remove_Special_Characters(sHTML);

            sHTML = remove_Punctuation_Mark_Characters(sHTML);

            sHTML = remove_Brackets_Characters(sHTML);

            //</ remove Charaters >

            //< output >

            return sHTML;
            //</ output >
            //--------</ HTML_to_Text() >--------
        }



        public static string remove_HTML_Characters(string sHTML)
        {

            //--------< HTML_to_Text() >--------

            sHTML = sHTML.Replace("&gt;", " ");

            sHTML = sHTML.Replace("&lt;", " ");

            sHTML = sHTML.Replace("&nbsp;", " ");

            sHTML = sHTML.Replace("&gt;", " ");

            while (sHTML.IndexOf("  ") >= 0)

            { sHTML = sHTML.Replace("  ", " "); }

            //< output >

            return sHTML;

            //</ output >

            //--------</ HTML_to_Text() >--------
        }



        public static string remove_Special_Characters(string sHTML)
        {
            //--------< HTML_to_Text() >--------
            sHTML = sHTML.Replace("\\", " ");

            sHTML = sHTML.Replace("/", " ");

            while (sHTML.IndexOf("  ") >= 0)

            { sHTML = sHTML.Replace("  ", " "); }

            //< output >

            return sHTML;

            //</ output >

            //--------</ HTML_to_Text() >--------
        }



        public static string remove_Punctuation_Mark_Characters(string sHTML)
        {
            //--------< remove_Punctuation_Mark_Characters() >--------

            sHTML = sHTML.Replace(";", " ");
            sHTML = sHTML.Replace(".", " ");
            sHTML = sHTML.Replace(",", " ");
            sHTML = sHTML.Replace("'", " ");
            sHTML = sHTML.Replace(":", " ");
            sHTML = sHTML.Replace("*", " ");
            sHTML = sHTML.Replace("+", " ");
            sHTML = sHTML.Replace("=", " ");
            sHTML = sHTML.Replace("\"", " ");
            sHTML = sHTML.Replace("-", " ");
            sHTML = sHTML.Replace("_", " ");
            sHTML = sHTML.Replace("!", " ");
            sHTML = sHTML.Replace("?", " ");
            sHTML = sHTML.Replace("~", " ");
            sHTML = sHTML.Replace("#", " ");
            sHTML = sHTML.Replace("$", " ");
            sHTML = sHTML.Replace("%", " ");
            sHTML = sHTML.Replace("`", " ");
            sHTML = sHTML.Replace("´", " ");
            sHTML = sHTML.Replace("°", " ");
            sHTML = sHTML.Replace("^", " ");
            sHTML = sHTML.Replace("&", " ");

            while (sHTML.IndexOf("  ") >= 0)

            { sHTML = sHTML.Replace("  ", " "); }

            //< output >
            return sHTML;
            //</ output >
            //--------</ remove_Punctuation_Mark_Characters() >--------

        }



        public static string remove_Brackets_Characters(string sHTML)
        {
            //--------< remove_Brackets_Characters() >--------

            sHTML = sHTML.Replace("(", " ");
            sHTML = sHTML.Replace(")", " ");
            sHTML = sHTML.Replace("[", " ");
            sHTML = sHTML.Replace("]", " ");
            sHTML = sHTML.Replace("{", " ");
            sHTML = sHTML.Replace("}", " ");
            sHTML = sHTML.Replace("<", " ");
            sHTML = sHTML.Replace(">", " ");
            while (sHTML.IndexOf("  ") >= 0)
            { sHTML = sHTML.Replace("  ", " "); }

            //< output >
            return sHTML;
            //</ output >
            //--------</ remove_Brackets_Characters() >--------

        }

        public static string remove_Control_Characters(string sHTML)
        {
            //--------< HTML_to_Text() >--------

            sHTML = sHTML.Replace("\n", " ");
            sHTML = sHTML.Replace("\r", " ");
            sHTML = sHTML.Replace("\t", " ");

            while (sHTML.IndexOf("  ") >= 0)
            { sHTML = sHTML.Replace("  ", " "); }

            //< output >
            return sHTML;
            //</ output >
            //--------</ HTML_to_Text() >--------

        }

        public static string remove_Tags(string sHTML)
        {
            //--------< remove_Tags() >--------

            //----< @Loop: Search tags >----

            int intStart = -1;

            while (1 == 1)
            {
                //---< Search Tag >---
                //< check end >

                if (sHTML.Length <= intStart) break;

                //< check end >

                //< find open >

                int posOpenTag = sHTML.IndexOf("<", intStart + 1);
                if (posOpenTag < 0) break;

                //</ find open >


                //< find close >

                int posCloseTag = sHTML.IndexOf(">", posOpenTag);

                if (posCloseTag < 0) break; //no end tag

                //</ find close >


                //< cut Tag >

                string sLeft = sHTML.Substring(0, posOpenTag);

                string sRight = sHTML.Substring(posCloseTag + 1);

                sHTML = sLeft + " " + sRight;

                //</ cut Tag >

                intStart = sLeft.Length;

                //---</ Search Tag >---
            }

            //----</ @Loop: Search tags >----

            //< output >

            return sHTML;

            //</ output >

            //--------</ remove_Tags() >--------

        }

        public static string remove_script(string sHTML)
        {
            //--------< remove_script() >--------

            //----< @Loop: Search tags >----

            int intStart = 0;

            while (1 == 1)
            {
                //---< Search Tag >---

                //< check end >

                if (sHTML.Length <= intStart) break;

                //< check end >

                //< find open >

                int posscript_Open = sHTML.IndexOf("<script", intStart + 1, comparisonType: System.StringComparison.InvariantCultureIgnoreCase);

                if (posscript_Open < 0) break; //no open tag

                //</ find open >

                //< find close >

                int posscript_Close = sHTML.IndexOf("</script", posscript_Open + 1, comparisonType: System.StringComparison.InvariantCultureIgnoreCase);

                if (posscript_Close < 0) break; //no end tag

                //</ find close >


                //< find close >

                int posCloseTag = sHTML.IndexOf(">", posscript_Close);

                if (posCloseTag < 0) break; //no end tag

                //</ find close >


                //< cut Tag >

                string sLeft = sHTML.Substring(0, posscript_Open);

                string sRight = sHTML.Substring(posCloseTag + 1);

                sHTML = sLeft + sRight;

                //</ cut Tag >

                intStart = sLeft.Length;

                //---</ Search Tag >---

            }

            //----</ @Loop: Search tags >----

            //< output >

            return sHTML;

            //</ output >

            //--------</ remove_script() >--------

        }

        public static string remove_Head(string sHTML)
        {
            //--------< remove_Head() >--------

            //----< @Loop: Search tags >----

            int intStart = 0;

            while (1 == 1)
            {
                //---< Search Tag >---

                //< check end >

                if (sHTML.Length <= intStart) break;

                //< check end >

                //< find open >

                int posHead_Open = sHTML.IndexOf("<head", intStart + 1, comparisonType: System.StringComparison.InvariantCultureIgnoreCase);

                if (posHead_Open < 0) break; //no open tag

                //</ find open >

                //< find close >

                int posHead_Close = sHTML.IndexOf("</head", posHead_Open + 1, comparisonType: System.StringComparison.InvariantCultureIgnoreCase);

                if (posHead_Close < 0) break; //no end tag

                //</ find close >

                //< find close >

                int posCloseTag = sHTML.IndexOf(">", posHead_Close);

                if (posCloseTag < 0) break; //no end tag

                //</ find close >

                //< cut Tag >

                string sLeft = sHTML.Substring(0, posHead_Open);

                string sRight = sHTML.Substring(posCloseTag + 1);

                sHTML = sLeft + sRight;

                //</ cut Tag >

                intStart = sLeft.Length;

                //---</ Search Tag >---
            }

            //----</ @Loop: Search tags >----

            //< output >

            return sHTML;

            //</ output >

            //--------</ remove_Head() >--------

        }





    }
}
