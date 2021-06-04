using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Questionnaire.Data;

namespace Questionnaire.Tests.Data
{
    [TestFixture]
    public class QuestionnaireCreatorTests
    {
        private QuestionnaireCreator CreateQuestionnaireCreator() => new();

        [Test]
        public void CreateQuestionnaire_StateUnderTest_ExpectedBehavior()
        {
            QuestionnaireCreator questionnaireCreator = CreateQuestionnaireCreator();
            string[] textLines = new string[]
            {
                "?Which of these animals is a mammal",
                "Ant",
                "Bee",
                "*Cat",
                "*Dog",
                "??What is the sum of 2+3",
                "2",
                "*5",
                "6",
                "?What kind of animal is \"Cisco\" in \"Dances with Wolves\"",
                "*Horse"
            };
            IList<Question> expectedresult = new List<Question>
            {
                TestData.MultiSelectQuestion,
                TestData.OptionalQuestion,
                TestData.TextInputQuestion
            };
            IList<QuestionType> expectedTypes = new List<QuestionType> { QuestionType.MultiSelect, QuestionType.SingleSelect, QuestionType.TextInput };
            bool[] expectedIsValidFlags = new bool[] { false, true, false };

            IList<Question> result = questionnaireCreator.CreateQuestionnaire(
                textLines);

            result.Should().BeEquivalentTo(expectedresult, opt => opt.WithStrictOrdering());
            result.Select(r => r.Type).Should().BeEquivalentTo(expectedTypes, opt => opt.WithStrictOrdering());
            result.Select(r => r.IsValid).Should().BeEquivalentTo(expectedIsValidFlags, opt => opt.WithStrictOrdering());
            result.Select(r => r.IsOptional).Should().BeEquivalentTo(expectedIsValidFlags, opt => opt.WithStrictOrdering());
        }
    }
}
