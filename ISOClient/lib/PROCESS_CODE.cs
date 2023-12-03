// Decompiled with JetBrains decompiler
// Type: ISOClient.PROCESS_CODE
// Assembly: ISOClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1a92fae21c820fb5
// MVID: 1FD8936C-EF25-464C-93F3-AA6C1534FA77
// Assembly location: C:\Users\Dell\Downloads\ISOClient.dll

namespace ISOClient
{
  public enum PROCESS_CODE
  {
    NormalPurchaseCA = 1000, // 0x000003E8
    NormalPurchaseSA = 2000, // 0x000007D0
    CashWithdrawalCA = 11000, // 0x00002AF8
    CashWithdrawalSA = 12000, // 0x00002EE0
    ModifyBlock = 170000, // 0x00029810
    BalanceInquiryCA = 311000, // 0x0004BED8
    BalanceInquirySA = 312000, // 0x0004C2C0
    MiniStatementCA = 381000, // 0x0005D048
    MiniStatementSA = 382000, // 0x0005D430
    FundTransferCA = 401010, // 0x00061E72
    FundTransferSA = 402010, // 0x0006225A
    FundTransferPayment = 502020, // 0x0007A904
    GeneralAccountInq = 821000, // 0x000C8708
    FullStatementCA = 931000, // 0x000E34B8
    FullStatementSA = 932000, // 0x000E38A0
    NormalAccountInq = 970000, // 0x000ECD10
        NetworkEchoTest = 831, // 0x0000033F
    }
}
