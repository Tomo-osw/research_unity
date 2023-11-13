using System;
using System.Net;
using System.Net.Sockets;
using System.Security;


namespace Samples.UDP
{
    public class UDPSocket
    {
        #region Members
        private Socket socket = null;
        private EndPoint endpoint;
        private IPEndPoint localEndpoint = null;

        private AsyncCallback receiveCallback;
        private AsyncCallback sendCallback;

        private int bufferSize = 1024;
        private byte[] buffer;

        private bool isListening = false;
        private bool isAvailable = false;
        #endregion



        #region Delegates and Events
        public delegate void OnReceiveEventHandler(byte[] bytesRead, string address, int port);
        public delegate void OnErrorEventHandler(string message);
        public delegate void OnSendEventHandler(int sentByteSize);

        public event OnReceiveEventHandler OnReceive;
        public event OnErrorEventHandler OnError;
        public event OnSendEventHandler OnSend;
        #endregion



        #region Constructor
        /// <summary>
        /// Create a socket for the UDP communication. 
        /// </summary>
        public UDPSocket(int port)
        {
            receiveCallback = new AsyncCallback(ReceiveCallback);
            sendCallback = new AsyncCallback(SendCallback);

            buffer = new byte[bufferSize];

            try
            {
                localEndpoint = new IPEndPoint(IPAddress.Loopback, port);
                endpoint = new IPEndPoint(IPAddress.Any, 0);

                socket = new(localEndpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(localEndpoint);

                isAvailable = true;
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("Failed to initialize the UDP socket.", e);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new ArgumentOutOfRangeException("Failed to initialize the UDP socket.", e);
            }
            
            catch (SocketException e)
            {
                throw e;
            }
            catch (SecurityException e)
            {
                throw new SecurityException("Failed to initialize the UDP socket.", e);
            } 
        }
        #endregion



        #region Functions to Control the Socket

        public void Listen()
        {
            if (!isAvailable)
            {
                OnError?.Invoke("( ﾟДﾟ) This socket is not available.");
                return;
            }

            if (isListening)
            {
                OnError?.Invoke("( ﾟДﾟ) This socket starts to listen already.");
                return;
            }

            StartListening();
        }


        /// <summary>
        /// Sends data asynchronously to a specific to the other localhost associated with specified port number.
        /// </summary>
        /// <param name="bytesSent">An array of type Byte that contains the data to send.</param>
        /// <param name="port">A port number of the host to receive this data.</param>
        public void Send(byte[] bytesSent, int port)
        {
            if (!isAvailable)
            {
                OnError?.Invoke("( ﾟДﾟ) This socket is not available.");
                return;
            }


            try
            {
                endpoint = new IPEndPoint(IPAddress.Loopback, port);
            }
            catch (ArgumentOutOfRangeException)
            {
                OnError?.Invoke("( ﾟДﾟ) ArgumentOutOfRangeException -----\nThe IP address isn't specified.");
                return;
            }


            try
            {
                socket.BeginSendTo(bytesSent, 0, bytesSent.Length, SocketFlags.None, endpoint, sendCallback, socket);
            }
            catch (ArgumentNullException)
            {
                OnError?.Invoke("( ﾟДﾟ) Argument Exception -----\n" +
                    "Either the buffer to receive the data is not available or the remote endpoint has not been specified.");
            }
            catch (SocketException se)
            {
                OnError?.Invoke(string.Format("( ﾟДﾟ) Socket Exception -----\n" +
                   "An error occured when attempting to access the socket. (Error Code: {0})", se.ErrorCode));
            }
            catch (ArgumentOutOfRangeException)
            {
                OnError?.Invoke("( ﾟДﾟ) ArgumentOutOfRangeException -----\n" +
                    "The specified read position or read data size is invalid.");
            }
            catch (ObjectDisposedException)
            {
                OnError?.Invoke("( ﾟДﾟ) ObjectDisposedException -----\nThe socket has been closed.");
            }
            catch (SecurityException)
            {
                OnError?.Invoke("( ﾟДﾟ) SecurityException -----\n" +
                     "A caller higher in the call stack does not have permission for the requested operation.");
            }
        }


        /// <summary>
        /// Close the socket connection and releases all associated resources. 
        /// </summary>
        public void Close()
        {
            isListening = false;
            isAvailable = false;
            socket.Close();
        }
        #endregion



        #region Private methods
        private void StartListening()
        {
            try
            {
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endpoint, receiveCallback, socket);
                isListening = true;
            }
            catch (ArgumentNullException)
            {
                OnError?.Invoke("( ﾟДﾟ) Argument Exception -----\n" +
                    "Either the buffer to receive the data is not available or the remote endpoint has not been specified.");
            }
            catch (SocketException se)
            {
                OnError?.Invoke(string.Format("( ﾟДﾟ) Socket Exception -----\n" +
                    "An error occured when attempting to access the socket. (Error Code: {0})", se.ErrorCode));
            }
            catch (ArgumentOutOfRangeException)
            {
                OnError?.Invoke("( ﾟДﾟ) ArgumentOutOfRangeException -----\n" +
                    "The specified read position or read data size is invalid.");
            }
            catch (ObjectDisposedException)
            {
                OnError?.Invoke("( ﾟДﾟ) ObjectDisposedException -----\nThe socket has been closed.");
            }
            catch (SecurityException)
            {
                OnError?.Invoke("( ﾟДﾟ) SecurityException -----\n" +
                    "A caller higher in the call stack does not have permission for the requested operation.");
            }
        }
        #endregion





        #region Callbacks
        private void ReceiveCallback(IAsyncResult ar)
        {
            socket = ar.AsyncState as Socket;
            int size = socket.EndReceiveFrom(ar, ref endpoint);

            if (size > 0)
            {
                IPEndPoint remoteEndpoint = endpoint as IPEndPoint;

                byte[] bytesReceived = new byte[size];
                Array.Copy(buffer, 0, bytesReceived, 0, size);

                OnReceive?.Invoke(bytesReceived, remoteEndpoint.Address.ToString(), remoteEndpoint.Port);

                StartListening();
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            Socket socket = ar.AsyncState as Socket;
            int size = socket.EndSendTo(ar);
            OnSend?.Invoke(size);
        }
        #endregion




        #region Getter
        /// <summary>
        /// The size of the receive buffer of the socket.
        /// </summary>
        public int BufferSize { get { return bufferSize; } }


        /// <summary>
        /// IP address of the endpoint. 
        /// </summary>
        public string Address
        {
            get
            {
                if (localEndpoint != null)
                {
                    return localEndpoint.Address.ToString();
                }
                else
                {
                    return "";
                }
            }
        }


        /// <summary>
        /// Port number of the endpoint.
        /// </summary>
        public int Port
        {
            get
            {
                if (localEndpoint != null)
                {
                    return localEndpoint.Port;
                }
                else
                {
                    return -1;
                }
            }
        }


        public bool IsAvailabe { get { return isAvailable; } }

        public bool IsListening { get { return isListening; } }
        #endregion
    }
}
