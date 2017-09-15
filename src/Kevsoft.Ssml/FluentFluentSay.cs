using System.Threading.Tasks;
using System.Xml;

namespace Kevsoft.Ssml
{
    public class FluentFluentSay : FluentSsml, IFluentSay, ISsmlWriter
    {
        private readonly string _value;
        private ISsmlWriter _ssmlWriter;

        public FluentFluentSay(string value, ISsml ssml)
            : base(ssml)
        {
            _value = value;
            _ssmlWriter = new PlainTextWriter(value);
        }

        public async Task WriteAsync(XmlWriter xml)
        {
            await _ssmlWriter.WriteAsync(xml)
                .ConfigureAwait(false);
        }

        public ISsml AsAlias(string alias)
        {
            _ssmlWriter = new SubWriter(_ssmlWriter, alias);

            return this;
        }

        public ISsml Emphasised()
        {
            return Emphasised(EmphasiseLevel.NotSet);
        }

        public ISsml Emphasised(EmphasiseLevel level)
        {
            _ssmlWriter = new EmphasiseWriter(_ssmlWriter, level);

            return this;
        }

        public ISsml AsTelephone()
        {
            _ssmlWriter = new SayAsWriter("telephone", _value);

            return this;
        }

        public ISsml AsCharacters()
        {
            _ssmlWriter = new SayAsWriter("characters", "characters", _value);

            return this;
        }
    }
}