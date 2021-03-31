namespace TapBootstrapSDK
{
    public class TapConfig
    {
        public string cliendID;

        public bool isCN;

        public TapConfig()
        {
        }

        public TapConfig(string cliendID, bool isCn)
        {
            this.cliendID = cliendID;
            isCN = isCn;
        }
    }
}