using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TapTap.Bootstrap;
using TapTap.Common;

namespace TapTap.Achievement
{
    public class TapAchievement
    {
        public static void Init(TapConfig config)
        {
            TapAchievementImpl.GetInstance().Init(config);
        }

        public static void RegisterCallback(IAchievementCallback callback)
        {
            TapAchievementImpl.GetInstance().RegisterCallback(callback);
        }

        public static void InitData()
        {
            TapAchievementImpl.GetInstance().InitData();
        }

        public static void FetchAllAchievementList(Action<List<TapAchievementBean>, TapError> action)
        {
            TapAchievementImpl.GetInstance().FetchAllAchievementList(action);
        }

        public static void GetLocalAllAchievementList(Action<List<TapAchievementBean>, TapError> action)
        {
            TapAchievementImpl.GetInstance().GetLocalAllAchievementList(action);
        }


        public static void GetLocalUserAchievementList(Action<List<TapAchievementBean>, TapError> action)
        {
            TapAchievementImpl.GetInstance().GetLocalUserAchievementList(action);
        }

        public static void FetchUserAchievementList(Action<List<TapAchievementBean>, TapError> action)
        {
            TapAchievementImpl.GetInstance().FetchUserAchievementList(action);
        }

        public static Task<List<TapAchievementBean>> FetchAllAchievementList()
        {
            return TapAchievementImpl.GetInstance().FetchAllAchievementList();
        }

        public static Task<List<TapAchievementBean>> GetLocalUserAchievementList()
        {
            return TapAchievementImpl.GetInstance().GetLocalUserAchievementList();
        }

        public static Task<List<TapAchievementBean>> GetLocalAllAchievementList()
        {
            return TapAchievementImpl.GetInstance().GetLocalAllAchievementList();
        }

        public static Task<List<TapAchievementBean>> FetchUserAchievementList()
        {
            return TapAchievementImpl.GetInstance().FetchUserAchievementList();
        }

        public static void Reach(string id)
        {
            TapAchievementImpl.GetInstance().Reach(id);
        }

        public static void GrowSteps(string id, int step)
        {
            TapAchievementImpl.GetInstance().GrowSteps(id, step);
        }

        public static void MakeSteps(string id, int step)
        {
            TapAchievementImpl.GetInstance().MakeSteps(id, step);
        }

        public static void ShowAchievementPage()
        {
            TapAchievementImpl.GetInstance().ShowAchievementPage();
        }

        public static void SetShowToast(bool showToast)
        {
            TapAchievementImpl.GetInstance().SetShowToast(showToast);
        }
    }
}