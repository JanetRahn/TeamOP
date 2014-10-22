using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Client_TeamOP;


namespace ProgrammTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void isConnected()
        {            
            Connector connector = new Connector();
            connector.connectToServer("127.0.0.1", 666);
            Assert.IsTrue(connector.isConnected());
        }

        [TestMethod]
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

        [TestMethod]
        public void writeToBuffer()
        {
            Connector connector = new Connector();
            Client_TeamOP.Buffer b = new Client_TeamOP.Buffer();
            connector.writeToBuffer("test");
            Assert.IsFalse(b.isBufferEmpty());
        }


        [TestMethod]
        public void SendToServer()
        {
            Connector connector = new Connector();
            connector.connectToServer("127.0.0.1", 666);
            Assert.IsTrue(connector.sendCommandToServer("TEST"));
        }

        [TestMethod]
        public void readFromBuffer()
        {
          Parser p = new Parser();
          Client_TeamOP.Buffer b = new Client_TeamOP.Buffer();
          b.addToBuffer("Test");
          Assert.IsTrue(p.readFromBuffer()!=null);
        }

        [TestMethod]
        public void AddToBuffer()
        {
           Client_TeamOP.Buffer b = new Client_TeamOP.Buffer(); 
           b.addToBuffer("Test");
           Assert.IsTrue(!b.isBufferEmpty());
        }

        [TestMethod]
        public void GetFromBuffer()
        {
            Client_TeamOP.Buffer b = new Client_TeamOP.Buffer();
            b.addToBuffer("Test");
            String message = b.getMessageFromBuffer();
            Assert.IsTrue(b.isBufferEmpty());
        }

        [TestMethod]
        public void CommandToConnector()
        {
            Backend bean = new Backend();
            Assert.IsTrue(bean.sendToConnector("ask:map"));
        }

        [TestMethod]
        public void ObjectToBackend()
        {
            Backend b = new Backend();
            
        }

    }
}

