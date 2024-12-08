using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Infrastructure.Data.Repositories;
public class BaseRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task ToggleActiveStateAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        // Obtén los estados "Activo" y "No Activo" de la tabla States
        var activeState = await _context.States.FirstOrDefaultAsync(s => s.Name == "Activo");
        var inactiveState = await _context.States.FirstOrDefaultAsync(s => s.Name == "Inactivo");

        if (activeState == null || inactiveState == null)
        {
            throw new InvalidOperationException("The 'Activo' or 'Inactivo' states do not exist in the States table.");
        }

        // Verifica si la entidad tiene una propiedad "StateId" para actualizar el estado
        var stateProperty = typeof(T).GetProperty("StateId");
        if (stateProperty != null && stateProperty.PropertyType == typeof(int))
        {
            var currentStateId = (int)stateProperty.GetValue(entity);

            // Cambia el StateId al estado opuesto
            if (currentStateId == activeState.Id)
            {
                stateProperty.SetValue(entity, inactiveState.Id); // Cambia a "No Activo"
            }
            else if (currentStateId == inactiveState.Id)
            {
                stateProperty.SetValue(entity, activeState.Id); // Cambia a "Activo"
            }
            else
            {
                throw new InvalidOperationException("The current state is neither 'Activo' nor 'No Activo'.");
            }
        }
        else
        {
            throw new InvalidOperationException("The entity does not have a 'StateId' property or it is not of type int.");
        }

        // Marca la entidad como modificada para que se actualice en la base de datos
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}