using System;

namespace TapCommonSDK
{
    public interface IBridge
    {
        void Register(string serviceClzName, string serviceImplName);

        void Call(Command command);

        void Call(Command command, Action<Result> action);
        
    }
}