using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Kevsoft.Ssml
{
    public class Ssml : ISsml
    {
        private readonly List<ISsmlWriter> _says = new List<ISsmlWriter>();

        public ISay Say(string value)
        {
            var say = new Say(value, this);

            _says.Add(say);

            return say;
        }

        public async Task Write(XmlWriter writer)
        {
            await writer.WriteStartDocumentAsync()
                .ConfigureAwait(false);

            await writer.WriteStartElementAsync(null, "speak", null)
                .ConfigureAwait(false);

            for (var index = 0; index < _says.Count; index++)
            {
                var say = _says[index];

                await say.WriteAsync(writer)
                    .ConfigureAwait(false);

                if (index != _says.Count -1)
                {
                    await writer.WriteStringAsync(" ")
                        .ConfigureAwait(false);
                }
            }

            await writer.WriteEndElementAsync()
                .ConfigureAwait(false);

            await writer.WriteEndDocumentAsync();
        }

        public async Task<string> ToStringAsync()
        {
            var stringBuilder = new StringBuilder();
            var xmlWriterSettings = new XmlWriterSettings()
            {
                Async = true
            };

            using (var xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings))
            {
                await Write(xmlWriter);
                await xmlWriter.FlushAsync();
                    
                return stringBuilder.ToString();
            }
        }

        public IBreak Break()
        {
            var @break = new BreakWriter(this);

            _says.Add(@break);

            return @break;
        }
    }

    /*
    public enum InterpretAs
    {
        None = 0,
        Date,
        Time,
        Telephone,
        Characters,
        Cardinal,
        Ordinal
    }

    public enum InterpretAsDateFormat
    {
        None = 0,
        MonthDaYearYear,
        DaYearMonthYear,
        YearMonthDaYear,
        MonthDaYear,
        DaYearMonth,
        YearMonth,
        MonthYear,
        DaYear,
        Month,
        Year,
    }*/
}
