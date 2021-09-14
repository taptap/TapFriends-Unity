using System.Threading.Tasks;

namespace TapTap.Desk
{
    public interface ITapDesk
    {
        void Init(TapDeskConfig config);

        Task<string> Login(string appId, string sessionToken);

        Task<string> AnonymousLogin(string uuid);

        Task<string> AnonymousLogin();

        void Resume();

        void Pause();

        string ConstructorTapDesk();
    }

    public class TapDeskContants
    {
        public readonly string PathHome = "/";
        public readonly string PathCategory = "/categories/";
        public readonly string PathTicketHistory = "/tickets";
        public readonly string PathTicketNew = "/tickets/new?category_id=";
    }

    public enum TapDeskMode
    {
        Anonymous,
        TapSDK
    }
}