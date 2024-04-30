using Booster.CodingTest.Library;

namespace BoosterCodeTest.Services
{
    public class StreamWatcher
    {
        private readonly WordStream stream;

        private byte[] sizeBuffer = new byte[2];

        public StreamWatcher(WordStream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            this.stream = stream;
            WatchNext();
        }

        protected void OnMessageAvailable(MessageAvailableEventArgs e)
        {
            var handler = MessageAvailable;
            if (handler != null)
                handler(this, e);
        }

        protected void WatchNext()
        {
            
            stream.BeginRead(sizeBuffer, 0, 2, new AsyncCallback(ReadCallback),
                null);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            int bytesRead = stream.EndRead(ar);
            if (bytesRead != 2)
                throw new InvalidOperationException("Invalid message header.");
            int messageSize = sizeBuffer[1] << 8 + sizeBuffer[0];
            OnMessageAvailable(new MessageAvailableEventArgs(messageSize));
            WatchNext();
        }

        public event MessageAvailableEventHandler MessageAvailable;

        public delegate void MessageAvailableEventHandler(object sender,
    MessageAvailableEventArgs e);

        public class MessageAvailableEventArgs : EventArgs
        {
            public MessageAvailableEventArgs(int messageSize) : base()
            {
                this.MessageSize = messageSize;
            }

            public int MessageSize { get; private set; }
        }
    }
}
