using ProjectsResolver.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ProjectsResolver.Lib.Data
{
    public class PropertiesReader
    {
        public PropertiesReader() { }

        public string ReadPhysicalPath(XmlDocument document)
        {
            XmlElement root = document.DocumentElement;

            return root.GetAttribute("physicalPath");
        }

        public List<Site> ReadSites(XmlDocument document)
        {
            var siteList = new List<Site>();
            XmlElement root = document.DocumentElement;
            if (root != null)
            {
                foreach (XmlElement element in root)
                {
                    bool state = element.GetAttribute("state").GetState(); 
                    var name = element.GetAttribute("SITE.NAME");

                    siteList.Add(new Site() { Name = name, IsRunnig = state });
                }
            }

            return siteList;
        }

        public string ReadVersion(XmlDocument document)
        {
            var projectElement = document.DocumentElement;

            foreach (XmlElement group in projectElement)
            {
                if (group.Name.Contains("PropertyGroup"))
                {
                    foreach (XmlElement property in group)
                    {
                        if (property.Name == "Version")
                            return property.InnerText;
                    }
                }
            }

            return null;
        }

        public string ReadPublishUrl(XmlDocument document)
        {
            var projectElement = document.DocumentElement;

            foreach (XmlElement group in projectElement)
            {
                if (group.Name.Contains("PropertyGroup"))
                {
                    foreach (XmlElement property in group)
                    {
                        if (property.Name == "PublishUrl")
                            return property.InnerText;
                    }
                }
            }

            return null;
        }
    }
}
