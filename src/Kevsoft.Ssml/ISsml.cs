using System.Threading.Tasks;

namespace Kevsoft.Ssml
{
    public interface ISsml
    {
        ISay Say(string value);

        Task<string> ToStringAsync();
    }
}