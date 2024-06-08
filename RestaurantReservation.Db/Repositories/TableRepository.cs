using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories;

public class TableRepository
{
    private readonly RestaurantReservationDbContext _context;

    public TableRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddTableAsync(Table table)
    {
        _context.Tables.Add(table);
        await _context.SaveChangesAsync();
        return "Table added successfully.";
    }

    public async Task<string> UpdateTableAsync(Table table)
    {
        var existingTable = await _context.Tables.FindAsync(table.TableID);
        if (existingTable == null)
        {
            return "Table not found.";
        }

        _context.Entry(existingTable).CurrentValues.SetValues(table);
        await _context.SaveChangesAsync();
        return "Table updated successfully.";
    }

    public async Task<string> DeleteTableAsync(int tableId)
    {
        var existingTable = await _context.Tables.FindAsync(tableId);
        if (existingTable == null)
        {
            return "Table not found.";
        }

        _context.Tables.Remove(existingTable);
        await _context.SaveChangesAsync();
        return "Table deleted successfully.";
    }

    public async Task<Table?> GetTableAsync(int tableId)
    {
        return await _context.Tables.FindAsync(tableId);
    }
}