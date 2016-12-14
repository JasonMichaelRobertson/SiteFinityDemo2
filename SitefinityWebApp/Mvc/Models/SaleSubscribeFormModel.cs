using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SitefinityWebApp.Mvc.Models
{
    public class SaleSubscribeFormModel
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

        public SaleSubscribeFormModel()
        {

        }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Organisation")]
        public string Organisation { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
                
        [DisplayName("Phone")]
        public string Phone { get; set; }

        [DisplayName("All")]		
        public bool All { get; set; }

        [DisplayName("Agribusiness")]		
        public bool Agribusiness { get; set; }

        [DisplayName("Aviation")]		
        public bool Aviation { get; set; }

        [DisplayName("Building & Construction")]		
        public bool BuildingConstruction { get; set; }

        [DisplayName("Education & Training")]		
        public bool EducationTraining { get; set; }

        [DisplayName("Finance & Business Services")]		
        public bool FinanceBusinessServices { get; set; }

        [DisplayName("Food & Beverage")]		
        public bool FoodBeverage { get; set; }

        [DisplayName("Health & Aged Care")]		
        public bool HealthAgedCare { get; set; }

        [DisplayName("Hospitality & Leisure")]		
        public bool HospitalityLeisure { get; set; }

        [DisplayName("Information Technology")]		
        public bool InformationTechnology { get; set; }

        [DisplayName("Infrastructure & Utilities")]		
        public bool InfrastructureUtilities { get; set; }

        [DisplayName("Manufacturing")]		
        public bool Manufacturing { get; set; }

        [DisplayName("Media")]		
        public bool Media { get; set; }

        [DisplayName("Mining & Resources")]		
        public bool MiningResources { get; set; }

        [DisplayName("Motor Vehicle")]		
        public bool MotorVehicle { get; set; }

        [DisplayName("Prinitng & Publishing")]		
        public bool PrinitngPublishing { get; set; }

        [DisplayName("Real Estate")]		
        public bool RealEstate { get; set; }

        [DisplayName("Retail & Consumer")]		
        public bool RetailConsumer { get; set; }

        [DisplayName("Textile, Clothing & Footwear")]		
        public bool TextileClothingFootwear { get; set; }

        [DisplayName("Transport & Logistics")]		
        public bool TransportLogistics { get; set; }

        [DisplayName("Selected industry sale asset notification.")]
        public string ConfirmSelectedIndustries { get; set; }


    }
}