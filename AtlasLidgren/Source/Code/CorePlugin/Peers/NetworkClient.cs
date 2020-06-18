﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lidgren.Network;

using Duality;

using Soulstone.Duality.Plugins.Atlas.Network;

namespace Soulstone.Duality.Plugins.Atlas.Lidgren
{
    public class NetworkClient : NetworkPeer, IClient
    {
        private NetClient _client;
        private bool _joining = false;

        public event EventHandler<ServerJoinedEventArgs> Joined;

        public override PeerInfo Server
        {
            get => Connections.FirstOrDefault();
        }

        public bool Joining
        {
            get => _joining;
        }

        protected virtual void OnJoined(ServerJoinedEventArgs e)
        {
            Joined?.Invoke(this, e);
        }

        public bool Join(string name, IPEndPoint target)
        {
            if (Joining)
            {
                AtlasApp.NetworkLog.WriteWarning("Started to join while joining already");
                AtlasApp.NetworkLog.Write("Interrupting previous Join attempt");
                Quit();
            }

            if (!Idle)
            {
                AtlasApp.NetworkLog.WriteWarning("Must be idle to start joining");
                return false;
            }

            _joining = true;

            string appId = Properties.Settings.Default.AppID;

            var config = new NetPeerConfiguration(appId);
            var client = new NetClient(config);
            _client = client;

            if (!Start(client, name)) return false;

            _client.Connect(Conversions.ToNet(target));
            return true;
        }

        protected override void OnIdentified(PeerInfo senderInfo)
        {
            base.OnIdentified(senderInfo);

            OnJoined(new ServerJoinedEventArgs(senderInfo));
        }

        public void SendData(byte[] data, DeliveryMethod deliveryMethod, int channel = 0)
        {
            SendData(data, deliveryMethod, channel, null);
        }

        protected override void OnConnected(ConnectedEventArgs e)
        {
            _joining = false;

            base.OnConnected(e);
        }

        protected override void OnDisconnected(DisconnectEventArgs e)
        {
            _joining = false;

            base.OnDisconnected(e);

            Quit();
        }
    }
}
