using Abp.Web.Mvc.Views;

namespace LearningForAbp.Web.Views
{
    public abstract class LearningForAbpWebViewPageBase : LearningForAbpWebViewPageBase<dynamic>
    {

    }

    public abstract class LearningForAbpWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected LearningForAbpWebViewPageBase()
        {
            LocalizationSourceName = LearningForAbpConsts.LocalizationSourceName;
        }
    }
}