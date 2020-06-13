using FluentAssertions;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.ExtensionMethods;
using Xunit;

namespace RandomData.Api.Tests.Services.StringServices
{
    public class StringFormatterExtensionMethodsTests
    {
        [Theory]
        [InlineData(Format.Pascal, "She can live her life however she wants as long as she listens to what I have to say.",
        "SheCanLiveHerLifeHoweverSheWantsAsLongAsSheListensToWhatIHaveToSay.")]
        [InlineData(Format.Kebab, "Nobody questions who built the pyramids in Mexico.",
            "nobody-questions-who-built-the-pyramids-in-mexico.")]
        [InlineData(Format.Lower, "Courage and stupidity were all he had.", "courage and stupidity were all he had.")]
        [InlineData(Format.Upper, "There can never be too many cherries on an ice cream sundae.", "THERE CAN NEVER BE TOO MANY CHERRIES ON AN ICE CREAM SUNDAE.")]
        [InlineData(Format.Camel, "His mind was blown that there was nothing in space except space itself.",
            "hisMindWasBlownThatThereWasNothingInSpaceExceptSpaceItself.")]
        [InlineData(Format.Snake, "The urgent care center was flooded with patients after the news of a new deadly virus was made public.",
            "the_urgent_care_center_was_flooded_with_patients_after_the_news_of_a_new_deadly_virus_was_made_public.")]
        [InlineData(Format.Default, "Nobody questions who built the pyramids in Mexico.", "Nobody questions who built the pyramids in Mexico.")]
        public void FormatToExtensionTests(Format format, string input, string output)
        {
            input.FormatTo(format).Should().Be(output);
        }
        
        [Theory]
        [InlineData("Nobody questions who built the pyramids in Mexico.",
            "NobodyQuestionsWhoBuiltThePyramidsInMexico.")]
        [InlineData("His mind was blown that there was nothing in space except space itself.",
            "HisMindWasBlownThatThereWasNothingInSpaceExceptSpaceItself.")]
        [InlineData(
            "The urgent care center was flooded with patients after the news of a new deadly virus was made public.",
            "TheUrgentCareCenterWasFloodedWithPatientsAfterTheNewsOfANewDeadlyVirusWasMadePublic.")]
        [InlineData("There can never be too many cherries on an ice cream sundae.",
            "ThereCanNeverBeTooManyCherriesOnAnIceCreamSundae.")]
        [InlineData("Courage and stupidity were all he had.", "CourageAndStupidityWereAllHeHad.")]
        [InlineData("She can live her life however she wants as long as she listens to what I have to say.",
            "SheCanLiveHerLifeHoweverSheWantsAsLongAsSheListensToWhatIHaveToSay.")]
        public void ToPascalCaseExtensionTests(string input, string output)
        {
            input.ToPascalCase().Should().Be(output);
        }

        [Theory]
        [InlineData("Nobody questions who built the pyramids in Mexico.",
            "nobodyQuestionsWhoBuiltThePyramidsInMexico.")]
        [InlineData("His mind was blown that there was nothing in space except space itself.",
            "hisMindWasBlownThatThereWasNothingInSpaceExceptSpaceItself.")]
        [InlineData(
            "The urgent care center was flooded with patients after the news of a new deadly virus was made public.",
            "theUrgentCareCenterWasFloodedWithPatientsAfterTheNewsOfANewDeadlyVirusWasMadePublic.")]
        [InlineData("There can never be too many cherries on an ice cream sundae.",
            "thereCanNeverBeTooManyCherriesOnAnIceCreamSundae.")]
        [InlineData("Courage and stupidity were all he had.", "courageAndStupidityWereAllHeHad.")]
        [InlineData("She can live her life however she wants as long as she listens to what I have to say.",
            "sheCanLiveHerLifeHoweverSheWantsAsLongAsSheListensToWhatIHaveToSay.")]
        public void ToCamelCaseExtensionTests(string input, string output)
        {
            input.ToCamelCase().Should().Be(output);
        }

        [Theory]
        [InlineData("Nobody questions who built the pyramids in Mexico.",
            "nobody_questions_who_built_the_pyramids_in_mexico.")]
        [InlineData("His mind was blown that there was nothing in space except space itself.",
            "his_mind_was_blown_that_there_was_nothing_in_space_except_space_itself.")]
        [InlineData(
            "The urgent care center was flooded with patients after the news of a new deadly virus was made public.",
            "the_urgent_care_center_was_flooded_with_patients_after_the_news_of_a_new_deadly_virus_was_made_public.")]
        [InlineData("There can never be too many cherries on an ice cream sundae.",
            "there_can_never_be_too_many_cherries_on_an_ice_cream_sundae.")]
        [InlineData("Courage and stupidity were all he had.", "courage_and_stupidity_were_all_he_had.")]
        [InlineData("She can live her life however she wants as long as she listens to what I have to say.",
            "she_can_live_her_life_however_she_wants_as_long_as_she_listens_to_what_i_have_to_say.")]
        public void ToSnakeCaseExtensionTests(string input, string output)
        {
            input.ToSnakeCase().Should().Be(output);
        }
        
        [Theory]
        [InlineData("Nobody questions who built the pyramids in Mexico.",
            "nobody-questions-who-built-the-pyramids-in-mexico.")]
        [InlineData("His mind was blown that there was nothing in space except space itself.",
            "his-mind-was-blown-that-there-was-nothing-in-space-except-space-itself.")]
        [InlineData(
            "The urgent care center was flooded with patients after the news of a new deadly virus was made public.",
            "the-urgent-care-center-was-flooded-with-patients-after-the-news-of-a-new-deadly-virus-was-made-public.")]
        [InlineData("There can never be too many cherries on an ice cream sundae.",
            "there-can-never-be-too-many-cherries-on-an-ice-cream-sundae.")]
        [InlineData("Courage and stupidity were all he had.", "courage-and-stupidity-were-all-he-had.")]
        [InlineData("She can live her life however she wants as long as she listens to what I have to say.",
            "she-can-live-her-life-however-she-wants-as-long-as-she-listens-to-what-i-have-to-say.")]
        public void ToKebabCaseExtensionTests(string input, string output)
        {
            input.ToKebabCase().Should().Be(output);
        }
    }
}