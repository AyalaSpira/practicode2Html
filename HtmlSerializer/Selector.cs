using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class Selector
    {

        public string TagName { get; set; }

        public string Id { get; set; }

        public List<string> Classes { get; set; } = new List<string>();

        public Selector Parent { get; set; }

        public Selector Child { get; set; }

        #region mine
        //public static Selector FromQuery(string query)
        //{
        //    var parts = query.Split(' ');  
        //    Selector root = new Selector(); 
        //    Selector currentSelector = root;  

        //    foreach (var part in parts)
        //    {
        //        HtmlElement a = new HtmlElement();
        //        var segments = part.Split(new[] { '#', '.' }, StringSplitOptions.RemoveEmptyEntries);

        //        if (part.StartsWith("#"))
        //        {
        //            a.Id = segments[0];
        //        }
        //        else if (part.StartsWith("."))
        //        {
        //            a.Classes.Add(segments[0]);
        //        }
        //        else
        //        {
        //            a.Name = segments[0];  
        //        }

        //        currentSelector.Children.Add(a);

        //        if (parts.Length > 1)
        //        {
        //            Selector newSelector = new Selector();

        //            if (part.StartsWith("#"))
        //                newSelector.Id = a.Id;
        //            if (part.StartsWith("."))
        //                newSelector.Classes.AddRange(a.Classes);
        //            if (a.Name != null)
        //                newSelector.TagName = a.Name;

        //            currentSelector.Children.Add(new HtmlElement
        //            {
        //                Name = newSelector.TagName,  
        //                Id = newSelector.Id,  
        //                Classes = newSelector.Classes  
        //            });
        //            currentSelector = newSelector;
        //        }
        //    }

        //    return root; 
        //}

        #endregion

        public static Selector ConvertToSelector(string query)
        {
            var queryParts = query.Split(' ');
            Selector rootSelector = null;
            Selector currentSelector = null;
            var AllTags = HtmlHelper.Instance.AllTags;
            foreach (var part in queryParts)
            {
                var newSelector = new Selector();
                if (rootSelector == null)
                {
                    rootSelector = newSelector;
                    currentSelector = rootSelector;
                }
                else
                {
                    currentSelector.Child = newSelector;
                    newSelector.Parent = currentSelector;
                    currentSelector = newSelector;
                }
                var partsTag = part.Split(new[] { '#', '.' }, StringSplitOptions.None);
                if (partsTag.Length > 0 && !string.IsNullOrEmpty(partsTag[0]) && AllTags.Contains(partsTag[0]))
                {
                    currentSelector.TagName = partsTag[0];
                }
                for (var i = 1; i < partsTag.Length; i++)
                {
                    if (part.Contains('#' + partsTag[i]))
                        currentSelector.Id = partsTag[i];
                    else if (part.Contains('.'))
                        currentSelector.Classes.Add(partsTag[i]);
                }
            }
            return rootSelector;
        }




    }
}
