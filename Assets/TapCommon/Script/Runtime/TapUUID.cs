namespace TapCommon
{
    public class TapUUID
    {
        public static string UUID(){
            return System.Guid.NewGuid().ToString();
        }
    }
}