using Xunit;
using AdaptiveHuffman.Core;
using System.IO;
using System.Collections.Generic;

namespace AdaptiveHuffman.UnitTests
{
  public class BitReaderTest
  {
    [Theory]
    [InlineData(new byte[] { }, "")]
    [InlineData(new byte[] { 0 }, "")]
    [InlineData(new byte[] { 0b_1000, 4 }, "0001")]
    [InlineData(new byte[] { 0b_010_1000, 7 }, "0001010")]
    [InlineData(new byte[] { 0b_1111_1000, 0 }, "00011111")]
    [InlineData(new byte[] { 0b_1111_1000, 0b_101, 3 }, "00011111101")]
    [InlineData(new byte[] { 0, 0b_1111_1000, 0b_01, 2 }, "000000000001111110")]
    public void BitReader_ReadingBufferByBits_ShouldWorkCorrect(byte[] toRead, string expectedRead)
    {
      // Arrange
      using var memoryStream = new MemoryStream(toRead);
      var bitReader = new BitReader(memoryStream);

      // Act
      string acutalRead = "";

      while (!bitReader.IsEndOfStream)
      {
        acutalRead += bitReader.ReadBit().ToString();
      }

      // Assert
      Assert.Equal(expectedRead, acutalRead);
    }

    [Theory]
    [InlineData(new byte[] { }, 0)]
    [InlineData(new byte[] { 0 }, 0)]
    [InlineData(new byte[] { 0b_1000, 4 }, 4)]
    [InlineData(new byte[] { 0b_010_1000, 7 }, 7)]
    [InlineData(new byte[] { 0b_1111_1000, 0 }, 8)]
    [InlineData(new byte[] { 0b_1111_1000, 0b_101, 3 }, 11)]
    [InlineData(new byte[] { 0, 0b_1111_1000, 0b_01, 2 }, 18)]
    public void BitReader_ReadingEndOfStream_ShouldFail(byte[] toRead, int expectedSafeRead)
    {
      // Arrange
      using var memoryStream = new MemoryStream(toRead);
      var bitReader = new BitReader(memoryStream);

      // Act
      for (int i = 0; i < expectedSafeRead; i++)
      {
        bitReader.ReadBit();
      }

      // Assert
      Assert.Throws<EndOfStreamException>(() => bitReader.ReadBit());
    }

    [Theory]
    [InlineData(new byte[] { 0 }, new byte[] { })]
    [InlineData(new byte[] { 123, 0 }, new byte[] { 123 })]
    [InlineData(new byte[] { 42, 122, 22, 44, 0 }, new byte[] { 42, 122, 22, 44 })]
    [InlineData(new byte[] { 1, 2, 3, 4, 5, 6, 7, 0 }, new byte[] { 1, 2, 3, 4, 5, 6, 7 })]
    public void BitReader_ReadingEvenByte_ShouldWorkFine(byte[] toRead, byte[] expectedRead)
    {
      // Arrange
      using var memoryStream = new MemoryStream(toRead);
      var bitReader = new BitReader(memoryStream);

      // Act
      var actualRead = new List<byte>();
      while (!bitReader.IsEndOfStream)
      {
        actualRead.Add(bitReader.ReadByte());
      }

      // Assert
      Assert.Equal(expectedRead, actualRead);
    }

    [Theory]
    [InlineData(new byte[] { 123, 3 }, 0)]
    [InlineData(new byte[] { 42, 122, 22, 44, 7 }, 3)]
    [InlineData(new byte[] { 1, 2, 3, 4, 5, 6, 7, 1 }, 6)]
    public void BitReader_ReadingOddByte_ShouldFail(byte[] toRead, int expectedSafeRead)
    {
      // Arrange
      using var memoryStream = new MemoryStream(toRead);
      var bitReader = new BitReader(memoryStream);

      // Act
      for (int i = 0; i < expectedSafeRead; i++)
      {
        bitReader.ReadByte();
      }

      // Assert
      Assert.Throws<EndOfStreamException>(() => bitReader.ReadByte());
    }

    [Theory]
    [InlineData("")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("101")]
    [InlineData("00000000")]
    [InlineData("11111111")]
    [InlineData("01010111")]
    [InlineData("01010111010101110111")]
    [InlineData("0101011101010111010111")]
    [InlineData("010101110101011101010111")]
    [InlineData("0110101010100101110101011101010111")]
    public void BitReader_ReadingBitWritterResultShouldMatch(string expectedBitSequense)
    {
      // Arrange
      using var memoryStream = new MemoryStream();
      var bitWriter = new BitWriter(memoryStream);
      bitWriter.WriteBitSequenseAsString(expectedBitSequense);
      bitWriter.WriteTerminator();

      memoryStream.Seek(0, SeekOrigin.Begin);
      var bitReader = new BitReader(memoryStream);

      // Act
      string actualBitSequense = "";

      while (!bitReader.IsEndOfStream)
      {
        actualBitSequense += bitReader.ReadBit().ToString();
      }

      // Assert
      Assert.Equal(expectedBitSequense, actualBitSequense);
    }

  }
}
