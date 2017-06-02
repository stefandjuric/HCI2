using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HCI2.Model
{
    [Serializable]
    [DataContract]
    public class SoftwareItem
    {
        [DataMember]
        private string id;

        [DataMember]
        private string name;

        [DataMember]
        private int operatingSystem;

        [DataMember]
        private string producer;

        [DataMember]
        private string site;

        [DataMember]
        private DateTime productionYear;

        [DataMember]
        private double price;

        [DataMember]
        private string description;

        public SoftwareItem() { }

        public SoftwareItem(string name, int operatingSystem, string producer,
            string site, DateTime productionYear, double price, string description)
        {
            this.id = DateTime.Now.ToString("MMddyyHmmss");
            this.name = name;
            this.operatingSystem = operatingSystem;
            this.producer = producer;
            this.site = site;
            this.productionYear = productionYear;
            this.price = price;
            this.description = description;

        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int OperatingSystem
        {
            get { return operatingSystem; }
            set { operatingSystem = value; }
        }

        public string Producer
        {
            get { return producer; }
            set { producer = value; }
        }

        public string Site
        {
            get { return site; }
            set { site = value; }
        }

        public DateTime ProductionYear
        {
            get { return productionYear; }
            set { productionYear = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }


    }
}
