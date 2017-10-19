using System.Threading.Tasks;

namespace HordeFlow.HR.Infrastructure
{
    public interface ISeeder
    {
        Task EnsureSeededAsync();
    }
}