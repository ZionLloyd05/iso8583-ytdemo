// Decompiled with JetBrains decompiler
// Type: ISOClient.Iso8583Connect
// Assembly: ISOClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1a92fae21c820fb5
// MVID: 1FD8936C-EF25-464C-93F3-AA6C1534FA77
// Assembly location: C:\Users\Dell\Downloads\ISOClient.dll

using System;
using System.Collections.Generic;

namespace ISOClient
{
  public class Iso8583Connect
  {
    private string m_hostIp;
    private int m_port;
    private int m_stan;

    public Iso8583Connect(string hostIp, int port)
    {
      this.m_hostIp = hostIp;
      this.m_port = port;
      this.m_stan = 0;
    }

    public int GetNextSTAN() => ++this.m_stan;

    private bool Echo()
    {
      bool flag = false;
      Iso8583Msg iso8583Msg = new Iso8583Msg();
      int nextStan = this.GetNextSTAN();
      iso8583Msg.FunctionCode = FUNCTION_CODE.NetworkEchoTest;
      iso8583Msg.MessageType = "804";
      iso8583Msg.TraceAuditNo = nextStan;
      iso8583Msg.LocalTxnDateTime = DateTime.Now;
      iso8583Msg.PosDataCode = "FEP";
      iso8583Msg.Send(this.m_hostIp, this.m_port);
      if (iso8583Msg.ResponseCode.ToString("000") == "800")
        flag = true;
      return flag;
    }

