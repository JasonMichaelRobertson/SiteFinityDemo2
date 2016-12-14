using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Utilities
{
    
    public class ServiceLines
    {
        public List<ServiceLine> Items { get; set; }

        public ServiceLines()
        {
            Items = new List<ServiceLine>();

            Items.Add(new ServiceLine("0", "Restructuring"));
            //Items.Add(new ServiceLine("1", "Turnaround and Restructuring"));
            Items.Add(new ServiceLine("2", "Real Estate"));
            Items.Add(new ServiceLine("3", "Forensic"));
            Items.Add(new ServiceLine("4", "Investment Management"));
        }

        /// <summary>
        /// Return name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>name</returns>
        public string FindItemById(string id, string endtag = "")
        {
            if (!String.IsNullOrEmpty(id) && id != "-1")
            {
                return Items.Where(item => item.Id == id).First().Name + endtag;
            }
            else
            {
                return "";
            }
        }       

        public class ServiceLine
        {
            public ServiceLine()
            {

            }

            public ServiceLine(string id, string name)
            {
                Id = id;
                Name = name;
            }

            public string Id { get; set; }
            public string Name { get; set; }


        }

    }
}