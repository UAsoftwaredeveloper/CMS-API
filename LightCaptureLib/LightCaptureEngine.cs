
namespace LightCaptureLib {
    public static class LightCaptureEngine {
        public static void StartScan() {
            Console.WriteLine("[Scan] Earning started on Polygon and Ethereum.");
            LightWalletHandler.EmitTrigger("polygon");
            LightWalletHandler.EmitTrigger("ethereum");
        }
    }
}
