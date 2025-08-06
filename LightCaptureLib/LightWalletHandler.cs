
namespace LightCaptureLib {
    public static class LightWalletHandler {
        private static readonly string polygonRpc = "https://polygon-mainnet.g.alchemy.com/v2/CnmU2i3SqSZCMZ0ovJqG9";
        private static readonly string ethereumRpc = "https://eth-mainnet.g.alchemy.com/v2/GugKjzvz03GJXU_k1ZnKO";
        private static readonly string wallet = "0x879449E0B9584a520404fF94BD8d79bb042Bb050";

        public static void EmitTrigger(string chain) {
            string rpc = chain == "polygon" ? polygonRpc : ethereumRpc;
            Console.WriteLine($"> [{chain.ToUpper()}] Connected to {rpc}");
            Console.WriteLine($"> [{chain.ToUpper()}] Monitoring wallet: {wallet}");
            Console.WriteLine($"> [{chain.ToUpper()}] Dummy earning executed...");
        }
    }
}
