using Newtonsoft.Json.Linq;

namespace Rigid.Matchers
{
    public interface IMatcher
    {
        bool Match(JToken actualValue);
    }
}
