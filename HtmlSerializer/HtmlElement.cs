using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
        public List<string> Classes { get; set; } = new List<string>();
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();


        public HtmlElement(Dictionary<string, string> attributes, List<string> classes, string innerHtml)
        {
            Attributes = new Dictionary<string, string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }

        public HtmlElement()
        {
        }

        // פונקציה שמחזירה את כל האבות של האלמנט הנוכחי
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            HtmlElement current = this;
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                foreach (HtmlElement child in current.Children)
                {
                    queue.Enqueue(child);
                }
                yield return current;
            }
        }
        // פונקציה שמחזירה את כל הצאצאים של האלמנט הנוכחי
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement current = this.Parent;
            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }

        }

        public HashSet<HtmlElement> FindElements(Selector selector)
        {
            var res = new HashSet<HtmlElement>();
            Search(this, selector, res);
            return res;
        }
        private void Search(HtmlElement currentElement, Selector selector, HashSet<HtmlElement> res)
        {

            foreach (var element in currentElement.Descendants())
            {
                if (Match(element, selector))
                {
                    if (selector.Child == null)
                    {
                        res.Add(element);
                        continue;
                    }
                    Search(element, selector.Child, res);
                }

            }


        }


        private bool Match(HtmlElement el, Selector sel)
        {
            bool tagMatch = string.IsNullOrEmpty(sel.TagName) || el.Name == sel.TagName;
            bool idMatch = string.IsNullOrEmpty(sel.Id) || el.Id == sel.Id;
            bool classMatch = !sel.Classes.Any() || sel.Classes.All(c => el.Classes.Contains(c));
          //  if(tagMatch && idMatch && classMatch) Console.WriteLine("true");
            return tagMatch && idMatch && classMatch;
        }

        #region hashequals
        //public override bool Equals(object? obj)
        //{
        //    if (obj is HtmlElement element)
        //    {
        //        return Id == element.Id &&
        //               Name == element.Name &&
        //               EqualityComparer<Dictionary<string, string>>.Default.Equals(Attributes, element.Attributes) &&
        //               EqualityComparer<List<string>>.Default.Equals(Classes, element.Classes) &&
        //               InnerHtml == element.InnerHtml &&
        //               IsSelfClosing == element.IsSelfClosing &&
        //               AreParentsEqual(Parent, element.Parent) &&
        //               AreChildrenEqual(Children, element.Children);
        //    }
        //    return false;
        //}

        //private bool AreParentsEqual(HtmlElement? parent1, HtmlElement? parent2)
        //{
        //    if (parent1 == null && parent2 == null) return true;
        //    if (parent1 == null || parent2 == null) return false;
        //    return parent1.Id == parent2.Id && parent1.Name == parent2.Name;
        //}

        //private bool AreChildrenEqual(List<HtmlElement> children1, List<HtmlElement> children2)
        //{
        //    if (children1.Count != children2.Count) return false;

        //    for (int i = 0; i < children1.Count; i++)
        //    {
        //        if (!children1[i].Equals(children2[i]))
        //            return false;
        //    }
        //    return true;
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(Id, Name, Attributes, Classes, InnerHtml, IsSelfClosing, Parent, Children);
        //}
        #endregion

        public override string ToString()
        {
            string s = "\nName: " + Name + "\n Id: " + Id + "\n";
            foreach (string atr in Attributes.Keys)
            {
                s += "[" + atr + ": " + Attributes[atr] + "] ";
            }
            if (Classes.Count != 0)
            {
                s += "\nClass:";
                foreach (string class1 in Classes)
                {
                    s += " " + class1;
                }
            }
            s += "\nInnerHtml: " + InnerHtml;
            return s;
        }


    }
}
