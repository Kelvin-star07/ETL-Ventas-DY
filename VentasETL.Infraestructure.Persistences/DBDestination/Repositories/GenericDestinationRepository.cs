using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VentasETL.Domain.Interfaces.Destination;
using VentasETL.Infraestructure.Persistences.Contexto;

namespace VentasETL.Infraestructure.Persistences.Destination.Repositories
{
    public class GenericDestinationRepository<Tentity> : IGenericDestinationRepository<Tentity> where Tentity : class
    {

        private readonly DWHContext _context;
        private readonly ILogger<Tentity> _logger;

        public GenericDestinationRepository(DWHContext context, ILogger<Tentity> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Tentity> AddReturnAsync(Tentity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.ChangeTracker.Clear();
                await _context.Set<Tentity>().AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar entidad en destino");
                throw;
            }
        }

        public async Task<List<Tentity>> AddRangeReturnAsync(IEnumerable<Tentity> entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    throw new ArgumentException("La lista está vacía");

                _context.ChangeTracker.Clear();
                await _context.Set<Tentity>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return entities.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar range en destino");
                throw;
            }
        }



        public async Task<List<Tentity>> GetAllAsync()
        {
            return await _context.Set<Tentity>().ToListAsync();
        }



        public async Task UpdateRangeAsync(IEnumerable<Tentity> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException("Lista vacía");

            _context.Set<Tentity>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }


    }
}
