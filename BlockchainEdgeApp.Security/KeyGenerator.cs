using System;
using System.IO;
using NBitcoin;
using NBitcoin.DataEncoders;

namespace BlockchainEdgeApp.Security
{
    public class KeyGenerator
    {
        private const string PrivateKeyPath = "private.key";
        private Key _privateKey;

        public KeyGenerator()
        {
            LoadOrCreateKey();
        }

        private void LoadOrCreateKey()
        {
            if (File.Exists(PrivateKeyPath))
            {
                var privateKeyHex = File.ReadAllText(PrivateKeyPath);
                _privateKey = new Key(Encoders.Hex.DecodeData(privateKeyHex));
            }
            else
            {
                _privateKey = new Key();
                File.WriteAllText(PrivateKeyPath, _privateKey.ToHex());
            }
        }

        public string GetPrivateKeyHex() => _privateKey.ToHex();

        public string GetPublicKeyHex() => _privateKey.PubKey.ToHex();

        public PubKey GetPublicKey() => _privateKey.PubKey;

        public Key GetPrivateKey() => _privateKey;
    }
}
