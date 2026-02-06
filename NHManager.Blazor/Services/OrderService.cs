using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IOrderService
{
    Task<List<Order>> GetAllAsync();
    Task<List<Order>> GetByClientIdAsync(int clientId);
    Task<Order?> GetByIdAsync(int id);
    Task<Order> CreateAsync(Order order, string userName);
    Task<Order> UpdateAsync(Order order, string userName, List<int>? deletedItemIds = null);
    Task DeleteAsync(int id, string userName);
}

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Where(o => o.Valid)
            .Include(o => o.Client)
            .Include(o => o.Consultant)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.CreateDate)
            .ToListAsync();
    }

    public async Task<List<Order>> GetByClientIdAsync(int clientId)
    {
        return await _context.Orders
            .Where(o => o.Valid && o.ClientId == clientId)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.CreateDate)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Client)
            .Include(o => o.Consultant)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id && o.Valid);
    }

    public async Task<Order> CreateAsync(Order order, string userName)
    {
        order.CreatedAt = DateTime.Now;
        order.UpdatedAt = DateTime.Now;
        order.CreatedBy = userName;
        order.UpdatedBy = userName;
        order.Valid = true;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateAsync(Order order, string userName, List<int>? deletedItemIds = null)
    {
        if (deletedItemIds != null && deletedItemIds.Any())
        {
            var itemsToDelete = _context.OrderItems.Where(x => deletedItemIds.Contains(x.Id)).ToList();
            if(itemsToDelete.Any())
            {
                 _context.OrderItems.RemoveRange(itemsToDelete);
            }
        }

        order.UpdatedAt = DateTime.Now;
        order.UpdatedBy = userName;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task DeleteAsync(int id, string userName)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            order.Valid = false;
            order.UpdatedAt = DateTime.Now;
            order.UpdatedBy = userName;
            await _context.SaveChangesAsync();
        }
    }
}
