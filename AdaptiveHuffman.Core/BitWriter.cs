using System;
using System.IO;

namespace AdaptiveHuffman.Core
{
  public class BitWriter : IDisposable
  {
    private byte _tempByte = 0;
    private int _currentTempByteWrites = 0;
    private Stream _stream;

    public BitWriter(Stream stream)
    {
      _stream = stream;
    }

    private void WriteTempByte()
    {
      _stream.WriteByte(_tempByte);
      _currentTempByteWrites = 0;
      _tempByte = 0;
    }

    private void WriteBit(int value)
    {
      if (value != 0)
      {
        _tempByte |= (byte)(1 << _currentTempByteWrites);
      }
      _currentTempByteWrites++;

      if (_currentTempByteWrites >= 8)
      {
        WriteTempByte();
      }
    }

    public void WriteByte(byte value)
    {
      for (int i = 0; i < 8; i++)
      {
        WriteBit(value & (1 << i));
      }
    }

    public void WriteBitSequenseAsString(string bits)
    {
      foreach (var bit in bits)
      {
        WriteBit(bit == '0' ? 0 : 1);
      }
    }

    public void Dispose()
    {
      if (_currentTempByteWrites != 0)
      {
        _stream.WriteByte(_tempByte);
      }
      _stream.WriteByte((byte)_currentTempByteWrites);
    }
  }
}
