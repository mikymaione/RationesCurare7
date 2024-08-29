using System.Threading;
using System.Web.UI;

namespace RationesCurare
{
    public class CulturePage : Page
    {
        protected override void InitializeCulture()
        {
            var culture = GB.Instance.getCurrentSession(Session).Culture;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            base.InitializeCulture();
        }

    }
}