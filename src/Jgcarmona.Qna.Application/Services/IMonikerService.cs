using System.Threading.Tasks;

namespace Jgcarmona.Qna.Application.Services
{
    public interface IMonikerService
    {
        Task<string> GenerateMonikerAsync<T>(string baseText) where T : IdentifiableEntity;
    }
}
