using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLetterWordChallenge.Domain.CombinedWords;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixLetterWordChallenge.Domain.Tests.CombinedWords
{
    [TestClass]
    public class WordCombinerTest
    {
        [TestMethod]
        public void Should_Return_Multiple_Results_For_Same_Word()
        {
            // Arrange
            var combinationGenerator = A.Fake<ICombinationGenerator>();
            var sut = new WordCombiner(combinationGenerator);

            var length = 9;

            var words = new List<Word>
            {
                Word.Create("microsoft"),
                Word.Create("micro"),
                Word.Create("soft"),
                Word.Create("mi"),
                Word.Create("cr"),
                Word.Create("oso"),
                Word.Create("ft")
            };

            A.CallTo(() => combinationGenerator.Generate(words, length)).Returns(new List<CombinedWord>
            {
                CombinedWord.Create(Word.Create("micro"), Word.Create("soft")),
                CombinedWord.Create(Word.Create("mi"), Word.Create("cr")),
                CombinedWord.Create(Word.Create("mi"), Word.Create("cr"), Word.Create("oso")),
                CombinedWord.Create(Word.Create("mi"), Word.Create("cr"), Word.Create("oso"), Word.Create("ft"))
            });

            var expectedCombination1 = CombinedWord.Create(Word.Create("micro"), Word.Create("soft"));
            var expectedCombination2 = CombinedWord.Create(Word.Create("mi"), Word.Create("cr"), Word.Create("oso"), Word.Create("ft"));

            // Act
            var results = sut.Combine(words, length).ToList();

            // Assert
            Assert.IsNotNull(results);

            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.Any(r => r.Words.SequenceEqual(expectedCombination1.Words)));
            Assert.IsTrue(results.Any(r => r.Words.SequenceEqual(expectedCombination2.Words)));
        }

        [TestMethod]
        public void Should_Return_No_Results_When_No_Valid_Combination_Can_Be_Made()
        {
            // Arrange
            var combinationGenerator = A.Fake<ICombinationGenerator>();
            var sut = new WordCombiner(combinationGenerator);

            var length = 9;

            var words = new List<Word>
            {
                Word.Create("micro"),
                Word.Create("soft"),
                Word.Create("mi"),
                Word.Create("cr"),
                Word.Create("oso"),
                Word.Create("ft")
            };

            A.CallTo(() => combinationGenerator.Generate(words, length)).Returns(new List<CombinedWord>
            {
                CombinedWord.Create(Word.Create("micro"), Word.Create("soft")),
                CombinedWord.Create(Word.Create("mi"), Word.Create("cr")),
                CombinedWord.Create(Word.Create("mi"), Word.Create("cr"), Word.Create("oso")),
                CombinedWord.Create(Word.Create("mi"), Word.Create("cr"), Word.Create("oso"), Word.Create("ft"))
            });

            // Act
            var results = sut.Combine(words, length).ToList();

            // Assert
            Assert.IsNotNull(results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Should_Return_No_Results_When_There_Are_No_Words_With_The_Correct_Length()
        {
            // Arrange
            var combinationGenerator = A.Fake<ICombinationGenerator>();
            var sut = new WordCombiner(combinationGenerator);

            var length = 10;

            var words = new List<Word>
            {
                Word.Create("microsoft"),
                Word.Create("micro"),
                Word.Create("soft"),
                Word.Create("mi"),
                Word.Create("cr"),
                Word.Create("oso"),
                Word.Create("ft")
            };

            // Act
            var results = sut.Combine(words, length).ToList();

            // Assert
            Assert.IsNotNull(results);

            Assert.AreEqual(0, results.Count);
        }
    }
}