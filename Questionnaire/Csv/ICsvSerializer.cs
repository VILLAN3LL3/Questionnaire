using System.Collections.Generic;

namespace Questionnaire.Csv
{
    public interface ICsvSerializer
    {
        IList<string> SerializeToCsvFile<T>(IEnumerable<T> objectsToSerialize, bool includeHeader);
    }
}