using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Client_TeamOP;


namespace ProgrammTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]        //Method to Connect to Server C1/3
        public void isConnected()
        {            
            Connector connector = new Connector();
            connector.connectToServer("127.0.0.1", 666);
            Assert.IsTrue(connector.isConnected());
        }

        [TestMethod]        //
        public void isDisconnected()
        {
            Connector connector = new Connector();
            connector.connectToServer("127.0.0.1", 666);
            connector.disconnectFromServer();
            Assert.IsFalse(connector.isConnected());
        }

        [TestMethod]        
        public void writeToStreamFailed()
        {
            Connector connector = new Connector();
            connector.connectToServer("127.0.0.1", 666);
            connector.disconnectFromServer();
            Assert.IsTrue(connector.readFromStream());
        }
       
        [TestMethod]
        public void readStream()
        {
            Connector connector = new Connector();
            connector.connectToServer("127.0.0.1", 666);
            Assert.IsTrue(connector.readFromStream());
        }

        [TestMethod]        //Method to write to Buffer C2/3
        public void writeToBuffer()
        {
            Connector connector = new Connector();
            Client_TeamOP.Buffer b = new Client_TeamOP.Buffer();
            connector.writeToBuffer("test");
            Assert.IsFalse(b.isBufferEmpty());
        }

        [TestMethod]        //Method to send Commands to Server C3/3
        public void SendToServer()
        {
            Connector connector = new Connector();
            connector.connectToServer("127.0.0.1", 666);
            Assert.IsTrue(connector.sendCommandToServer("TEST"));
        }

        [TestMethod]        //Method to read from the Buffer P1/3
        public void readFromBuffer()
        {
          Parser p = new Parser();
          Client_TeamOP.Buffer b = new Client_TeamOP.Buffer();
          b.addToBuffer("Test");
          Assert.IsTrue(p.readFromBuffer()!=null);
        }

        [TestMethod]        //Method add a String to the buffer B1/3
        public void AddToBuffer()
        {
           Client_TeamOP.Buffer b = new Client_TeamOP.Buffer(); 
           b.addToBuffer("Test");
           Assert.IsTrue(!b.isBufferEmpty());
        }

        [TestMethod]        //Method get a String to the buffer B2/3
        public void GetFromBuffer()
        {
            Client_TeamOP.Buffer b = new Client_TeamOP.Buffer();
            b.addToBuffer("Test");
            String message = b.getMessageFromBuffer();
            Assert.IsTrue(b.isBufferEmpty());
        }

        [TestMethod]        //Method send command to connector BE1/2
        public void CommandToConnector()
        {
            Backend bean = new Backend();
            Assert.IsTrue(bean.sendToConnector("get:map"));
        }

        [TestMethod]
        public void ObjectToBackend()
        {
            Backend b = new Backend();
            
        }

        [TestMethod]        //send Servermessage to a rule method P3/3
        public void sendToMethod()
        {
            Parser p = new Parser();
            Assert.IsTrue(p.sendToMethod("begin:999"));
        }

        [TestMethod]        //test for buffer status(empty) B3/3
        public void bufferEmpty()
        {
            Client_TeamOP.Buffer b = new Client_TeamOP.Buffer();
            Assert.IsTrue(b.isBufferEmpty());
        }

        [TestMethod]        //Test for refresh Gui G1/1
        public void guirefresh()
        {
            GUI g = new GUI();
            Assert.IsTrue(g.refreshGui());
        }

        [TestMethod]        //test for buffer status(full) B3/3
        public void bufferFull()
        {
            Client_TeamOP.Buffer b = new Client_TeamOP.Buffer();
            Assert.IsTrue(b.isBufferFull());
        }




        //EBNF
        [TestMethod]
        public void parseToDragon()
        {
            Parser p = new Parser();
            Assert.IsTrue(p.parseDragon());
        }


        [TestMethod]
        public void parseToDragon()
        {
            Backend b = new Backend();
            Assert.IsTrue(b.storeEnemy(Object o));
        }




    }
}

