using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLetterWordChallenge.Domain.CombinedWords;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixLetterWordChallenge.Domain.Tests.CombinedWords
{
    [TestClass]
    public class CombinationGeneratorTest
    {
        [TestMethod]
        public void Should_Return_Combinations()
        {
            // Arrange
            var sut = new CombinationGenerator();
            var length = 4;

            var words = new List<Word>
            {
                Word.Create("a"),
                Word.Create("b"),
                Word.Create("c"),
                Word.Create("d")
            };

            // Act
            var results = sut.Generate(words, length).ToList();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(256, results.Count);
        }

        [TestMethod]
        public void Should_Return_No_Combinations_When_The_Words_Are_The_Same_Length_As_The_Given_Length()
        {
            // Arrange
            var sut = new CombinationGenerator();
            var length = 4;

            var words = new List<Word>
            {
                Word.Create("play"),
                Word.Create("stay")
            };

            // Act
            var results = sut.Generate(words, length).ToList();

            // Assert
            Assert.IsNotNull(results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Should_Return_Nothing_When_The_Input_Is_Empty()
        {
            // Arrange
            var sut = new CombinationGenerator();
            var length = 9;
            var words = new List<Word>();

            // Act
            var results = sut.Generate(words, length).ToList();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(0)]
        [DataRow(-1)]
        public void Should_Throw_ArgumentException_When_Length_Is_One_Or_Less(int length)
        {
            // Arrange
            var sut = new CombinationGenerator();
            var words = new List<Word>();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Generate(words, length));
        }
    }
}