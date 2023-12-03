// Decompiled with JetBrains decompiler
// Type: ISOClient.Iso8583Msg
// Assembly: ISOClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1a92fae21c820fb5
// MVID: 1FD8936C-EF25-464C-93F3-AA6C1534FA77
// Assembly location: C:\Users\Dell\Downloads\ISOClient.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ISOClient
{
  public class Iso8583Msg
  {
    private const int MAX_LEN_ACCT_INQ = 478;
    private string m_mti;
    public List<Iso8583Msg.Iso8583Field> Iso8583flds;

    public Iso8583Msg()
    {
      this.Iso8583flds = new List<Iso8583Msg.Iso8583Field>(128);
      for (int index = 0; index < 128; ++index)
        this.Iso8583flds.Add((Iso8583Msg.Iso8583Field) null);
      this.Iso8583flds[2] = new Iso8583Msg.Iso8583Field("Primary Account No", "LLVAR", 19);
      this.Iso8583flds[3] = new Iso8583Msg.Iso8583Field("Process Code", "N", 6);
      this.Iso8583flds[4] = new Iso8583Msg.Iso8583Field("Txn Amount", "N", 16);
      this.Iso8583flds[5] = new Iso8583Msg.Iso8583Field("Stl Amount", "N", 16);
      this.Iso8583flds[7] = new Iso8583Msg.Iso8583Field("Transmission Date Time", "DTL", 10);
      this.Iso8583flds[11] = new Iso8583Msg.Iso8583Field("Trace Number", "N", 12);
      this.Iso8583flds[12] = new Iso8583Msg.Iso8583Field("Transmission Date Time", "DTL", 14);
      this.Iso8583flds[13] = new Iso8583Msg.Iso8583Field("Txn Local Date", "DTS", 8);
      this.Iso8583flds[14] = new Iso8583Msg.Iso8583Field("Stl Date", "DTS", 8);
      this.Iso8583flds[15] = new Iso8583Msg.Iso8583Field("Expiry Date", "DTS", 8);
      this.Iso8583flds[17] = new Iso8583Msg.Iso8583Field("Date Capture", "DTS", 8);
      this.Iso8583flds[18] = new Iso8583Msg.Iso8583Field("Merchant Type", "N", 3);
      this.Iso8583flds[24] = new Iso8583Msg.Iso8583Field("Function Code", "N", 3);
      this.Iso8583flds[28] = new Iso8583Msg.Iso8583Field("Txn Fee", "AMT", 8);
      this.Iso8583flds[29] = new Iso8583Msg.Iso8583Field("Stl Fee", "AMT", 8);
      this.Iso8583flds[30] = new Iso8583Msg.Iso8583Field("Original Amounts", "N", 32);
      this.Iso8583flds[31] = new Iso8583Msg.Iso8583Field("Stl Processing Fee", "AMT", 8);
      this.Iso8583flds[32] = new Iso8583Msg.Iso8583Field("Acquiring Inst ID", "LLVAR", 11);
      this.Iso8583flds[33] = new Iso8583Msg.Iso8583Field("Fowarder Inst ID", "LLVAR", 11);
      this.Iso8583flds[34] = new Iso8583Msg.Iso8583Field("Extended PAN", "LLVAR", 28);
      this.Iso8583flds[35] = new Iso8583Msg.Iso8583Field("Track 2", "LLVAR", 37);
      this.Iso8583flds[37] = new Iso8583Msg.Iso8583Field("Retrieval Ref No", "AN", 12);
      this.Iso8583flds[38] = new Iso8583Msg.Iso8583Field("Approval Code", "AN", 6);
      this.Iso8583flds[39] = new Iso8583Msg.Iso8583Field("Response Code", "AN", 3);
      this.Iso8583flds[41] = new Iso8583Msg.Iso8583Field("Terminal ID", "ANS", 16);
      this.Iso8583flds[42] = new Iso8583Msg.Iso8583Field("Card Acceptor ID", "ANS", 15);
      this.Iso8583flds[43] = new Iso8583Msg.Iso8583Field("Card Acceptor Name", "LLVAR", 50);
      this.Iso8583flds[46] = new Iso8583Msg.Iso8583Field("Amount Fees", "FEE", 300);
      this.Iso8583flds[48] = new Iso8583Msg.Iso8583Field("Additional Data", "LLLVAR", 999);
      this.Iso8583flds[49] = new Iso8583Msg.Iso8583Field("Txn Ccy Code", "LLLVAR", 3);
      this.Iso8583flds[50] = new Iso8583Msg.Iso8583Field("Stl Ccy Code", "LLLVAR", 3);
      this.Iso8583flds[54] = new Iso8583Msg.Iso8583Field("Additional Amounts", "LLVAR", 120);
      this.Iso8583flds[56] = new Iso8583Msg.Iso8583Field("Original Data Elements", "LLVAR", 43);
      this.Iso8583flds[59] = new Iso8583Msg.Iso8583Field("Transport data", "N", 3);
      this.Iso8583flds[62] = new Iso8583Msg.Iso8583Field("Cheque Details", "LLLVAR", 999);
      this.Iso8583flds[66] = new Iso8583Msg.Iso8583Field("Original Amount Fees", "FEE", 300);
      this.Iso8583flds[70] = new Iso8583Msg.Iso8583Field("Network Code", "AN", 3);
      this.Iso8583flds[93] = new Iso8583Msg.Iso8583Field("Field 93", "LLVAR", 11);
      this.Iso8583flds[94] = new Iso8583Msg.Iso8583Field("Field 94", "LLVAR", 11);
      this.Iso8583flds[102] = new Iso8583Msg.Iso8583Field("Account Iden 1", "LLVAR", 28);
      this.Iso8583flds[103] = new Iso8583Msg.Iso8583Field("Account Iden 2", "LLVAR", 28);
      this.Iso8583flds[123] = new Iso8583Msg.Iso8583Field("POS Data Code", "LLVAR", 15);
      this.Iso8583flds[124] = new Iso8583Msg.Iso8583Field("Terminal Type", "AN", 3);
      this.Iso8583flds[125] = new Iso8583Msg.Iso8583Field("Field 125", "LLLVAR", 999);
      this.Iso8583flds[126] = new Iso8583Msg.Iso8583Field("Field 126", "LLLVAR", 999);
      this.Iso8583flds[(int) sbyte.MaxValue] = new Iso8583Msg.Iso8583Field("Field 127", "LLLVAR", 999);
    }

    public List<Iso8583Msg.MiniStatement> GetMiniStatement()
    {
      List<Iso8583Msg.MiniStatement> miniStatement = (List<Iso8583Msg.MiniStatement>) null;
      if (this.Field_125 != null)
      {
        string field125 = this.Field_125;
        int length = 79;
        int capacity = (int) Math.Ceiling((double) field125.Length / (double) length);
        miniStatement = new List<Iso8583Msg.MiniStatement>(capacity);
        try
        {
          int startIndex = 0;
          for (int index = 0; index < capacity; ++index)
          {
            miniStatement.Add(new Iso8583Msg.MiniStatement(field125.Substring(startIndex, length)));
            startIndex += length;
          }
        }
        catch (Exception ex)
        {
          throw new Exception("Invalid Format" + ex.Message);
        }
      }
      return miniStatement;
    }

    public List<Iso8583Msg.FullStatement> GetFullStatement()
    {
      List<Iso8583Msg.FullStatement> fullStatement = (List<Iso8583Msg.FullStatement>) null;
      if (this.Field_125 != null && this.Field_125.Substring(1, 2) != "00")
      {
        string field125 = this.Field_125;
        string m_stmt = field125.Substring(3, field125.Length - 3);
        if (this.Field_126 != null)
          m_stmt += this.Field_126;
        if (this.Field_127 != null)
          m_stmt += this.Field_127;
        fullStatement = Iso8583Msg.FullStatement.Parse(m_stmt, this.AdditionalData);
      }
      return fullStatement;
    }

    public string MessageType
    {
      get => this.m_mti;
      set => this.m_mti = value;
    }

    public string PrimaryAccountNo
    {
      get => this.Iso8583flds[2].Value;
      set => this.Iso8583flds[2].Value = value;
    }

    public PROCESS_CODE ProcessCode
    {
      get => (PROCESS_CODE) Enum.Parse(typeof (PROCESS_CODE), this.Iso8583flds[3].Value);
      set => this.Iso8583flds[3].Value = Enum.Format(typeof (PROCESS_CODE), (object) value, "d");
    }

    public Decimal TransactionAmount
    {
      get => Convert.ToDecimal(this.Iso8583flds[4].Value);
      set => this.Iso8583flds[4].Value = double.Parse(value.ToString()).ToString();
    }

    public Decimal SettlementAmount
    {
      get => Convert.ToDecimal(this.Iso8583flds[5].Value);
      set => this.Iso8583flds[5].Value = value.ToString();
    }

    public DateTime TransmissionDateTime
    {
      get => Convert.ToDateTime(this.Iso8583flds[7].Value);
      set => this.Iso8583flds[7].Value = value.ToString("MMhhmmss");
    }

    public int TraceAuditNo
    {
      get => Convert.ToInt32(this.Iso8583flds[11].Value);
      set => this.Iso8583flds[11].Value = value.ToString("000000");
    }

    public DateTime LocalTxnDateTime
    {
      get => DateTime.ParseExact(this.Iso8583flds[12].Value, "yyyyMMddhhmmss", (IFormatProvider) null);
      set => this.Iso8583flds[12].Value = value.ToString("yyyyMMddhhmmss");
    }

    public DateTime LocalDateTime
    {
      get => DateTime.ParseExact(this.Iso8583flds[13].Value, "hhmmss", (IFormatProvider) null);
      set => this.Iso8583flds[13].Value = value.ToString("hhmmss");
    }

    public DateTime DateStl
    {
      get => DateTime.ParseExact(this.Iso8583flds[14].Value, "yyyyMMdd", (IFormatProvider) null);
      set => this.Iso8583flds[14].Value = value.ToString("yyyyMMdd");
    }

    public DateTime DateExp
    {
      get => DateTime.ParseExact(this.Iso8583flds[15].Value, "yyyyMMdd", (IFormatProvider) null);
      set => this.Iso8583flds[15].Value = value.ToString("yyyyMMdd");
    }

    public DateTime DateCapture
    {
      get => DateTime.ParseExact(this.Iso8583flds[17].Value, "yyyyMMdd", (IFormatProvider) null);
      set => this.Iso8583flds[17].Value = value.ToString("yyyyMMdd");
    }

    public int MerchantType
    {
      get => Convert.ToInt32(this.Iso8583flds[18].Value);
      set => this.Iso8583flds[18].Value = Convert.ToString(value);
    }

    public int PosEntryMode
    {
      get => Convert.ToInt32(this.Iso8583flds[22].Value);
      set => this.Iso8583flds[22].Value = Convert.ToString(value);
    }

    public int CardSeqNo
    {
      get => Convert.ToInt32(this.Iso8583flds[13].Value);
      set => this.Iso8583flds[13].Value = Convert.ToString(value);
    }

    public FUNCTION_CODE FunctionCode
    {
      get => (FUNCTION_CODE) Enum.Parse(typeof (FUNCTION_CODE), this.Iso8583flds[24].Value);
      set => this.Iso8583flds[24].Value = Enum.Format(typeof (FUNCTION_CODE), (object) value, "d");
    }

    public int PosCondCode
    {
      get => Convert.ToInt32(this.Iso8583flds[25].Value);
      set => this.Iso8583flds[25].Value = Convert.ToString(value);
    }

    public int PosPinCapCode
    {
      get => Convert.ToInt32(this.Iso8583flds[26].Value);
      set => this.Iso8583flds[26].Value = Convert.ToString(value);
    }

    public Decimal TransactionFee
    {
      get => Convert.ToDecimal(this.Iso8583flds[28].Value);
      set => this.Iso8583flds[28].Value = Convert.ToString(value);
    }

    public Decimal SettlementFee
    {
      get => Convert.ToDecimal(this.Iso8583flds[29].Value);
      set => this.Iso8583flds[29].Value = Convert.ToString(value);
    }

    public Decimal OriginalAmounts
    {
      get => Convert.ToDecimal(this.Iso8583flds[30].Value);
      set => this.Iso8583flds[30].Value = double.Parse(value.ToString()).ToString();
    }

    public Decimal SettlementProcFee
    {
      get => Convert.ToDecimal(this.Iso8583flds[31].Value);
      set => this.Iso8583flds[31].Value = Convert.ToString(value);
    }

    public string AcquirerInstId
    {
      get => this.Iso8583flds[32].Value;
      set => this.Iso8583flds[32].Value = Convert.ToString(value);
    }

    public string FowarderInstCode
    {
      get => this.Iso8583flds[33].Value;
      set => this.Iso8583flds[33].Value = Convert.ToString(value);
    }

    public string ExtendedPAN
    {
      get => this.Iso8583flds[34].Value;
      set => this.Iso8583flds[34].Value = Convert.ToString(value);
    }

    public string Track2Data
    {
      get => this.Iso8583flds[35].Value;
      set => this.Iso8583flds[35].Value = Convert.ToString(value);
    }

    public int RetrievalRefNo
    {
      get => Convert.ToInt32(this.Iso8583flds[37].Value);
      set => this.Iso8583flds[37].Value = Convert.ToString(value);
    }

    public string ApprovalCode
    {
      get => this.Iso8583flds[38].Value;
      set => this.Iso8583flds[38].Value = value;
    }

    public int ResponseCode
    {
      get => Convert.ToInt32(this.Iso8583flds[39].Value);
      set => this.Iso8583flds[39].Value = Convert.ToString(value);
    }

    public string CardAcceptorTerminalId
    {
      get => this.Iso8583flds[41].Value;
      set => this.Iso8583flds[41].Value = Convert.ToString(value);
    }

    public string CardAcceptorId
    {
      get => this.Iso8583flds[42].Value;
      set => this.Iso8583flds[42].Value = Convert.ToString(value);
    }

    public string CardAcceptorNameLoc
    {
      get => this.Iso8583flds[43].Value;
      set => this.Iso8583flds[43].Value = Convert.ToString(value);
    }

    public string AdditionalRspData
    {
      get => this.Iso8583flds[44].Value;
      set => this.Iso8583flds[44].Value = Convert.ToString(value);
    }

    public Iso8583Msg.AmountFees AmtFees
    {
      get => new Iso8583Msg.AmountFees(this.Iso8583flds[46].Value);
      set => this.Iso8583flds[46].Value = value.ToString();
    }

    public string AdditionalData
    {
      get => this.Iso8583flds[48].Value;
      set => this.Iso8583flds[48].Value = Convert.ToString(value);
    }

    public string TxnCurrencyCode
    {
      get => this.Iso8583flds[49].Value;
      set => this.Iso8583flds[49].Value = Convert.ToString(value);
    }

    public string StlCurrencyCode
    {
      get => this.Iso8583flds[50].Value;
      set => this.Iso8583flds[50].Value = Convert.ToString(value);
    }

    public string AdditionalAmounts
    {
      get => this.Iso8583flds[54].Value;
      set => this.Iso8583flds[54].Value = Convert.ToString(value);
    }

    public string OriginalDataElements
    {
      get => this.Iso8583flds[56].Value;
      set => this.Iso8583flds[56].Value = value.ToString();
    }

    public string TransportDataCode
    {
      get => this.Iso8583flds[59].Value;
      set => this.Iso8583flds[59].Value = Convert.ToString(value);
    }

    public string Field_62
    {
      get => this.Iso8583flds[62].Value;
      set => this.Iso8583flds[62].Value = Convert.ToString(value);
    }

    public Iso8583Msg.AmountFees OriginalFees
    {
      get => new Iso8583Msg.AmountFees(this.Iso8583flds[66].Value);
      set => this.Iso8583flds[66].Value = value.ToString();
    }

    public string NetworkMgmtCode
    {
      get => this.Iso8583flds[70].Value;
      set => this.Iso8583flds[70].Value = Convert.ToString(value);
    }

    public string Field_93
    {
      get => this.Iso8583flds[93].Value;
      set => this.Iso8583flds[93].Value = value;
    }

    public string Field_94
    {
      get => this.Iso8583flds[94].Value;
      set => this.Iso8583flds[94].Value = value;
    }

    public string ReplacementAmount
    {
      get => this.Iso8583flds[95].Value;
      set => this.Iso8583flds[95].Value = Convert.ToString(value);
    }

    public string AccountIden1
    {
      get => this.Iso8583flds[102].Value;
      set => this.Iso8583flds[102].Value = Convert.ToString(value);
    }

    public string AccountIden2
    {
      get => this.Iso8583flds[103].Value;
      set => this.Iso8583flds[103].Value = Convert.ToString(value);
    }

    public string PosDataCode
    {
      get => this.Iso8583flds[123].Value;
      set => this.Iso8583flds[123].Value = value;
    }

    public string TerminalType
    {
      get => this.Iso8583flds[124].Value;
      set => this.Iso8583flds[124].Value = value;
    }

    public string Field_125
    {
      get => this.Iso8583flds[125].Value;
      set => this.Iso8583flds[125].Value = value;
    }

    public string Field_126
    {
      get => this.Iso8583flds[126].Value;
      set => this.Iso8583flds[126].Value = value;
    }

    public string Field_127
    {
      get => this.Iso8583flds[(int) sbyte.MaxValue].Value;
      set => this.Iso8583flds[(int) sbyte.MaxValue].Value = value;
    }

    private byte[] Encode()
    {
      string s = "";
      BitArray avb_flds_bits = new BitArray(128, false);
      string str1 = "";
      try
      {
        avb_flds_bits.Set(0, true);
        avb_flds_bits.Set(1, true);
        for (int index = 2; index < 128; ++index)
        {
          Iso8583Msg.Iso8583Field iso8583fld = this.Iso8583flds[index];
          if (iso8583fld != null && iso8583fld.Value != null)
          {
            avb_flds_bits.Set(index, true);
            if (iso8583fld.Value.Length > iso8583fld.Length)
              iso8583fld.Value = iso8583fld.Value.Substring(1, iso8583fld.Length);
            int length1;
            switch (iso8583fld.Format)
            {
              case "LLVAR":
                length1 = iso8583fld.Value.Length;
                string str2 = length1.ToString();
                length1 = iso8583fld.Length;
                int length2 = length1.ToString().Length;
                str1 = str2.PadLeft(length2, '0') + iso8583fld.Value;
                break;
              case "LLLVAR":
                length1 = iso8583fld.Value.Length;
                str1 = length1.ToString("000") + iso8583fld.Value;
                break;
              case "FEE":
                length1 = iso8583fld.Value.Length;
                str1 = length1.ToString("000") + iso8583fld.Value;
                break;
              case "AN":
                str1 = iso8583fld.Value.PadLeft(iso8583fld.Length, '0');
                break;
              case "ANS":
                str1 = iso8583fld.Value.PadRight(iso8583fld.Length, '0');
                break;
              case "N":
                str1 = iso8583fld.Value.PadLeft(iso8583fld.Length, '0').Substring(0, iso8583fld.Length);
                break;
              case "DTL":
                str1 = iso8583fld.Value;
                break;
              case "DTS":
                str1 = iso8583fld.Value;
                break;
              default:
                Console.Write("Format " + iso8583fld.Format + " not defined for idx:" + index.ToString());
                break;
            }
            s += str1;
          }
        }
        ushort length = (ushort) (s.Length + 22);
        byte[] destinationArray = new byte[(int) length];
        byte[] bytes1 = Encoding.ASCII.GetBytes(this.m_mti);
        byte[] bitmap = this.GenerateBitmap(avb_flds_bits);
        byte[] bytes2 = Encoding.ASCII.GetBytes(s);
        int num = (int) length - 2;
        if (num > 256)
        {
          destinationArray[0] = (byte) Math.Floor((double) (num / 256));
          num %= 256;
        }
        destinationArray[1] = (byte) num;
        destinationArray[2] = (byte) 49;
        Array.Copy((Array) bytes1, 0, (Array) destinationArray, 3, bytes1.Length);
        Array.Copy((Array) bitmap, 0, (Array) destinationArray, 6, bitmap.Length);
        Array.Copy((Array) bytes2, 0, (Array) destinationArray, 22, bytes2.Length);
        return destinationArray;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Decode(byte[] iso_msg)
    {
      int num = 128;
      try
      {
        if ((int) iso_msg[0] * 256 + (int) iso_msg[1] < 22)
          throw new Exception("Msg Decode Failed");
        byte[] numArray = new byte[16];
        Array.Copy((Array) iso_msg, 6, (Array) numArray, 0, 16);
        BitArray availableFields = this.GetAvailableFields(numArray);
        int startIndex1 = 0;
        string str = Encoding.ASCII.GetString(iso_msg, 22, iso_msg.Length - 22);
        for (int index1 = 0; index1 < num - 1; ++index1)
        {
          if (availableFields.Get(index1))
          {
            int index2 = index1 + 1;
            if (this.Iso8583flds[index2] != null)
            {
              switch (this.Iso8583flds[index2].Format)
              {
                case "LLVAR":
                  int length1 = int.Parse(str.Substring(startIndex1, 2));
                  int startIndex2 = startIndex1 + 2;
                  this.Iso8583flds[index2].Value = str.Substring(startIndex2, length1);
                  startIndex1 = startIndex2 + length1;
                  break;
                case "LLLVAR":
                  int length2 = int.Parse(str.Substring(startIndex1, 3));
                  int startIndex3 = startIndex1 + 3;
                  this.Iso8583flds[index2].Value = str.Substring(startIndex3, length2);
                  startIndex1 = startIndex3 + length2;
                  break;
                case "FEE":
                  int length3 = int.Parse(str.Substring(startIndex1, 3));
                  int startIndex4 = startIndex1 + 3;
                  this.Iso8583flds[index2].Value = str.Substring(startIndex4, length3);
                  startIndex1 = startIndex4 + length3;
                  break;
                case "AN":
                  int length4 = this.Iso8583flds[index2].Length;
                  this.Iso8583flds[index2].Value = str.Substring(startIndex1, length4);
                  startIndex1 += length4;
                  break;
                case "ANS":
                  int length5 = this.Iso8583flds[index2].Length;
                  this.Iso8583flds[index2].Value = str.Substring(startIndex1, length5);
                  startIndex1 += length5;
                  break;
                case "N":
                  int length6 = this.Iso8583flds[index2].Length;
                  this.Iso8583flds[index2].Value = str.Substring(startIndex1, length6);
                  startIndex1 += length6;
                  break;
                case "DTL":
                  int length7 = this.Iso8583flds[index2].Length;
                  this.Iso8583flds[index2].Value = str.Substring(startIndex1, length7);
                  startIndex1 += length7;
                  break;
                case "DTS":
                  int length8 = this.Iso8583flds[index2].Length;
                  this.Iso8583flds[index2].Value = str.Substring(startIndex1, length8);
                  startIndex1 += length8;
                  break;
                default:
                  Console.Write("Format " + this.Iso8583flds[index2].Format + " not defined for idx:" + index1.ToString());
                  break;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private byte BinToByte(string bin_val)
    {
      byte num = 0;
      int y = bin_val.Length - 1;
      int startIndex = 0;
      while (y >= 0)
      {
        num += Convert.ToByte(Convert.ToInt32(bin_val.Substring(startIndex, 1)) * Convert.ToInt32(Math.Pow(2.0, (double) y)));
        --y;
        ++startIndex;
      }
      return num;
    }

    private byte[] GenerateBitmap(BitArray avb_flds_bits)
    {
      int length1 = 16;
      int length2 = 8;
      string str1 = (string) null;
      byte[] bitmap = new byte[length1];
      if (avb_flds_bits.Length % length1 == 0)
      {
        for (int index = 0; index < avb_flds_bits.Length; ++index)
          str1 += avb_flds_bits.Get(index) ? "1" : "0";
        string str2 = str1.Substring(1, str1.Length - 1) + "0";
        int index1 = 0;
        for (int startIndex = 0; startIndex < str2.Length; startIndex += length2)
        {
          string bin_val = str2.Substring(startIndex, length2);
          bitmap[index1] = this.BinToByte(bin_val);
          ++index1;
        }
      }
      return bitmap;
    }

    private BitArray GetAvailableFields(byte[] bitmap)
    {
      string str = (string) null;
      byte length = 128;
      for (int index = 0; index < bitmap.Length; ++index)
        str += Convert.ToString(bitmap[index], 2).PadLeft(8, '0');
      BitArray availableFields = new BitArray((int) length, false);
      for (int index = 0; index < (int) length; ++index)
        availableFields.Set(index, str.Substring(index, 1) == "1");
      return availableFields;
    }

    public static string GetFEPResponseDesc(int rsp_cod)
    {
      string fepResponseDesc;
      switch (rsp_cod)
      {
        case 0:
          fepResponseDesc = "Transaction Successful";
          break;
        case 1:
          fepResponseDesc = "Refer Card Issuer";
          break;
        case 2:
          fepResponseDesc = "Refer Card Issuer Special Condition";
          break;
        case 3:
          fepResponseDesc = "Invalid Merchant";
          break;
        case 4:
          fepResponseDesc = "Pick up";
          break;
        case 5:
          fepResponseDesc = "Do not Honour";
          break;
        case 6:
          fepResponseDesc = "Error";
          break;
        case 7:
          fepResponseDesc = "Pick-up Special Condition";
          break;
        case 8:
          fepResponseDesc = "Honour with identification";
          break;
        case 9:
          fepResponseDesc = "Request in Progress";
          break;
        case 10:
          fepResponseDesc = "Approved for part number";
          break;
        case 11:
          fepResponseDesc = "Approved (VIP)";
          break;
        case 13:
          fepResponseDesc = "Invalid Amount";
          break;
        case 14:
          fepResponseDesc = "Invalid Card Number";
          break;
        case 15:
          fepResponseDesc = "No Such Issuer";
          break;
        case 16:
          fepResponseDesc = "Approved, Update track 3";
          break;
        case 17:
          fepResponseDesc = "Customer Cancellation";
          break;
        case 18:
          fepResponseDesc = "Customer Dispute";
          break;
        case 19:
          fepResponseDesc = "Reenter Transaction";
          break;
        case 20:
          fepResponseDesc = "Invalid Response";
          break;
        case 21:
          fepResponseDesc = "No Action Taken";
          break;
        case 22:
          fepResponseDesc = "Suspected Malfunction";
          break;
        case 23:
          fepResponseDesc = "Unacceptable Transaction Fee";
          break;
        case 24:
          fepResponseDesc = "File Update not supported";
          break;
        case 25:
          fepResponseDesc = "Unable to locate rec on file";
          break;
        case 26:
          fepResponseDesc = "Duplicate file record";
          break;
        case 27:
          fepResponseDesc = "File Update edit error";
          break;
        case 28:
          fepResponseDesc = "File Update File Locked Out";
          break;
        case 29:
          fepResponseDesc = "File Update Error";
          break;
        case 30:
          fepResponseDesc = "Invalid Format";
          break;
        case 31:
          fepResponseDesc = "Bank Not Supported";
          break;
        case 32:
          fepResponseDesc = "Completed Partially";
          break;
        case 33:
          fepResponseDesc = "Card Expired";
          break;
        case 34:
          fepResponseDesc = "Suspected Fraud (Pickup)";
          break;
        case 35:
          fepResponseDesc = "Card Acceptor Contact Acquirer (P)";
          break;
        case 36:
          fepResponseDesc = "Restricted Card (Pickup)";
          break;
        case 37:
          fepResponseDesc = "Card Acptr Contact Acquirer";
          break;
        case 38:
          fepResponseDesc = "PIN Retries Exceeded (Pickup)";
          break;
        case 39:
          fepResponseDesc = "No Credit Account";
          break;
        case 40:
          fepResponseDesc = "Invalid Function Requested";
          break;
        case 41:
          fepResponseDesc = "Lost Card";
          break;
        case 42:
          fepResponseDesc = "No Universal Account";
          break;
        case 43:
          fepResponseDesc = "Stolen Card";
          break;
        case 44:
          fepResponseDesc = "Invalid Investment Account";
          break;
        case 51:
          fepResponseDesc = "Not Sufficient Funds";
          break;
        case 52:
          fepResponseDesc = "No Chequeing Account";
          break;
        case 53:
          fepResponseDesc = "No Savings Account";
          break;
        case 54:
          fepResponseDesc = "Expired Card";
          break;
        case 55:
          fepResponseDesc = "Invalid PIN";
          break;
        case 56:
          fepResponseDesc = "No Card Record";
          break;
        case 57:
          fepResponseDesc = "Txn not permitted to Cardholder";
          break;
        case 58:
          fepResponseDesc = "Txn not permitted to Terminal";
          break;
        case 59:
          fepResponseDesc = "Suspected Fraud (Decline)";
          break;
        case 60:
          fepResponseDesc = "Card Accep. Contact Acq.(Decline)";
          break;
        case 61:
          fepResponseDesc = "Withdrawal Amt Exceeds Limit";
          break;
        case 62:
          fepResponseDesc = "Restricted Card(Decline)";
          break;
        case 63:
          fepResponseDesc = "Security Violation";
          break;
        case 64:
          fepResponseDesc = "Invalid Original Amount";
          break;
        case 65:
          fepResponseDesc = "Exceeds Withdrawal Freq Limit";
          break;
        case 66:
          fepResponseDesc = "Card Acptr Contacct Acquirer";
          break;
        case 67:
          fepResponseDesc = "Hard Capture";
          break;
        case 68:
          fepResponseDesc = "Response Arrived Too Late";
          break;
        case 75:
          fepResponseDesc = "PIN Retries Exceeded (Decline)";
          break;
        case 90:
          fepResponseDesc = "Cutoff in progress";
          break;
        case 91:
          fepResponseDesc = "Issuer Inoperative";
          break;
        case 92:
          fepResponseDesc = "Forwarder Inoperative";
          break;
        case 93:
          fepResponseDesc = "Txn cannot be completed";
          break;
        case 94:
          fepResponseDesc = "Duplicate Trasmission";
          break;
        case 95:
          fepResponseDesc = "Reconcilation Error";
          break;
        case 96:
          fepResponseDesc = "System Malfunction";
          break;
        case 98:
          fepResponseDesc = "Duplicate reversal send";
          break;
        case 99:
          fepResponseDesc = "Duplicate transaction send";
          break;
        default:
          fepResponseDesc = "Msg Not Set";
          break;
      }
      return fepResponseDesc;
    }

    public void Send(string machine_ip, int port)
    {
      try
      {
        byte[] numArray1 = this.Encode();
        byte[] buffer = new byte[2];
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.BeginConnect(machine_ip, port, (AsyncCallback) null, (object) null).AsyncWaitHandle.WaitOne(2000, true);
        socket.SendTimeout = 5000;
        socket.ReceiveTimeout = 30000;
        Trace.WriteLine("*****Core Banking Request **** length:" + numArray1.Length.ToString());
        this.TextDumpMsg(numArray1);
        socket.Send(numArray1, 0, numArray1.Length, SocketFlags.None);
        Thread.Sleep(200);
        if (socket.Receive(buffer, 0, 2, SocketFlags.None) <= 0)
        {
          Trace.WriteLine("Message header incomplete...Socket Receive failed to get all header bytes..");
          throw new Exception("EndOfStreamException()");
        }
        int num1 = (int) buffer[0] * 256 + (int) buffer[1];
        byte[] numArray2 = new byte[num1 + 2];
        int offset = 2;
        int size = num1;
        while (size > 0)
        {
          int num2 = socket.Receive(numArray2, offset, size, SocketFlags.None);
          if (num2 <= 0)
          {
            Trace.WriteLine("Message data incomplete...Socket Receive failed to get all data bytes..");
            throw new Exception("EndOfStreamException()");
          }
          size -= num2;
          offset += num2;
        }
        socket.Close();
        numArray2[0] = buffer[0];
        numArray2[1] = buffer[1];
        this.Decode(numArray2);
        Trace.WriteLine("***Core Banking Response **** length:" + numArray2.Length.ToString());
        this.TextDumpMsg(numArray2);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message + " " + ex.StackTrace);
        throw ex;
      }
    }

    private void HexDumpMsg(byte[] iso_msg)
    {
      try
      {
        int count1 = 16;
        Trace.WriteLine("HexDumpMsg Size= " + iso_msg.Length.ToString());
        int index = 0;
        for (; index < iso_msg.Length; ++index)
        {
          int num = index + 1;
          Trace.Write(iso_msg[index].ToString("X").PadLeft(2, '0') + " ");
          if (num % count1 == 0)
          {
            Trace.Write(Encoding.ASCII.GetString(iso_msg, num - count1, count1).Replace(char.MinValue, '.'));
            Trace.WriteLine("");
          }
          else if (num == iso_msg.Length)
          {
            int count2 = num % count1;
            string message = Encoding.ASCII.GetString(iso_msg, num - count2, count2).Replace(char.MinValue, '.');
            Trace.Write(new string(' ', 3 * (count1 - count2)));
            Trace.Write(message);
            Trace.WriteLine("");
            Trace.Flush();
          }
        }
      }
      catch (Exception ex)
      {
       // EventLog.WriteEntry("ISO8583::TextDumpMsg()", ex.Message + ex.StackTrace, EventLogEntryType.Error);
        Console.WriteLine(ex.Message);
      }
    }

    private void TextDumpMsg(byte[] iso_msg)
    {
      try
      {
        Trace.WriteLine("TextDumpMsg Size= " + iso_msg.Length.ToString());
        Iso8583Msg iso8583Msg = new Iso8583Msg();
        iso8583Msg.Decode(iso_msg);
        for (int index = 0; index < iso8583Msg.Iso8583flds.Count; ++index)
        {
          if (iso8583Msg.Iso8583flds[index] != null && iso8583Msg.Iso8583flds[index].Value != null)
            Trace.WriteLine(index.ToString("000") + " " + iso8583Msg.Iso8583flds[index].Value);
        }
        Trace.Flush();
      }
      catch (Exception ex)
      {
       // EventLog.WriteEntry("ISO8583::TextDumpMsg()", ex.Message + ex.StackTrace, EventLogEntryType.Error);
        Console.WriteLine(ex.Message);
      }
    }

    public static string GetResponseMsg(string msg_code)
    {
      string responseMsg;
      switch (msg_code)
      {
        case "000":
          responseMsg = "Approved";
          break;
        case "111":
          responseMsg = "Invalid Scheme type";
          break;
        case "114":
          responseMsg = "Invalid account number";
          break;
        case "115":
          responseMsg = "Func not supported";
          break;
        case "116":
          responseMsg = "Insufficient funds";
          break;
        case "119":
          responseMsg = "Txn not permitted";
          break;
        case "121":
          responseMsg = "wdr limit exceeded";
          break;
        case "163":
          responseMsg = "Inv Chq Status";
          break;
        case "180":
          responseMsg = "Tfr Limit Exceeded";
          break;
        case "181":
          responseMsg = "Chqs in different books";
          break;
        case "182":
          responseMsg = "Not all chqs could be stopped";
          break;
        case "183":
          responseMsg = "chqs not issued to this account";
          break;
        case "184":
          responseMsg = "Account is closed";
          break;
        case "185":
          responseMsg = "Inv Txn Currency or Amount";
          break;
        case "186":
          responseMsg = "Block does not exist";
          break;
        case "187":
          responseMsg = "Cheque Stopped";
          break;
        case "188":
          responseMsg = "Invalid Rate Currency Combination";
          break;
        case "189":
          responseMsg = "Cheque Book Already Issued";
          break;
        case "190":
          responseMsg = "DD Already Paid";
          break;
        case "800":
          responseMsg = "Network message was accepted";
          break;
        case "902":
          responseMsg = "Invalid Txn";
          break;
        case "904":
          responseMsg = "Format Error";
          break;
        case "906":
          responseMsg = "Cut-over in progress";
          break;
        case "907":
          responseMsg = "Card issuer inoperative";
          break;
        case "909":
          responseMsg = "Malfunction";
          break;
        case "911":
          responseMsg = "Card issuer timed out";
          break;
        case "913":
          responseMsg = "Duplicate transmission";
          break;
        default:
          responseMsg = "Unknown Msg Code";
          break;
      }
      return responseMsg;
    }

    public static string GetISOCcyCode(string ccy_code)
    {
      string isoCcyCode = "566";
      switch (ccy_code)
      {
        case "NGN":
          isoCcyCode = "566";
          break;
        case "USD":
          isoCcyCode = "840";
          break;
        case "GBP":
          isoCcyCode = "826";
          break;
      }
      return isoCcyCode;
    }

    public static Decimal ConvertToISOAmount(Decimal dec_amt) => dec_amt * 100M;

    public static Decimal ConvertToDecimalAmount(Decimal iso_amt) => iso_amt / 100M;

    public class AmountFees
    {
      public string FeeType;
      public string FeeCcyCode;
      public char FeeTxnType;
      public Decimal FeeAmount;
      public Decimal FeeConvRate;
      public char ReconTxnType;
      public Decimal ReconAmount;
      public string ReconCcyCode;

      public AmountFees(
        string f_type,
        string f_ccy_cod,
        char f_txn_typ,
        Decimal f_amt,
        Decimal f_conv_rate,
        char r_txn_type,
        Decimal r_amt,
        string r_ccy_cod)
      {
        this.FeeType = f_type;
        this.FeeCcyCode = f_ccy_cod;
        this.FeeTxnType = f_txn_typ;
        this.FeeAmount = f_amt * 100M;
        this.FeeConvRate = f_conv_rate;
        this.ReconTxnType = r_txn_type;
        this.ReconAmount = r_amt * 100M;
        this.ReconCcyCode = r_ccy_cod;
      }

      public override string ToString() => this.FeeType.PadLeft(2, '0') + this.FeeCcyCode.PadLeft(3, 'x') + this.FeeTxnType.ToString() + this.FeeAmount.ToString("0000000000000000") + this.FeeConvRate.ToString("00000000") + this.ReconTxnType.ToString() + this.ReconAmount.ToString("0000000000000000") + this.ReconCcyCode;

      public AmountFees(string fee_val)
      {
        try
        {
          this.FeeType = fee_val.Substring(0, 2);
          this.FeeCcyCode = fee_val.Substring(2, 3);
          this.FeeTxnType = char.Parse(fee_val.Substring(5, 1));
          this.FeeAmount = Decimal.Parse(fee_val.Substring(6, 16));
          this.FeeConvRate = Decimal.Parse(fee_val.Substring(22, 8));
          this.ReconTxnType = char.Parse(fee_val.Substring(30, 1));
          this.ReconAmount = Decimal.Parse(fee_val.Substring(31, 16));
          this.ReconCcyCode = fee_val.Substring(47, 3);
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }
    }

    public class Balance
    {
      public string Ledger;
      public string Available;
      public string Float;
      public string FFD;
      public string UserDefined;
      public string CcyCode;

      public Balance(string sBalance)
      {
        try
        {
          this.Ledger = Convert.ToString(double.Parse(sBalance.Substring(0, 17)) / 100.0);
          this.Available = Convert.ToString(double.Parse(sBalance.Substring(17, 17)) / 100.0);
          this.Float = Convert.ToString(double.Parse(sBalance.Substring(34, 17)) / 100.0);
          this.FFD = Convert.ToString(double.Parse(sBalance.Substring(51, 17)) / 100.0);
          this.UserDefined = Convert.ToString(double.Parse(sBalance.Substring(68, 17)) / 100.0);
          this.CcyCode = sBalance.Substring(85, 3);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message + "Invalid format in Balance()");
        }
      }
    }

    public class MiniStatement
    {
      public string Date;
      public string InstrID;
      public string TranCode;
      public string TranPart;
      public string DrCr;
      public string Amount;

      public MiniStatement(string m_stmt_item)
      {
        this.Date = m_stmt_item.Substring(0, 8).Trim();
        this.InstrID = m_stmt_item.Substring(8, 8).Trim();
        this.TranCode = m_stmt_item.Substring(16, 5).Trim();
        this.TranPart = m_stmt_item.Substring(21, 40).Trim();
        this.DrCr = m_stmt_item.Substring(61, 1).Trim();
        this.Amount = m_stmt_item.Substring(62, 17).Trim();
      }
    }

    public class FullStatement
    {
      public string TranDate;
      public string TranID;
      public string TranSerialNo;
      public string TranType;
      public string TranSubType;
      public string DrCr;
      public string ValueDate;
      public string Amount;
      public string Particulars;
      public string PostDate;
      public string InstrNumber;
      public string BalanceBd;
      public string AvailBalance;
      public string UnclrBalance;
      public string LedgerBalance;

      public static List<Iso8583Msg.FullStatement> Parse(string m_stmt, string balance)
      {
        int num1 = 139;
        int capacity = (int) Math.Ceiling((double) m_stmt.Length / (double) num1);
        List<Iso8583Msg.FullStatement> fullStatementList = new List<Iso8583Msg.FullStatement>(capacity);
        try
        {
          int startIndex1 = 0;
          double num2 = 0.0;
          Iso8583Msg.FullStatement fullStatement = (Iso8583Msg.FullStatement) null;
          Iso8583Msg.Balance balance1 = new Iso8583Msg.Balance(balance);
          for (int index = 0; index < capacity; ++index)
          {
            string str = m_stmt.Substring(startIndex1, num1 - 17) + balance1.Ledger + balance1.Available + balance1.Float;
            fullStatement = new Iso8583Msg.FullStatement();
            fullStatement.TranDate = str.Substring(0, 8).Trim();
            fullStatement.TranID = str.Substring(8, 9).Trim();
            fullStatement.TranSerialNo = str.Substring(17, 4).Trim();
            fullStatement.TranType = str.Substring(21, 1).Trim();
            fullStatement.TranSubType = str.Substring(22, 2).Trim();
            fullStatement.DrCr = str.Substring(24, 1).Trim();
            fullStatement.ValueDate = str.Substring(25, 8).Trim();
            fullStatement.Amount = str.Substring(33, 17).Trim();
            fullStatement.Particulars = str.Substring(50, 50).Trim();
            fullStatement.PostDate = str.Substring(100, 14).Trim();
            fullStatement.InstrNumber = str.Substring(114, 8).Trim();
            fullStatement.LedgerBalance = str.Substring(122, 17).Trim();
            fullStatement.AvailBalance = str.Substring(139, 17).Trim();
            fullStatement.BalanceBd = "00000000000000000";
            int startIndex2 = fullStatement.AvailBalance.IndexOf('-');
            if (startIndex2 > 0)
              fullStatement.AvailBalance = fullStatement.AvailBalance.Substring(startIndex2, fullStatement.AvailBalance.Length - startIndex2);
            fullStatement.UnclrBalance = str.Substring(156, 17).Trim();
            fullStatement.Particulars = fullStatement.Particulars.Replace('\'', ' ');
            fullStatement.Particulars = fullStatement.Particulars.Replace(',', ' ');
            fullStatement.Particulars = fullStatement.Particulars.Replace('\\', ' ');
            fullStatement.Particulars = fullStatement.Particulars.Replace('"', ' ');
            fullStatementList.Add(fullStatement);
            startIndex1 += num1;
          }
          fullStatementList.Sort((IComparer<Iso8583Msg.FullStatement>) new Iso8583Msg.FullStatement.StmtComparer());
          if (fullStatementList.Count > 0)
            num2 = double.Parse(fullStatementList[0].AvailBalance) / 100.0;
          for (int index = 0; index < fullStatementList.Count; ++index)
          {
            fullStatementList[index].BalanceBd = num2.ToString("00000000000000000");
            if (fullStatement.DrCr == "D")
              num2 += double.Parse(fullStatementList[index].Amount);
            else
              num2 -= double.Parse(fullStatementList[index].Amount);
          }
          return fullStatementList;
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message);
        }
      }

      public class StmtComparer : Comparer<Iso8583Msg.FullStatement>
      {
        public override int Compare(Iso8583Msg.FullStatement x, Iso8583Msg.FullStatement y)
        {
          long num1 = long.Parse(x.PostDate);
          long num2 = long.Parse(y.PostDate);
          if (num1 > num2)
            return -1;
          return num1 == num2 ? 0 : 1;
        }
      }
    }

    public class Account
    {
      public string CustomerId;
      public string CustomerName;
      public string AcctName;
      public string DateOpened;
      public string SchemeCode;
      public string AcctType;
      public string AcctStatus;
      public string ModeOperation;
      public string JointHolder1;
      public string JointHolder2;
      public string JointHolder3;
      public string GLSubHeadCode;
      public string SolId;
      public string DrawingPower;
      public string LienAmount;
      public Iso8583Msg.Balance Balance;

      public Account(string acct_str, string acct_bal)
      {
        this.CustomerId = acct_str.Length == 478 ? acct_str.Substring(0, 9) : throw new Exception("Invalid Account string length");
        this.CustomerName = acct_str.Substring(9, 80);
        this.AcctName = acct_str.Substring(89, 80);
        this.DateOpened = acct_str.Substring(169, 8);
        this.SchemeCode = acct_str.Substring(177, 5);
        this.AcctType = acct_str.Substring(182, 3);
        this.AcctStatus = acct_str.Substring(185, 1);
        this.ModeOperation = acct_str.Substring(186, 5);
        this.JointHolder1 = acct_str.Substring(191, 80);
        this.JointHolder2 = acct_str.Substring(271, 80);
        this.JointHolder3 = acct_str.Substring(351, 80);
        this.GLSubHeadCode = acct_str.Substring(431, 5);
        this.SolId = acct_str.Substring(436, 8);
        this.DrawingPower = acct_str.Substring(444, 17);
        this.LienAmount = acct_str.Substring(461, 17);
        this.Balance = new Iso8583Msg.Balance(acct_bal);
      }
    }

    public class Iso8583Field
    {
      private string m_name;
      private string m_fmt;
      private int m_len;
      private string m_value = (string) null;

      public Iso8583Field(string Name, string Format, int Len)
      {
        this.m_name = Name;
        this.m_fmt = Format;
        this.m_len = Len;
      }

      public string Value
      {
        get => this.m_value;
        set => this.m_value = value;
      }

      public string Name => this.m_name;

      public string Format => this.m_fmt;

      public int Length => this.m_len;

      public override string ToString() => this.m_value;
    }
  }
}