    public Iso8583Msg ModifyBlockFunds(
      string cod_acct_no,
      Decimal hold_amt,
      bool block,
      int tran_ref_no,
      string lien_ref,
      string lien_remarks)
    {
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(tran_ref_no, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.ModifyBlock;
        iso8583Msg.AccountIden1 = cod_acct_no;
        iso8583Msg.TransactionAmount = hold_amt * 100M;
        string str = block ? "P" : "N";
        if (lien_ref.Length > 18)
          lien_ref = lien_ref.Substring(0, 18);
        lien_ref = lien_ref.PadRight(27, ' ');
        iso8583Msg.Field_125 = str + lien_ref + "01320991231" + lien_remarks;
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        return iso8583Msg;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public Iso8583Msg.Balance GetBalance(string cod_acct_no, out string rsp_code)
    {
      Iso8583Msg.Balance balance = (Iso8583Msg.Balance) null;
      rsp_code = "907";
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(0, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.BalanceInquiryCA;
        iso8583Msg.AccountIden1 = cod_acct_no;
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        if (iso8583Msg.ResponseCode.ToString("000") == "000")
          balance = new Iso8583Msg.Balance(iso8583Msg.AdditionalData);
        rsp_code = iso8583Msg.ResponseCode.ToString("000");
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return balance;
    }

    public Iso8583Msg MakeWithdrawal(
      string cod_acct_no,
      Decimal txn_amt,
      Iso8583Msg.AmountFees txn_fee,
      out string rsp_msg,
      int tran_ref_no)
    {
      rsp_msg = (string) null;
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(tran_ref_no, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.CashWithdrawalCA;
        iso8583Msg.TransactionAmount = Iso8583Msg.ConvertToISOAmount(txn_amt);
        iso8583Msg.AccountIden1 = cod_acct_no;
        iso8583Msg.AmtFees = txn_fee;
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        rsp_msg = Iso8583Msg.GetResponseMsg(iso8583Msg.ResponseCode.ToString("000"));
        return iso8583Msg;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public Iso8583Msg MakePurchase(
      string cod_acct_no,
      Decimal txn_amt,
      Iso8583Msg.AmountFees txn_fee,
      out string rsp_msg,
      int tran_ref_no)
    {
      rsp_msg = (string) null;
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(tran_ref_no, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.NormalPurchaseCA;
        iso8583Msg.TransactionAmount = Iso8583Msg.ConvertToISOAmount(txn_amt);
        iso8583Msg.AccountIden1 = cod_acct_no;
        iso8583Msg.AmtFees = txn_fee;
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        rsp_msg = Iso8583Msg.GetResponseMsg(iso8583Msg.ResponseCode.ToString("000"));
        return iso8583Msg;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public Iso8583Msg TransferFundRev(
      int sys_trace_no,
      string src_acct_no,
      string dest_acct_no,
      Decimal txn_amt,
      DateTime tran_date,
      Decimal charge_amt,
      string tran_particulars,
      string tran_rmks,
      string incomeAccount,
      out string rsp_msg)
    {
      rsp_msg = (string) null;
      Iso8583Msg iso8583Msg;
      try
      {
        iso8583Msg = this.InitIso8583Msg(sys_trace_no, FUNCTION_CODE.ReversalAdvice);
        iso8583Msg.ProcessCode = PROCESS_CODE.FundTransferCA;
        iso8583Msg.FunctionCode = FUNCTION_CODE.ReversalRequest;
        iso8583Msg.AccountIden2 = dest_acct_no;
        iso8583Msg.AccountIden1 = src_acct_no;
        iso8583Msg.CardAcceptorNameLoc = tran_particulars;
        iso8583Msg.Field_125 = tran_rmks + ";" + incomeAccount;
        iso8583Msg.TransactionAmount = Iso8583Msg.ConvertToISOAmount(txn_amt);
        iso8583Msg.OriginalAmounts = Iso8583Msg.ConvertToISOAmount(0M);
        iso8583Msg.LocalTxnDateTime = tran_date;
        iso8583Msg.DateCapture = tran_date;
        iso8583Msg.OriginalDataElements = "1200" + sys_trace_no.ToString().PadLeft(12, '0') + tran_date.ToString("yyyyMMddhhmmss") + "11" + "639138".PadLeft(11, '0');
        iso8583Msg.AmtFees = new Iso8583Msg.AmountFees("70", "NGN", 'C', charge_amt, 1M, 'D', 0M, "NGN");
        iso8583Msg.OriginalFees = iso8583Msg.AmtFees;
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        rsp_msg = Iso8583Msg.GetResponseMsg(iso8583Msg.ResponseCode.ToString("000"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return iso8583Msg;
    }

    public Iso8583Msg WithdrawalReversal(
      int sys_trace_no,
      string src_acct_no,
      Decimal txn_amt,
      DateTime tran_date,
      Decimal charge_amt,
      string tran_particulars,
      out string rsp_msg)
    {
      rsp_msg = (string) null;
      Iso8583Msg iso8583Msg;
      try
      {
        iso8583Msg = this.InitIso8583Msg(sys_trace_no, FUNCTION_CODE.ReversalAdvice);
        iso8583Msg.ProcessCode = PROCESS_CODE.CashWithdrawalCA;
        iso8583Msg.FunctionCode = FUNCTION_CODE.ReversalRequest;
        iso8583Msg.AccountIden1 = src_acct_no;
        iso8583Msg.CardAcceptorNameLoc = tran_particulars;
        iso8583Msg.TransactionAmount = Iso8583Msg.ConvertToISOAmount(txn_amt);
        iso8583Msg.OriginalAmounts = Iso8583Msg.ConvertToISOAmount(0M);
        iso8583Msg.LocalTxnDateTime = tran_date;
        iso8583Msg.DateCapture = tran_date;
        iso8583Msg.OriginalDataElements = "1200" + sys_trace_no.ToString().PadLeft(12, '0') + tran_date.ToString("yyyyMMddhhmmss") + "11" + "639138".PadLeft(11, '0');
        iso8583Msg.AmtFees = new Iso8583Msg.AmountFees("70", "NGN", 'C', charge_amt, 1M, 'D', 0M, "NGN");
        iso8583Msg.OriginalFees = iso8583Msg.AmtFees;
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        rsp_msg = Iso8583Msg.GetResponseMsg(iso8583Msg.ResponseCode.ToString("000"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return iso8583Msg;
    }

    public Iso8583Msg TransferFund(
      string cod_acct_no_src,
      string cod_acct_no_dest,
      Decimal txn_amt,
      DateTime tran_date,
      Iso8583Msg.AmountFees txn_fee,
      string tran_particulars,
      string tran_remarks,
      string commAccount,
      int tran_ref_no,
      out string rsp_msg)
    {
      rsp_msg = (string) null;
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(tran_ref_no, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.FundTransferCA;
        iso8583Msg.TransactionAmount = Iso8583Msg.ConvertToISOAmount(txn_amt);
        iso8583Msg.AccountIden1 = cod_acct_no_src;
        iso8583Msg.AccountIden2 = cod_acct_no_dest;
        iso8583Msg.AmtFees = txn_fee;
        iso8583Msg.LocalTxnDateTime = tran_date;
        iso8583Msg.DateCapture = tran_date;
        iso8583Msg.CardAcceptorNameLoc = tran_particulars;
        iso8583Msg.Field_125 = tran_remarks + ";" + commAccount;
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        rsp_msg = Iso8583Msg.GetResponseMsg(iso8583Msg.ResponseCode.ToString("000"));
        return iso8583Msg;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public Iso8583Msg TransferFund(
      string cod_acct_no_src,
      string cod_acct_no_dest,
      Decimal txn_amt,
      DateTime tran_date,
      Iso8583Msg.AmountFees txn_fee,
      string tran_particular1,
      string tran_particular2,
      string tran_remarks,
      string commAccount,
      int tran_ref_no,
      out string rsp_msg)
    {
      rsp_msg = (string) null;
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(tran_ref_no, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.FundTransferCA;
        iso8583Msg.TransactionAmount = Iso8583Msg.ConvertToISOAmount(txn_amt);
        iso8583Msg.AccountIden1 = cod_acct_no_src;
        iso8583Msg.AccountIden2 = cod_acct_no_dest;
        iso8583Msg.AmtFees = txn_fee;
        iso8583Msg.LocalTxnDateTime = tran_date;
        iso8583Msg.DateCapture = tran_date;
        iso8583Msg.CardAcceptorNameLoc = tran_particular1;
        iso8583Msg.Field_125 = tran_remarks + ";" + commAccount;
        iso8583Msg.Field_127 = tran_particular2;
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        rsp_msg = Iso8583Msg.GetResponseMsg(iso8583Msg.ResponseCode.ToString("000"));
        return iso8583Msg;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public Iso8583Msg TransferFundByCheque(
      string cod_acct_no_src,
      string cod_acct_no_dest,
      Decimal txn_amt,
      DateTime tran_date,
      Iso8583Msg.AmountFees txn_fee,
      string tran_particulars,
      string tran_remarks,
      string commAccount,
      int tran_ref_no,
      string InstrNum,
      out string rsp_msg)
    {
      rsp_msg = (string) null;
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(tran_ref_no, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.FundTransferCA;
        iso8583Msg.TransactionAmount = Iso8583Msg.ConvertToISOAmount(txn_amt);
        iso8583Msg.AccountIden1 = cod_acct_no_src;
        iso8583Msg.AccountIden2 = cod_acct_no_dest;
        iso8583Msg.AmtFees = txn_fee;
        iso8583Msg.LocalTxnDateTime = tran_date;
        iso8583Msg.DateCapture = tran_date;
        iso8583Msg.CardAcceptorNameLoc = tran_particulars;
        iso8583Msg.Field_125 = tran_remarks + ";" + commAccount;
        InstrNum = InstrNum.PadLeft(8);
        iso8583Msg.Field_62 = InstrNum + "050013  CHQ";
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        rsp_msg = Iso8583Msg.GetResponseMsg(iso8583Msg.ResponseCode.ToString("000"));
        return iso8583Msg;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public Iso8583Msg TransferFundByCheque(
      string cod_acct_no_src,
      string cod_acct_no_dest,
      Decimal txn_amt,
      DateTime tran_date,
      Iso8583Msg.AmountFees txn_fee,
      string tran_particular1,
      string tran_particular2,
      string tran_remarks,
      string commAccount,
      int tran_ref_no,
      string InstrNum,
      out string rsp_msg)
    {
      rsp_msg = (string) null;
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(tran_ref_no, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.FundTransferCA;
        iso8583Msg.TransactionAmount = Iso8583Msg.ConvertToISOAmount(txn_amt);
        iso8583Msg.AccountIden1 = cod_acct_no_src;
        iso8583Msg.AccountIden2 = cod_acct_no_dest;
        iso8583Msg.AmtFees = txn_fee;
        iso8583Msg.LocalTxnDateTime = tran_date;
        iso8583Msg.DateCapture = tran_date;
        iso8583Msg.CardAcceptorNameLoc = tran_particular1;
        iso8583Msg.Field_125 = tran_remarks + ";" + commAccount;
        iso8583Msg.Field_127 = tran_particular2;
        InstrNum = InstrNum.PadLeft(8);
        iso8583Msg.Field_62 = InstrNum + "050013  CHQ";
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        rsp_msg = Iso8583Msg.GetResponseMsg(iso8583Msg.ResponseCode.ToString("000"));
        return iso8583Msg;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public Iso8583Msg TransferFundByChequeRev(
      string cod_acct_no_src,
      string cod_acct_no_dest,
      Decimal txn_amt,
      DateTime tran_date,
      Iso8583Msg.AmountFees txn_fee,
      string tran_particulars,
      string tran_remarks,
      int tran_ref_no,
      string InstrNum,
      out string rsp_msg)
    {
      rsp_msg = (string) null;
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(tran_ref_no, FUNCTION_CODE.ReversalAdvice);
        iso8583Msg.FunctionCode = FUNCTION_CODE.ReversalRequest;
        iso8583Msg.ProcessCode = PROCESS_CODE.FundTransferCA;
        iso8583Msg.TransactionAmount = Iso8583Msg.ConvertToISOAmount(txn_amt);
        iso8583Msg.OriginalAmounts = Iso8583Msg.ConvertToISOAmount(0M);
        iso8583Msg.AccountIden1 = cod_acct_no_src;
        iso8583Msg.AccountIden2 = cod_acct_no_dest;
        iso8583Msg.AmtFees = txn_fee;
        iso8583Msg.LocalTxnDateTime = tran_date;
        iso8583Msg.DateCapture = tran_date;
        iso8583Msg.CardAcceptorNameLoc = tran_particulars;
        iso8583Msg.Field_125 = tran_remarks;
        iso8583Msg.TxnCurrencyCode = "566";
        iso8583Msg.StlCurrencyCode = "566";
        iso8583Msg.Field_62 = " " + InstrNum + "  CHQ";
        iso8583Msg.OriginalDataElements = "1200" + tran_ref_no.ToString().PadLeft(12, '0') + tran_date.ToString("yyyyMMddhhmmss") + "11" + "639138".PadLeft(11, '0');
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        rsp_msg = Iso8583Msg.GetResponseMsg(iso8583Msg.ResponseCode.ToString("000"));
        return iso8583Msg;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public List<Iso8583Msg.MiniStatement> GetMiniStatement(string cod_acct_no, out string rsp_msg)
    {
      List<Iso8583Msg.MiniStatement> miniStatement = (List<Iso8583Msg.MiniStatement>) null;
      rsp_msg = (string) null;
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(0, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.MiniStatementSA;
        iso8583Msg.AccountIden1 = cod_acct_no;
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        ref string local = ref rsp_msg;
        int responseCode = iso8583Msg.ResponseCode;
        string responseMsg = Iso8583Msg.GetResponseMsg(responseCode.ToString("000"));
        local = responseMsg;
        responseCode = iso8583Msg.ResponseCode;
        if (responseCode.ToString("000") == "000")
          miniStatement = iso8583Msg.GetMiniStatement();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return miniStatement;
    }

    public List<Iso8583Msg.FullStatement> GetFullStatement(
      string cod_acct_no,
      DateTime dat_start,
      DateTime dat_end,
      out string rsp_msg)
    {
      List<Iso8583Msg.FullStatement> fullStatement = (List<Iso8583Msg.FullStatement>) null;
      rsp_msg = (string) null;
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(0, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.FullStatementCA;
        iso8583Msg.AccountIden1 = cod_acct_no;
        iso8583Msg.Field_125 = dat_start.ToString("yyyyMMdd") + dat_end.ToString("yyyyMMdd") + "20AB";
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        ref string local = ref rsp_msg;
        int responseCode = iso8583Msg.ResponseCode;
        string responseMsg = Iso8583Msg.GetResponseMsg(responseCode.ToString("000"));
        local = responseMsg;
        responseCode = iso8583Msg.ResponseCode;
        if (responseCode.ToString("000") == "000")
          fullStatement = iso8583Msg.GetFullStatement();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return fullStatement;
    }

    public Iso8583Msg.Account AccountInquiry(string cod_acct_no, out string rsp_msg)
    {
      rsp_msg = (string) null;
      Iso8583Msg.Account account = (Iso8583Msg.Account) null;
      try
      {
        Iso8583Msg iso8583Msg = this.InitIso8583Msg(0, FUNCTION_CODE.NormalRequest);
        iso8583Msg.ProcessCode = PROCESS_CODE.GeneralAccountInq;
        iso8583Msg.TransactionAmount = Iso8583Msg.ConvertToISOAmount(0M);
        iso8583Msg.AccountIden1 = cod_acct_no;
        iso8583Msg.Send(this.m_hostIp, this.m_port);
        if (rsp_msg != null)
        {
          ref string local = ref rsp_msg;
          int responseCode = iso8583Msg.ResponseCode;
          string responseMsg = Iso8583Msg.GetResponseMsg(responseCode.ToString("000"));
          local = responseMsg;
          responseCode = iso8583Msg.ResponseCode;
          if (responseCode.ToString("000") == "000")
            account = new Iso8583Msg.Account(iso8583Msg.Field_125, iso8583Msg.AdditionalData);
        }
        return account;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public Iso8583Msg InitIso8583Msg(int Stan, FUNCTION_CODE func_code)
    {
      Iso8583Msg iso8583Msg = new Iso8583Msg()
      {
        FunctionCode = func_code
      };
      iso8583Msg.MessageType = ((int) iso8583Msg.FunctionCode).ToString();
      iso8583Msg.RetrievalRefNo = Stan;
      iso8583Msg.TraceAuditNo = Stan;
      iso8583Msg.AcquirerInstId = "639138";
      iso8583Msg.PrimaryAccountNo = "6391380000000000000";
      iso8583Msg.TransactionAmount = 0M;
      iso8583Msg.LocalTxnDateTime = DateTime.Now;
      iso8583Msg.DateCapture = DateTime.Now;
      iso8583Msg.FowarderInstCode = "111111";
      iso8583Msg.CardAcceptorTerminalId = "000000003INP0000";
      iso8583Msg.CardAcceptorId = "NIBBSPAY0000001";
      iso8583Msg.CardAcceptorNameLoc = "NIBSS Pay     LANG";
      iso8583Msg.TxnCurrencyCode = "566";
      iso8583Msg.PosDataCode = "NFP";
      iso8583Msg.TerminalType = "WEB";
      iso8583Msg.Field_93 = "12345678901";
      iso8583Msg.Field_94 = "12345612345";
      iso8583Msg.AmtFees = new Iso8583Msg.AmountFees("70", "NGN", 'C', 0M, 1M, 'D', 0M, "NGN");
      return iso8583Msg;
    }
  }
}
