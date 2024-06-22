using RestaurantReservation.Db.Entities;
using Serilog;

namespace RestaurantReservation.Db.Repositories;

public class TableRepository
{
    private readonly RestaurantReservationDbContext _context;

    public TableRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Table> AddTableAsync(Table table)
    {
        try
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
            return table;
        }
        catch (Exception e)
        {
            Log.Error(e, "Error adding table");
            throw;
        }
    }

    public async Task<bool> UpdateTableAsync(Table table)
    {
        try
        {
            var existingTable = await _context.Tables.FindAsync(table.TableID);
            if (existingTable == null)
            {
                return false;
            }

            _context.Entry(existingTable).CurrentValues.SetValues(table);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Error updating table");
            throw;
        }
    }

    public async Task<bool> DeleteTableAsync(int tableId)
    {
        try
        {
            var existingTable = await _context.Tables.FindAsync(tableId);
            if (existingTable == null)
            {
                return false;
            }

            _context.Tables.Remove(existingTable);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Error deleting table");
            throw;
        }
    }

    public async Task<Table?> GetTableAsync(int tableId)
    {
        try
        {
            return await _context.Tables.FindAsync(tableId);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error getting table");
            throw;
        }
    }
}