using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Entities.Source;

namespace VentasETL.Domain.Interfaces.Source.CSV
{
    public interface ICustumerRepository : IGenericCSVFileRepository<Custumer>
    {
    }
}
