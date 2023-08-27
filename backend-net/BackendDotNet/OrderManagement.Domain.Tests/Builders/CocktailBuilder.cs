using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Domain.Tests.Builders
{
    internal class CocktailBuilder : BuilderBase<Cocktail>
    {
        public CocktailBuilder() 
        {
            ConstructItem();
            SetProperty(e => e.SerialNumber, new SerialNumber("1234567891011"));
            SetProperty(e => e.Name, Random.NextString());
        }
    }
}
