using Microsoft.EntityFrameworkCore;
using TeamManager.Data.Dtos;
using TeamManager.Data.Enums;
using TeamManager.Data.Mappings;
using TeamManager.Repositories;

namespace TeamManager.Services;

public class LeaveService(ILeaveRepository leaveRepository)
{
    public async Task<LeaveDto?> GetLeaveByIdAsync(int id, CancellationToken ct = default)
    {
        var leave = await leaveRepository.GetByIdAsync(id, ct);
        return leave?.ToDto();
    }

    public async Task<List<LeaveDto>> GetAllLeavesAsync(CancellationToken ct = default)
        => (await leaveRepository.GetAllAsync(ct)).ToDtoList();

    public async Task<LeaveDto> CreateLeaveAsync(CreateLeaveDto dto, CancellationToken ct = default)
    {
        var leave = dto.ToEntity();
        await leaveRepository.AddAsync(leave, ct);
        await leaveRepository.SaveChangesAsync(ct);
        return leave.ToDto();
    }

    public async Task<UpdateResult> UpdateLeaveAsync(int id, UpdateLeaveDto dto, CancellationToken ct = default)
    {
        var leave = await leaveRepository.GetByIdAsync(id, ct);

        if (leave is null) return UpdateResult.NotFound;

        if (dto.StartDate.HasValue) leave.StartDate = dto.StartDate.Value;
        if (dto.EndDate.HasValue) leave.EndDate = dto.EndDate.Value;
        if (dto.Status.HasValue) leave.Status = dto.Status.Value;

        leaveRepository.Update(leave);

        try
        {
            await leaveRepository.SaveChangesAsync(ct);
            return UpdateResult.Success;
        }
        catch (DbUpdateConcurrencyException)
        {
            return UpdateResult.ConcurrencyConflict;
        }
        catch (DbUpdateException)
        {
            return UpdateResult.DatabaseError;
        }
    }

    public async Task<bool> DeleteLeaveAsync(int id, CancellationToken ct = default)
    {
        var leave = await leaveRepository.GetByIdAsync(id, ct);
        if (leave is null) return false;

        leaveRepository.Delete(leave);
        await leaveRepository.SaveChangesAsync(ct);
        return true;
    }
}