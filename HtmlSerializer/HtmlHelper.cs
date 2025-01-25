using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace HtmlSerializer
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public string[] AllTags { get; private set; }
        public string[] SelfClosingTags { get; private set; }


        private HtmlHelper()
        {
            // טוענים את הנתונים מקבצי JSON

            AllTags = LoadTagsFromJson("D:\\Users\\User\\Desktop\\practicode2\\HtmlSerializer\\HtmlSerializer\\HtmlTags.json");
            SelfClosingTags = LoadTagsFromJson("D:\\Users\\User\\Desktop\\practicode2\\HtmlSerializer\\HtmlSerializer\\HtmlVoidTags.json");
        }
        private string[] LoadTagsFromJson(string filePath)
        {
            try
            {
                var jsonContent = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<string[]>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                return Array.Empty<string>();
            }
        }





    }
}
