using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericStore.Identity.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task RegisterUserAsync(UserDTO userDTO);

        Task<string> LoginAsync(string email, string password);
    }
}
