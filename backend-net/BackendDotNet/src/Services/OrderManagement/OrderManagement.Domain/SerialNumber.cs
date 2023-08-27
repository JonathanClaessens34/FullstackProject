using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public class SerialNumber : ValueObject<SerialNumber>
    {
        public String Nummer { get; }

        public static implicit operator String(SerialNumber d) => d.Nummer;
        public static implicit operator SerialNumber(string b) => new SerialNumber(b);

        public override string ToString() => Nummer;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nummer;
        }

        public SerialNumber(string serialNumber)
        {
            if (serialNumber.Length != 13)
            {
                throw new BarCodeException("SerialNumber nummer klopt niet");
            }

            // Calculate the checksum
            int sum = 0;
            for (int i = 0; i < 13; i++)
            {
                int digit = int.Parse(serialNumber[i].ToString());

                // Every second digit is multiplied by 3
                if (i % 2 == 0)
                {
                    digit *= 3;
                }

                sum += digit;
            }

            // The checksum is the smallest number that can be added to the sum
            // to make it a multiple of 10
            int checksum = 10 - (sum % 10);
            if (checksum == 10)
            {
                checksum = 0;
            }
            Nummer = serialNumber;

            // Check that the checksum in the input string matches the calculated checksum
            //int checksumInInput = int.Parse(serialNumber[12].ToString());
            //if(checksumInInput == checksum)//checksumInInput == checksum
            //{
            //    Nummer = serialNumber;
            //}
            //else
            //{
            //    throw new BarCodeException("SerialNumber nummer klopt niet");
            //}


        }
    }
}
