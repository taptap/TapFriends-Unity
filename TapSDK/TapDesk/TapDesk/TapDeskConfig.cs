using System;

namespace TapTap.Desk
{
    public class TapDeskConfig
    {
        public string Server { get; }

        public string RootCategoryID { get; }

        public TapDeskMode Mode { get; }

        public string AnonymousId { get; }

        private TapDeskConfig(string server, string rootCategoryID, TapDeskMode mode, string anonymousId)
        {
            Server = server;
            RootCategoryID = rootCategoryID;
            Mode = mode;
            AnonymousId = anonymousId;
        }

        internal class Builder
        {
            private string _server;

            private string _rootCategoryID;

            private TapDeskMode _mode;

            private string _anonymousId;

            public Builder Server(string server)
            {
                _server = server;
                return this;
            }

            public Builder RootCategoryID(string rootCategoryID)
            {
                _rootCategoryID = rootCategoryID;
                return this;
            }

            public Builder Mode(TapDeskMode mode)
            {
                _mode = mode;
                return this;
            }

            public Builder AnonymousId(string anonymousId)
            {
                _anonymousId = string.IsNullOrEmpty(anonymousId) ? TapDeskPersistence.ConstructorUuid() : anonymousId;
                return this;
            }

            public TapDeskConfig DeskConfig()
            {
                return new TapDeskConfig(_server, _rootCategoryID, _mode, _anonymousId);
            }
        }
    }
}