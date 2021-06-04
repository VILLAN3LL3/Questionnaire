using System;

namespace Questionnaire.Csv
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CsvConfigAttribute : Attribute
    {
        public CsvConfigAttribute(string header, int sortOrder)
        {
            Header = header;
            SortOrder = sortOrder;
        }

        public string Header { get; }
        public int SortOrder { get; }
    }
}
