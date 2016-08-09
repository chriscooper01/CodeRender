using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRender
{
    public class Render
    {
        private static string renderedBody;

        public static string IntoHTML(string body)
        {
            renderedBody = body;
            renderedBody = RenderCode.Process(renderedBody);
            setBreak();
            setSectionHeaders();
            setQuotes();
        

            return renderedBody;
        }


        private static void setQuotes()
        {            
            renderedBody = renderedBody.Replace("[QUOTE]", "<blockquote>");
            renderedBody = renderedBody.Replace("[/QUOTE]", "</blockquote>");
        }

        private static void setSectionHeaders()
        {
            renderedBody = renderedBody.Replace("[SECTIONHEADER]", "<h2 class=\"section-heading\">");
            renderedBody = renderedBody.Replace("[/SECTIONHEADER]", "</h2>");

            renderedBody = renderedBody.Replace("[HEADINGSUB]", "<subheading>");
            renderedBody = renderedBody.Replace("[/HEADINGSUB]", "</subheading>");


    
            renderedBody = renderedBody.Replace("[HEADINGSUBCOMMENT]", "<subheadingcomment>");
            renderedBody = renderedBody.Replace("[/HEADINGSUBCOMMENT]", "</subheadingcomment>");


            renderedBody = renderedBody.Replace("[HEADING]", "<heading>");
            renderedBody = renderedBody.Replace("[/HEADING]", "</heading>");


        }



        private static void setBreak()
        {
            renderedBody = renderedBody.Replace("\r\n", "<br/>");
        }
       
    }
}
