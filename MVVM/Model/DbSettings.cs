using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeWish.MVVM.Model
{
    public class DbSettings
    {
        public required string DataSource { get; set; }
        public required string InitialCatalog { get; set; }
    }
}
