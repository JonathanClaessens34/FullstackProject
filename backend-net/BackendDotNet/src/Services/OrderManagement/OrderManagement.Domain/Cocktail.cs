using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public class Cocktail : Entity
    {
        public SerialNumber SerialNumber { get; private set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        private Cocktail()
        {
            SerialNumber = null;
            Name = string.Empty;
        }

        public Cocktail(string serialNumber, string name, string imageUrl)
        {
            SerialNumber = new SerialNumber(serialNumber);
            Name = name;
            ImageUrl = imageUrl;
        }

        protected override IEnumerable<object> GetIdComponents()
        {
            yield return SerialNumber;
        }

        public static Cocktail CreateNew(string serialNumber, string name, string imageUrl)
        {
            return new Cocktail(serialNumber, name, imageUrl);
        }
    }

}
