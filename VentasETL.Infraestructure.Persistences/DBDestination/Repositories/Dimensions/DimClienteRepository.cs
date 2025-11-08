using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Entities.Destination.Dimensions;
using VentasETL.Domain.Interfaces.Destination;
using VentasETL.Domain.Interfaces.Destination.Dimensions;
using VentasETL.Infraestructure.Persistences.Contexto;

namespace VentasETL.Infraestructure.Persistences.Destination.Repositories.Dimensions
{
    public class DimClienteRepository : GenericDestinationRepository<DimCliente>, IDimClienteRepository
    {

        private readonly DWHContext _context;



        public DimClienteRepository(DWHContext context,ILogger<DimCliente> logger) : base(context,logger)
        {

            _context = context; 


        }

        public async  Task<DimCliente?> GetByIdAsync(int clienteId)
        {

            if (clienteId < 0)
                throw new ArgumentException($"No se encuentra una  entidad con ese Id:{clienteId}");


            return await _context.Set<DimCliente>().FindAsync(clienteId);
            
        }










    }

}
