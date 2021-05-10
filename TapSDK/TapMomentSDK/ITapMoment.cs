using System;
using System.Collections.Generic;

namespace TapTap.Moment
{
    public interface ITapMoment
    {
        void SetCallback(Action<int, string> callback);

        void Init(string clientId);

        void Init(string clientId, bool isCN);

        void Open(Orientation orientation);

        void Publish(Orientation orientation, string[] imagePaths, string content);

        void PublishVideo(Orientation orientation, string[] videoPaths, string[] imagePaths, string title,
            string desc);

        void PublishVideo(Orientation orientation, string[] videoPaths, string title, string desc);

        void FetchNotification();

        void Close();

        void Close(string title, string content);

        void SetUseAutoRotate(bool useAuto);

        void DirectlyOpen(Orientation orientation, string page, Dictionary<string, object> extras);
        
    }

    public class TapMomentConstants
    {
        public static readonly string TapMomentPageShortCut = "tap://moment/scene/";

        public static readonly string TapMomentPageShortCutKey = "scene_id";

        public static readonly string TapMomentPageUser = "tap://moment/user/";

        public static readonly string TapMomentPageUserKey = "user_id";
    }
}