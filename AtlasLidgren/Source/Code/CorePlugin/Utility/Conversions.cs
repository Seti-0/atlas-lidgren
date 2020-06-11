using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

using Soulstone.Duality.Plugins.Atlas.Network;

namespace Soulstone.Duality.Plugins.Atlas.Lidgren
{
    internal static class Conversions
    {
        public static Network.IPEndPoint ToArke(System.Net.IPEndPoint ipEnd)
        {
            if (ipEnd == null) 
                throw new ArgumentNullException(nameof(ipEnd));

            return new Network.IPEndPoint(ToArke(ipEnd.Address), (ushort)ipEnd.Port);
        }

        public static Network.IPAddress ToArke(System.Net.IPAddress ip)
        {
            if (ip == null) 
                throw new ArgumentNullException(nameof(ip));

            return new Network.IPAddress(ip.GetAddressBytes());
        }

        public static System.Net.IPEndPoint ToNet(Network.IPEndPoint ipEnd)
        {
            if (ipEnd == null) 
                throw new ArgumentNullException(nameof(ipEnd));

            return new System.Net.IPEndPoint(ToNet(ipEnd.Address), ipEnd.Port);
        }

        public static System.Net.IPAddress ToNet(Network.IPAddress ip)
        {
            if (ip == null) 
                throw new ArgumentNullException(nameof(ip));

            return new System.Net.IPAddress(ip.Bytes);
        }

        public static global::Lidgren.Network.NetDeliveryMethod ToLidgren(DeliveryMethod method)
        {
            switch (method)
            {
                case DeliveryMethod.Unknown: return global::Lidgren.Network.NetDeliveryMethod.Unknown;
                case DeliveryMethod.Unreliable: return global::Lidgren.Network.NetDeliveryMethod.Unreliable;
                case DeliveryMethod.UnreliableSequenced: return global::Lidgren.Network.NetDeliveryMethod.UnreliableSequenced;
                case DeliveryMethod.ReliableUnordered: return global::Lidgren.Network.NetDeliveryMethod.ReliableUnordered;
                case DeliveryMethod.ReliableSequenced: return global::Lidgren.Network.NetDeliveryMethod.ReliableSequenced;
                case DeliveryMethod.ReliableOrdered: return global::Lidgren.Network.NetDeliveryMethod.ReliableOrdered;

                default: return global::Lidgren.Network.NetDeliveryMethod.Unknown;
            }
        }
    }
}
