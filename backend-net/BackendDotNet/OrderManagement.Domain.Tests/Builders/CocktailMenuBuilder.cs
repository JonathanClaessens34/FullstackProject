using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Domain.Tests.Builders
{
    internal class CocktailMenuBuilder : BuilderBase<CocktailMenu>
    {
        public CocktailMenuBuilder() 
        {
            ConstructItem();
            SetProperty(e => e.Id, new Guid());
            SetProperty(e => e.BarName, Random.NextString());   
        }
    }
}
