using System.Threading.Tasks;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Abstract.Services
{
    public interface IMonikerService
    {
        Task<string> GenerateMonikerAsync<T>(string baseText) where T : IdentifiableEntity;
    }
}
