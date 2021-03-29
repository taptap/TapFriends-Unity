using System;

namespace TapMoment
{
    public class TapMoment
    {
        public static void SetCallback(Action<int, string> callback)
        {
            MomentImpl.GetInstance().SetCallback(callback);
        }

        public static void Init(string clientId)
        {
            MomentImpl.GetInstance().Init(clientId);
        }

        public static void Init(string clientId, bool isCN)
        {
            MomentImpl.GetInstance().Init(clientId, isCN);
        }

        public static void Open(Orientation config)
        {
            MomentImpl.GetInstance().Open(config);
        }

        public static void Publish(Orientation config, string[] imagePaths, string content)
        {
            MomentImpl.GetInstance().Publish(config, imagePaths, content);
        }

        public static void PublishVideo(Orientation config, string[] videoPaths, string[] imagePaths, string title,
            string desc)
        {
            MomentImpl.GetInstance().PublishVideo(config, videoPaths, imagePaths, title, desc);
        }

        public static void PublishVideo(Orientation config, string[] videoPaths, string title, string desc)
        {
            MomentImpl.GetInstance().PublishVideo(config, videoPaths, title, desc);
        }

        public static void GetNoticeData()
        {
            MomentImpl.GetInstance().FetchNotification();
        }

        public static void Close()
        {
            MomentImpl.GetInstance().Close();
        }

        public static void Close(string title, string desc)
        {
            MomentImpl.GetInstance().Close(title, desc);
        }

        public static void SetGameScreenAutoRotate(bool auto)
        {
            MomentImpl.GetInstance().SetUseAutoRotate(auto);
        }

        public static void OpenSceneEntry(Orientation orientation, string sceneId)
        {
            MomentImpl.GetInstance().OpenSceneEntry(orientation, sceneId);
        }

        public static void OpenUserCenter(Orientation orientation, string userId)
        {
            MomentImpl.GetInstance().OpenUserCenter(orientation, userId);
        }
    }
}