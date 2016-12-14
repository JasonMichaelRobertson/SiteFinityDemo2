using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SitefinityWebApp.Mvc.Models
{
    public class IMFormModel
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        [Category("General")]
        public string HeaderText
        {
            get;
            set;
        }

       // private readonly List<SelectListItem> FundInterestOptions;

        private readonly List<SelectListItem> PreferredLanguageOptions;

        public IMFormModel()
        {
            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = "Potential Investor", Value = "1" });
            //items.Add(new SelectListItem { Text = "Potential Referrer", Value = "2" });
            //items.Add(new SelectListItem { Text = "Media / Journalist", Value = "3" });

            //FundInterestOptions = items;


            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "English", Value = "1" });
            items.Add(new SelectListItem { Text = "Chinese", Value = "2" });         

            PreferredLanguageOptions = items;
        }

        [Required]
        [DisplayName("First Name")]       
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Phone]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Referrer")]
        public string Referrer { get; set; }

        [DisplayName("Please tick this box if you wish to receive information about our products and services.")]
        public bool SubscribeToPublications { get; set; }

        [DisplayName("Products and services subscription")]
        public string SubscribeToPublicationsMessage { get; set; }


        [Required]
        [DisplayName("Preferred Language")]
        public string PreferredLanguageSelected { get; set; }

         //<summary>
         //Without Linq using string literals
         //</summary>
        public IEnumerable<SelectListItem>PreferredLanguageOld
        {
            get { return new SelectList(PreferredLanguageOptions, "Value", "Text"); }
        }

         //<summary>
         //Implementing it via Linq no string literals
         //</summary>
        [Required]
        public IEnumerable<SelectListItem> PreferredLanguages
        {
            get
            {
                var allPreferredLanguages = PreferredLanguageOptions.Select(f => new SelectListItem
                {
                    Value = f.Value,
                    Text = f.Text
                });

                return allPreferredLanguages;
                //return DefaultInterestItem.Concat(allFundInterests);

            }
        }

        public IEnumerable<SelectListItem> DefaultLanguageItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "0",
                    Text = "Select",
                    Selected = true
                }, count: 1);
            }
        }


        public string DisplaySelectedPreferredLanguage
        {
            get
            {
                SelectListItem selectedItem = PreferredLanguageOptions.FirstOrDefault(item => item.Value == PreferredLanguageSelected);

                if (selectedItem != null)
                {
                    return selectedItem.Text;
                }
                else
                {
                    return "Select";
                }
            }
        }

        //[Required]
        //[DisplayName("Please nominate your interest in the fund.")]
        //public string FundInterestSelected { get; set; }

        /// <summary>
        /// Without Linq using string literals
        /// </summary>
        //public IEnumerable<SelectListItem> FundInterestsOld
        //{
        //    get { return new SelectList(FundInterestOptions, "Value", "Text"); }
        //}

        /// <summary>
        /// Implementing it via Linq no string literals
        /// </summary>
        //[Required]
        //public IEnumerable<SelectListItem> FundInterests
        //{
        //    get
        //    {
        //        var allFundInterests = FundInterestOptions.Select(f => new SelectListItem
        //        {
        //            Value = f.Value,
        //            Text = f.Text
        //        });

        //        return allFundInterests;
        //        //return DefaultInterestItem.Concat(allFundInterests);

        //    }
        //}

        //public IEnumerable<SelectListItem> DefaultInterestItem
        //{
        //    get
        //    {
        //        return Enumerable.Repeat(new SelectListItem
        //        {
        //            Value = "0",
        //            Text = "Select",
        //            Selected = true
        //        }, count: 1);
        //    }
        //}


        //public string DisplaySelectedFundInterest
        //{
        //    get
        //    {
        //        SelectListItem selectedItem = FundInterestOptions.FirstOrDefault(item => item.Value == FundInterestSelected);

        //        if (selectedItem != null)
        //        {
        //            return selectedItem.Text;
        //        }
        //        else
        //        {
        //            return "Select";
        //        }
        //    }
        //}
    }
}