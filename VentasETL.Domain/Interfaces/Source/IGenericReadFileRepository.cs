using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasETL.Domain.Interfaces.Source
{
    public interface IGenericReadFileRepository<Tentity> where Tentity : class
    {

        Task<IEnumerable<Tentity>> ReadFile(string Path);



    }
}
