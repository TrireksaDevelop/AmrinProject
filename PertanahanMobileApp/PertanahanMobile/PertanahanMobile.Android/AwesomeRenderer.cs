using Android.Content;
using Android.Graphics;
using Android.Widget;
using PertanahanMobile;
using PertanahanMobile.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(IconAwesome), typeof(AwesomeRenderer))]
namespace PertanahanMobile.Droid
{
    public class AwesomeRenderer : LabelRenderer
    {

        public AwesomeRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var label = (TextView)Control;

            var text = label.Text;
            if (text.Length > 1 || text[0] < 0xf000)
            {
                return;
            }

            var font = Typeface.CreateFromAsset(Context.ApplicationContext.Assets, path: "FontAwesome.otf");
            label.Typeface = font;
        }
    }
}

