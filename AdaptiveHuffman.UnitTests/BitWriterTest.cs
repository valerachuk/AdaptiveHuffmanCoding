using Xunit;
using AdaptiveHuffman.Core;
using System.IO;

namespace AdaptiveHuffman.UnitTests
{
  public class BitWriterTest
  {
    [Theory]
    [InlineData(new object[] { }, new byte[] { 0 })]
    [InlineData(new object[] { "0010111", 0b_0110_0001, "", "", "1", "0" }, new byte[] { 0b_1111_0100, 0b_1011_0000, 0b_0, 1 })]
    [InlineData(new object[] { "", "01", 0b_1111_1111, "01", "1", "00", 0b_0000_1111 }, new byte[] { 0b_1111_1110, 0b_1001_1011, 0b_000_0111, 7 })]
    [InlineData(new object[] { 222, 18 }, new byte[] { 222, 18, 0 })]
    [InlineData(new object[] { "10111" }, new byte[] { 0b_1_1101, 5 })]
    public void BitWriter_RandomWrites_ShouldWorkCorrect(object[] toWrite, byte[] expectedBuffer)
    {
      // Arrange
      using var memoryStream = new MemoryStream();
      var bitWriter = new BitWriter(memoryStream);

      // Act
      foreach (var item in toWrite)
      {
        if (item is string)
        {
          bitWriter.WriteBitSequenseAsString((string)item);
        }
        else
        {
          bitWriter.WriteByte((byte)(int)item);
        }
      }

      bitWriter.WriteTerminator();

      // Assert
      var actualBuffer = memoryStream.ToArray();
      Assert.Equal(expectedBuffer, actualBuffer);
    }
  }
}