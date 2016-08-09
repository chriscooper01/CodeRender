using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeRender
{
    public class RenderCode
    {

        private static string contenToRender;
        private static string codeSubString;
        private static string STARTTAG = "[CODE]";
        private static string ENDTAG = "[/CODE]";
        private static List<Tuple<string, string>> codeElementsKeywords;
        private static List<Tuple<string, string>> codeElementsTags;

        private static int startSection;
        private static int sectionLength;


        public static string  Process(string content)
        {
            contenToRender = content;
            setQueryCodeKeywords();
            setQueryCodeTags();

            codeSection();


            return contenToRender;
        }


        private static void setQueryCodeKeywords()
        {
            codeElementsKeywords = new List<Tuple<string, string>>();
            codeElementsKeywords.Add(new Tuple<string, string>("public", "methodType"));
            codeElementsKeywords.Add(new Tuple<string, string>("private", "methodType"));
            codeElementsKeywords.Add(new Tuple<string, string>("void", "methodType"));
            codeElementsKeywords.Add(new Tuple<string, string>("return", "methodType"));
            codeElementsKeywords.Add(new Tuple<string, string>("static", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("string", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("int", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("decimal", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("var", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("object", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("if", "ifStart"));
            codeElementsKeywords.Add(new Tuple<string, string>("else", "ifStart"));
            codeElementsKeywords.Add(new Tuple<string, string>("try", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("Catch", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("catch", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("Dynamic", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("dynamic", "propertyType"));
            codeElementsKeywords.Add(new Tuple<string, string>("Exception", "ExceptionColour"));
            codeElementsKeywords.Add(new Tuple<string, string>("using", "propertyType"));
        }

        private static void setQueryCodeTags()
        {
            codeElementsTags = new List<Tuple<string, string>>();
            codeElementsTags.Add(new Tuple<string, string>(STARTTAG, "<pre><code>"));
            codeElementsTags.Add(new Tuple<string, string>(ENDTAG, "</pre></code>"));

            codeElementsTags.Add(new Tuple<string, string>("[CODECOMMENT]", "<CODECOMMENT>//"));
            codeElementsTags.Add(new Tuple<string, string>("[/CODECOMMENT]", "</CODECOMMENT>"));

        }

        private static void codeSection()
        {
            getCodeSubstring();
            if (!String.IsNullOrEmpty(codeSubString))
            {
                contenToRender = contenToRender.Remove(startSection, sectionLength);

                convertTags();
                
                convertKeywords();

                reInsertElement(startSection);

                codeSection();
            }


        }


        private static void getCodeSubstring()
        {
            startSection = contenToRender.IndexOf(STARTTAG);
            if (startSection >= 0)
            {
                int endSection = contenToRender.IndexOf(ENDTAG, startSection);
                if (endSection >= 0)
                {
                    sectionLength = endSection - startSection;
                    sectionLength += ENDTAG.Length;
                    codeSubString = contenToRender.Substring(startSection, sectionLength);



                }

            }
        }


        private static void convertKeywords()
        {
            foreach (var element in codeElementsKeywords)
                codeSubString = Regex.Replace(codeSubString, String.Format("\\b{0}\\b", element.Item1.Trim()).Trim(), String.Format("<{0}>{1}</{0}>", element.Item2, element.Item1));
            //codeSubString = codeSubString.Replace(element.Item1, String.Format("<{0}>{1}</{0}>", element.Item2, element.Item1));
        }


        private static void convertTags()
        {
            foreach (var element in codeElementsTags)
                codeSubString = codeSubString.Replace(element.Item1, element.Item2);
            //codeSubString = Regex.Replace(codeSubString, String.Format("\b{0}\b", element.Item1), element.Item2);
            
        }

        private static void reInsertElement(int startSection)
        {
            contenToRender = contenToRender.Insert(startSection, codeSubString);
            codeSubString = String.Empty;
        }

        
    }
}
