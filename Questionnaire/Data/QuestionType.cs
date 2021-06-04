using System;

namespace Questionnaire.Data
{
    [Flags]
    public enum QuestionType
    {
        SingleSelect = 1 << 0, //1
        MultiSelect = 1 << 1, // 2
        TextInput = 1 << 2 // 4
    }
}
