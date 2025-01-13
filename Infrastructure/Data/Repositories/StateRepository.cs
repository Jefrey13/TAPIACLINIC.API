    using Domain.Entities;
    using Domain.Repositories;
    using Microsoft.EntityFrameworkCore;

    namespace Infrastructure.Data.Repositories;

    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext context) : base(context) { }

    // Sobrescribe GetAllAsync para excluir estados Activo e Inactivo
    public override async Task<IEnumerable<State>> GetAllAsync()
    {
        return await _context.States
            .Where(s => s.Name != "Activo" && s.Name != "Inactivo") // Excluir estados
            .ToListAsync();
    }
    public async Task<IEnumerable<State>> GetStatesByTypeAsync(string stateType)
        {
            return await _context.States
                .Where(s => s.StateType == stateType)
                .ToListAsync();
        }
    }