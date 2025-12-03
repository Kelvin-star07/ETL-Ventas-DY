using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Aplication.Interfaces.Destination;
using VentasETL.Domain.Interfaces.Destination;

namespace VentasETL.Aplication.Services.Destination
{
    public class GenericDestinationService<T> : IGenericDetinationService<T> where T : class
    {
        private readonly  IGenericDestinationRepository<T> repo;

        public GenericDestinationService(IGenericDestinationRepository<T> repo )
        {
           this.repo = repo;    
        }

       

        public async Task AddAsync(T entity)
        {
            try
            {


                if(entity == null)
                {

                    throw new ArgumentNullException("Error al agregar la entidad");
                   
                }

                await repo.AddReturnAsync(entity);  

            }
            catch (Exception ex)
            {



                throw new Exception();
            
            }

        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {

            try
            {

                if(entities == null) 
                {

                    throw new Exception("Error al agregar el listado de entidades");
                
                }


                await repo.AddRangeReturnAsync(entities); 


            } catch (Exception ex) 
            {
            
            
                 throw new Exception();   
            
            }
          
        }
    
    }
}

