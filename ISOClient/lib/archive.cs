//using System.Net.Sockets;
//using System.Net;
//using BIM_ISO8583.NET;
//using System.Text;
//using System.Diagnostics;

//namespace ISOClient
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            //bool result = false;
//            Iso8583Msg iso8583Msg = new Iso8583Msg();
//            //int nextSTAN = 1;
//            iso8583Msg.MessageType = "0100";
//            //iso8583Msg.ProcessCode = PROCESS_CODE.NetworkEchoTest;
//            //iso8583Msg.TraceAuditNo = nextSTAN;
//            //iso8583Msg.LocalTxnDateTime = System.DateTime.Now;
//            //iso8583Msg.PrimaryAccountNo = "233";
//            //iso8583Msg.Send("127.0.0.1", 5050);
//            //bool result = false;
//            //if (iso8583Msg.ResponseCode.ToString("000") == "00")
//            //{
//            //    result = true;
//            //}

//            //Console.WriteLine(result);

//            //1. Set/Define Data
//            //Message Type Identifier (Financial Message Request)
//            string MTI = "0200";
//            //Primary Account No or Card No. [DE #2]
//            string PAN = "60197105032103634";
//            //Processing Code.[DE #3] (Purchase Transaction, Savings)
//            string ProCode = "001000";
//            //Transaction Amount.[DE #4] (X100) (ex: $150.75 = 15075 or
//            // 000000015075)  or simply remove the decimal.
//            string Amount = "15075";
//            //Transmission Date and Time.[DE #7] (format: MMDDhhmmss)
//            string DateTime = "0429104720";
//            //System Trace Audit No.[DE #11] (456 or 000456)
//            string STAN = "456";
//            //Terminal ID. [DE #41]    
//            string TID = "44449999";
//            //Point of Service Entry Mode. [DE #22] (02 - Magnetic Stripe)
//            string POSEM = "02";


//            //2.Create an object BIM-ISO8583.ISO8583
//            ISO8583 iso8583 = new ISO8583();

//            //3. Create Arrays
//            string[] DE = new string[130];

//            //4. Assign corresponding data to each array
//            //   Ex: ISO8583 Data Element No.2 (PAN) shall assign to newly created array, DE[2];

//            //DE[3] = "31";
//            //DE[7] = DateTime;
//            //DE[11] = STAN;
//            //DE[12] = "104522";
//            //DE[13] = "0703";
//            //DE[32] = "627821";
//            //DE[37] = "000000041798";
//            //DE[39] = "00";
//            //DE[102] = "000000041798";
//            //DE[48] = Amount;


//            DE[3] = "31";
//            DE[7] = DateTime;
//            DE[11] = STAN;
//            DE[12] = "104522";
//            DE[13] = "0703";
//            DE[32] = "627821";
//            DE[37] = "000000041798";
//            DE[102] = "000000041798";


//            //5.Use "Build" method of object iso8583 to create a new  message.
//            string NewISOmsg = iso8583.Build(DE, MTI);

//            Console.WriteLine(NewISOmsg);
//            // Send the sign on request to the remote server.  Exceptions will happen here
//            // which need to be dealt with
//            //var rsp = NetworkSend("127.0.0.1", 5050, NewISOmsg);

//            //if (rsp[39] == "00")
//            //{
//            //    Console.WriteLine("You are sign on now!");
//            //}
//            //else
//            //{

//            //    Console.WriteLine("Something went wrong!");
//            //}
//        }

//        /*  public void Send(string machine_ip, int port)
//          {
//              try
//              {
//                  byte[] numArray1 = this.Encode();
//                  byte[] buffer = new byte[2];
//                  Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//                  socket.BeginConnect(machine_ip, port, (AsyncCallback)null, (object)null).AsyncWaitHandle.WaitOne(2000, true);
//                  socket.SendTimeout = 5000;
//                  socket.ReceiveTimeout = 30000;
//                  Trace.WriteLine("*****Core Banking Request **** length:" + numArray1.Length.ToString());
//                  this.TextDumpMsg(numArray1);
//                  socket.Send(numArray1, 0, numArray1.Length, SocketFlags.None);
//                  Thread.Sleep(200);
//                  if (socket.Receive(buffer, 0, 2, SocketFlags.None) <= 0)
//                  {
//                      Trace.WriteLine("Message header incomplete...Socket Receive failed to get all header bytes..");
//                      throw new Exception("EndOfStreamException()");
//                  }
//                  int num1 = (int)buffer[0] * 256 + (int)buffer[1];
//                  byte[] numArray2 = new byte[num1 + 2];
//                  int offset = 2;
//                  int size = num1;
//                  while (size > 0)
//                  {
//                      int num2 = socket.Receive(numArray2, offset, size, SocketFlags.None);
//                      if (num2 <= 0)
//                      {
//                          Trace.WriteLine("Message data incomplete...Socket Receive failed to get all data bytes..");
//                          throw new Exception("EndOfStreamException()");
//                      }
//                      size -= num2;
//                      offset += num2;
//                  }
//                  socket.Close();
//                  numArray2[0] = buffer[0];
//                  numArray2[1] = buffer[1];
//                  this.Decode(numArray2);
//                  Trace.WriteLine("***Core Banking Response **** length:" + numArray2.Length.ToString());
//                  this.TextDumpMsg(numArray2);
//              }
//              catch (Exception ex)
//              {
//                  Trace.WriteLine(ex.Message + " " + ex.StackTrace);
//                  throw ex;
//              }
//          }*/

//        private static string[] NetworkSend(string ip, int port, string isoMsg)
//        {

//            // We're going to use a 2 byte header to indicate the message length
//            // which is not inclusive of the length of the header
//            //var msgData = new byte[isoMsg.PackedLength + 2];

//            //// The two byte header is a base 256 number so we can set the first two bytes in the data 
//            //// to send like this
//            //msgData[0] = (byte)(msg.PackedLength % 256);
//            //msgData[1] = (byte)(msg.PackedLength / 256);

//            //// Copy the message into msgData
//            //Array.Copy(msg.ToMsg(), 0, msgData, 2, msg.PackedLength);

//            var msgData = Encoding.UTF8.GetBytes(isoMsg);

//            // Now send the message.  We're going to behave like a terminal, which is 
//            // connect, send, receive response, disconnect
//            var client = new TcpClient();
//            var endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
//            client.Connect(endPoint);
//            var stream = client.GetStream();

//            // Send the packed message
//            stream.Write(msgData, 0, msgData.Length);

//            // Receive the response

//            // First we need to get the length of the message out of the socket
//            var lengthHeader = new byte[2];
//            stream.Read(lengthHeader, 0, 2);

//            // Work out the length of the incoming message
//            var rspLength = lengthHeader[0] * 256 + lengthHeader[1];
//            var rspData = new byte[rspLength];

//            // Read the bytes off the network stream
//            stream.Read(rspData, 0, rspLength);

//            // Close the network stream and client
//            stream.Close();
//            client.Close();

//            // Parse the data into an Iso8583 message and return it

//            var rspMsg = Unpack(rspData);
//            return rspMsg;
//        }

//        private static string[] Unpack(byte[]? resData)
//        {
//            ISO8583 iso8583 = new ISO8583();

//            string encodedString = Encoding.UTF8.GetString(resData);

//            string[] DE;

//            DE = iso8583.Parse(encodedString);

//            return DE;
//        }
//    }
//}