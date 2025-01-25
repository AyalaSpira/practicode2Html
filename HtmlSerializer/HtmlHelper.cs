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


        //private HtmlHelper()
        //{
        //    string open = File.ReadAllText("D:\\Users\\User\\Desktop\\practicode2\\HtmlSerializer\\HtmlSerializer\\HtmlTags.json");
        //    AllTags = JsonSerializer.Deserialize<string[]>(open);

        //    string close = File.ReadAllText("D:\\Users\\User\\Desktop\\practicode2\\HtmlSerializer\\HtmlSerializer\\HtmlVoidTags.json");
        //    SelfClosingTags = JsonSerializer.Deserialize<string[]>(close);
        //}
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

        //public HtmlElement BuildHtmlTree(List<string> htmlStrings, List<string> allTags, List<string> selfClosingTags)
        //{
        //    var root = new HtmlElement { Name = "html" };
        //    var currentElement = root;
        //    HtmlElement parentElement = null;

        //    foreach (var element in htmlStrings)
        //    {
        //        var parts = element.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        //        var tagName = parts[0].Trim();

        //        if (tagName == "html/")
        //        {
        //            break;
        //        }
        //        if (tagName.Contains("/"))
        //        {
        //            currentElement = currentElement.Parent;
        //        }

        //        else
        //        {
        //            HtmlElement a = new HtmlElement { Name = tagName };

        //            a.Parent = currentElement;

        //            foreach (var attribute in parts.Skip(1))
        //            {
        //                if (attribute.StartsWith("Id"))
        //                {
        //                    a.Id = attribute.Split('=')[1].Trim('"');
        //                }
        //                else if (attribute.StartsWith("Class"))
        //                {
        //                    a.Classes = attribute.Split('=')[1].Trim('"').Split(' ').ToList();
        //                }

        //            }

        //            if (tagName.EndsWith("/"))
        //            {
        //                a.IsSelfClosing = true;
        //                currentElement.Children.Add(a);
        //            }
        //            else
        //            {
        //                currentElement.Children.Add(a);
        //                currentElement = a;
        //            }
        //        }
        //    }
        //    return root;

        //}




    }
}
