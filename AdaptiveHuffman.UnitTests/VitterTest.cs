using Xunit;
using AdaptiveHuffman.Core;
using System.IO;
using System.Collections.Generic;

namespace AdaptiveHuffman.UnitTests
{
  public class VitterTest
  {
    [Theory]
    [InlineData(new byte[] { 1, 1, 1, 1 }, new byte[] { 0b_0000_0001, 0b_111, 3 })]
    [InlineData(new byte[] { 1, 1, 2, 2 }, new byte[] { 0b_0000_0001, 0b_0000_1001, 0b_1000, 4 })]
    public void Vitter_Compressing_ShouldWorkCorrect(byte[] toCompress, byte[] expectedCompressedData)
    {
      // Arrange
      using var memoryStreamToCompress = new MemoryStream(toCompress);
      using var memoryStreamCompressedData = new MemoryStream();

      // Act
      Vitter.Compress(memoryStreamToCompress, memoryStreamCompressedData);

      // Assert
      Assert.Equal(expectedCompressedData, memoryStreamCompressedData.ToArray());
    }

    [Theory]
    [InlineData(new byte[] { 0b_0000_0001, 0b_111, 3 }, new byte[] { 1, 1, 1, 1 })]
    [InlineData(new byte[] { 0b_0000_0001, 0b_0000_1001, 0b_1000, 4 }, new byte[] { 1, 1, 2, 2 })]
    public void Vitter_Decompressing_ShouldWorkCorrect(byte[] toDecompress, byte[] expectedDecompressedData)
    {
      // Arrange
      using var memoryStreamToDecompress = new MemoryStream(toDecompress);
      using var memoryStreamDecompressedData = new MemoryStream();

      // Act
      Vitter.Decompress(memoryStreamToDecompress, memoryStreamDecompressedData);

      // Assert
      Assert.Equal(expectedDecompressedData, memoryStreamDecompressedData.ToArray());
    }

    [Theory]
    [InlineData(new byte[] { 1, 1, 5 })]
    [InlineData(new byte[] { 1, 1, 1, 1 })]
    [InlineData(new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 })]
    [InlineData(new byte[] { 1, 1, 2, 1, 1, 2, 3, 4, 4, 1, 2, 3, 3, 1, 1, 2, 2, 1, 3, 1, 1, 1 })]
    [InlineData(new byte[] { 1, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7 })]
    public void Vitter_CompressingAndDecompressing_ShouldMath(byte[] binaryData)
    {
      // Arrange
      using var memoryStreamToCompress = new MemoryStream(binaryData);
      using var memoryStreamCompressedData = new MemoryStream();
      using var memoryStreamToDecompress = new MemoryStream();

      // Act
      Vitter.Compress(memoryStreamToCompress, memoryStreamCompressedData);
      memoryStreamCompressedData.Seek(0, SeekOrigin.Begin);
      Vitter.Decompress(memoryStreamCompressedData, memoryStreamToDecompress);

      // Assert
      Assert.Equal(binaryData, memoryStreamToDecompress.ToArray());
    }
  }
}