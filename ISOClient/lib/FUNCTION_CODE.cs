// Decompiled with JetBrains decompiler
// Type: ISOClient.FUNCTION_CODE
// Assembly: ISOClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1a92fae21c820fb5
// MVID: 1FD8936C-EF25-464C-93F3-AA6C1534FA77
// Assembly location: C:\Users\Dell\Downloads\ISOClient.dll

namespace ISOClient
{
  public enum FUNCTION_CODE
  {
    NormalRequest = 200, // 0x000000C8
    NormalResponse = 210, // 0x000000D2
    NormalAdvice = 220, // 0x000000DC
    NormalRepeat = 221, // 0x000000DD
    ReversalRequest = 400, // 0x00000190
    ReversalAdvice = 420, // 0x000001A4
    ReversalRepeat = 421, // 0x000001A5
    ReversalResponse = 430, // 0x000001AE
    NetworkEchoTest = 831, // 0x0000033F
  }
}
