using TeamManager.Dtos;
using TeamManager.Mappings;
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

    public async Task<bool> DeleteLeaveAsync(int id, CancellationToken ct = default)
    {
        var leave = await leaveRepository.GetByIdAsync(id, ct);
        if (leave is null) return false;

        await leaveRepository.DeleteAsync(leave, ct);
        await leaveRepository.SaveChangesAsync(ct);
        return true;
    }
}