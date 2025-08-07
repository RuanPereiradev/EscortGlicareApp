using GlicareApp.Domain.Entities;

namespace GlicareApp.Domain.Interfaces.Repositories;

public interface IPacientRepository
{
    Task<bool> ExistsAsync(string patientId);
    Task<List<Patient>> GetAllPatientsAsync();
    Task<Patient> GetPatientByIdAsync(string patientId);
}