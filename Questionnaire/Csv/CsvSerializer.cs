using System;
using System.Collections.Generic;
using System.Reflection;

namespace Questionnaire.Csv
{
    public class CsvSerializer : ICsvSerializer
    {

        public IList<string> SerializeToCsvFile<T>(IEnumerable<T> objectsToSerialize, bool includeHeader)
        {
            var csvFile = new List<string>();
            if (includeHeader)
            {
                csvFile.Add(SerializeHeader(typeof(T)));
            }
            foreach (var item in objectsToSerialize)
            {
                csvFile.Add(SerializeLine(item));
            }
            return csvFile;
        }

        public string SerializeLine(object objectToSerialize)
        {
            PropertyInfo[] propertyInfos = objectToSerialize.GetType().GetProperties();
            var values = new List<string>();
            OrderByAttributeValue(propertyInfos);

            foreach (var propertyInfo in propertyInfos)
            {
                values.Add(propertyInfo.GetValue(objectToSerialize).ToString());
            }

            return CreateCsvLine(values);
        }

        public string SerializeHeader(Type type)
        {
            PropertyInfo[] propertyInfos = type.GetProperties();
            OrderByAttributeValue(propertyInfos);
            var headerValues = new List<string>();

            foreach (var propertyInfo in propertyInfos)
            {
                var attr = propertyInfo.GetCustomAttribute<CsvConfigAttribute>();
                headerValues.Add(attr.Header);
            }

            return CreateCsvLine(headerValues);
        }

        private string CreateCsvLine(IEnumerable<string> values)
        {
            return string.Join(';', values);
        }

        private void OrderByAttributeValue(PropertyInfo[] propertyInfos)
        {
            Array.Sort(propertyInfos, (PropertyInfo p1, PropertyInfo p2) =>
            {
                var sortOrderP1 = p1.GetCustomAttribute<CsvConfigAttribute>()?.SortOrder;
                var sortOrderP2 = p2.GetCustomAttribute<CsvConfigAttribute>()?.SortOrder;

                if (sortOrderP1 is null && sortOrderP2 is null)
                {
                    return p1.Name.CompareTo(p2.Name);
                }

                sortOrderP1 ??= 0;
                sortOrderP2 ??= 0;
                return sortOrderP1.Value.CompareTo(sortOrderP2.Value);
            });
        }
    }
}
