using Microsoft.AspNetCore.Html;


namespace PracticeTest.Models
{
    public static class CustomHelperClass
    {  
       public static HtmlString CreateCustomSelectTag(int PersonId, int SpouseId, string SpouseName)
        {
            if(SpouseId == 0)
            {
                var singleTag = $"<select data-id = {PersonId} id = \"mySelect\"><option default>Single</option><option>Married</option></select>";
                return new HtmlString(singleTag); 
            } else
            {
                var marriedTag = $"<select  data-id = {PersonId} id = \"mySelect\" disabled><option default>Married({SpouseName})</option><option>Single</option></select>";
                return new HtmlString(marriedTag); 
            }
        }
    }
}
