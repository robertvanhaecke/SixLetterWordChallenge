using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLetterWordChallenge.Domain.CombinedWords;
using System;

namespace SixLetterWordChallenge.Domain.Tests.CombinedWords
{
    [TestClass]
    public class CombinedWordTest
    {
        [TestMethod]
        public void Create_When_Input_Is_Null_Then_ArgumentNullException_Is_Thrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => CombinedWord.Create(null));
        }

        [TestMethod]
        public void Create_When_Input_Is_Empty_Then_ArgumentException_Is_Thrown()
        {
            Assert.ThrowsException<ArgumentException>(() => CombinedWord.Create(Array.Empty<Word>()));
        }

        [TestMethod]
        public void Create_When_Input_Contains_One_Word_Then_CombinedWord_Is_Created_Successfully()
        {
            // Arrange
            var word = Word.Create("Test");
            var input = new Word[] { word };

            // Act
            var result = CombinedWord.Create(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Words.Count);
            Assert.IsTrue(result.Words.Contains(word));
        }

        [TestMethod]
        public void Create_When_Input_Contains_Multiple_Words_Then_CombinedWord_Is_Created_Successfully()
        {
            // Arrange
            var word1 = Word.Create("Test1");
            var word2 = Word.Create("Test2");
            var word3 = Word.Create("Test3");
            var input = new Word[] { word1, word2, word3 };

            // Act
            var result = CombinedWord.Create(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Words.Count);
            Assert.IsTrue(result.Words.Contains(word1));
            Assert.IsTrue(result.Words.Contains(word2));
            Assert.IsTrue(result.Words.Contains(word3));
        }

        [TestMethod]
        public void Combine_When_CombinedWord_Is_Null_Then_ArgumentNullException_Is_Thrown()
        {
            // Arrange
            var word = Word.Create("Test");

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => CombinedWord.Combine(null, word));
        }

        [TestMethod]
        public void Combine_When_Word_Is_Null_Then_ArgumentNullException_Is_Thrown()
        {
            // Arrange
            var word = Word.Create("Test");
            var input = new Word[] { word };
            var combinedWord = CombinedWord.Create(input);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => CombinedWord.Combine(combinedWord, null));
        }

        [TestMethod]
        public void Combine_When_Input_Is_Valid_Then_CombinedWord_Is_Combined_Successfully()
        {
            // Arrange
            var word1 = Word.Create("Test1");
            var word2 = Word.Create("Test2");
            var input = new Word[] { word1, word2 };
            var combinedWord = CombinedWord.Create(input);

            var wordToCombine = Word.Create("Combine");

            // Act
            var result = CombinedWord.Combine(combinedWord, wordToCombine);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Words.Count);
            Assert.IsTrue(result.Words.Contains(word1));
            Assert.IsTrue(result.Words.Contains(word2));
            Assert.IsTrue(result.Words.Contains(wordToCombine));
        }
    }
}