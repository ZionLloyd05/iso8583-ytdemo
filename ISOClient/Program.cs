using System.Net.Sockets;
using System.Net;
using BIM_ISO8583.NET;
using System.Text;
using System.Diagnostics;

namespace ISOClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Set/Define Data
            //Message Type Identifier (Authorization Message Request)
            string MTI = "0100";

            //Primary Account No or Card No. [DE #2]
            string PAN = "4321123443211234";

            //Processing Code.[DE #3] (Purchase Transaction, Savings)
            string ProCode = "000000";

            //Transaction Amount.[DE #4] (X100) (ex: $150.00 = 15000)  or simply remove the decimal.
            string Amount = "15000";

            //Transmission Date and Time.[DE #7] (format: MMDDhhmmss)
            string DateTime = "0429104720";

            //System Trace Audit No.[DE #11] (456 or 000456)
            string STAN = "456";

            //Expiry date YYMM [DE #14]
            string ExpDate = "0205";

            //Merchant Type[DE #18]
            string MerchantType = "5399";

            //POS Entry Mode [DE #22] Swiped Card
            string POSEntryMode = "022";

            //POS Condition Code [DE #25]
            string POSConditionCode = "00";

            //Retrieval Reference Number [DE #35]
            string RetrivalReference = "206305000014";

            //Terminal ID. [DE #41]    
            string TID = "29110001";

            //Merchant ID. [DE #42]    
            string MID = "1001001";

            //Currency [DE #49]    
            string Currency = "840";


            //2.Create an object BIM-ISO8583.ISO8583
            ISO8583 iso8583 = new ISO8583();

            //3. Create Arrays
            string[] DE = new string[130];

            DE[0] = MTI;
            DE[1] = PAN;
            DE[3] = ProCode;
            DE[4] = Amount;
            DE[7] = DateTime;
            DE[11] = STAN;
            DE[14] = ExpDate;
            DE[18] = MerchantType;
            DE[22] = POSEntryMode;
            DE[25] = POSConditionCode;
            DE[37] = RetrivalReference;
            DE[41] = TID;
            DE[42] = MID;
            DE[49] = Currency;


            //5.Use "Build" method of object iso8583 to create a new  message.
            string NewISOmsg = iso8583.Build(DE, MTI);

            Console.WriteLine(NewISOmsg);
        }
    }
}