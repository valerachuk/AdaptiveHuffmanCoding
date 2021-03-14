using System.IO;
using System.Collections.Generic;

namespace AdaptiveHuffman.Core
{
  public class BitReader
  {
    private readonly Stream _stream;

    private List<byte> _readQueue = new();

    private int _currentByteReads = 0;
    private bool _isLastByte = false;

    private byte CurrentByte => _readQueue[0];
    private byte CurrentBitSize => _isLastByte && _readQueue[1] != 0 ? _readQueue[1] : 8;

    private bool CurrentBitReadDone => _currentByteReads >= CurrentBitSize;
    public bool IsEndOfStream => _readQueue.Count == 2 && CurrentBitReadDone || _readQueue.Count <= 1;

    public BitReader(Stream stream)
    {
      _stream = stream;

      for (int i = 0; i < 2; i++)
      {
        var currentReadFirstTwoBytes = _stream.ReadByte();
        if (currentReadFirstTwoBytes == -1)
        {
          return;
        }
        _readQueue.Add((byte)currentReadFirstTwoBytes);
      }

      var currentReadThirdByte = _stream.ReadByte();
      if (currentReadThirdByte == -1)
      {
        _isLastByte = true;
        return;
      }
      _readQueue.Add((byte)currentReadThirdByte);
    }

    private void MoveRead()
    {
      _readQueue.RemoveAt(0);
      _currentByteReads = 0;
      if (IsEndOfStream)
      {
        return;
      }

      var currentRead = _stream.ReadByte();
      if (currentRead == -1)
      {
        _isLastByte = true;
        return;
      }
      _readQueue.Add((byte)currentRead);
    }

    public byte ReadByte()
    {
      byte byteToRead = 0;
      for (int i = 0; i < 8; i++)
      {
        if (ReadBit() == 1)
        {
          byteToRead |= (byte)(1 << i);
        }
      }

      return byteToRead;
    }

    public int ReadBit()
    {
      if (IsEndOfStream)
      {
        throw new EndOfStreamException();
      }

      if (CurrentBitReadDone)
      {
        MoveRead();
      }

      var readBit = (CurrentByte & (1 << _currentByteReads)) == 0 ? 0 : 1;
      _currentByteReads++;

      return readBit;
    }
  }
}
