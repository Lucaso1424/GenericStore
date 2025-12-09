using Core.Application.DTOs;
using GenericStore.Domain.Entities;
using GenericStore.Domain.Enums;

namespace GenericStore.Application.DTOs;
public partial class UserDTO : BaseEntityDTO
{
    public int UserId { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public int RoleId { get; set; }

    public string? RoleDisplayName { get; set; }
}