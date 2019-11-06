using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Media.Imaging;

namespace ImageProcessingUtility
{
    static class Extensions
    {
        public static BitmapSource ToSource(this Bitmap img)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }

        public static Bitmap ToBitmap(this BitmapSource img)
        {
            return new Bitmap(img.PixelWidth, img.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        }


        public static void Raise<TResult>(this PropertyChangedEventHandler _this, Expression<Func<TResult>> propertyName)
        {
            if (_this == null) return;

            if (!(propertyName.Body is MemberExpression memberEx))
                throw new ArgumentException();

            if (!(memberEx.Expression is ConstantExpression senderExpression))
                throw new ArgumentException();

            var sender = senderExpression.Value;
            _this(sender, new PropertyChangedEventArgs(memberEx.Member.Name));
        }

        public static bool RaiseIfSet<TResult>(this PropertyChangedEventHandler _this, Expression<Func<TResult>> propertyName, ref TResult source, TResult value)
        {
            if (EqualityComparer<TResult>.Default.Equals(source, value))
                return false;

            source = value;
            Raise(_this, propertyName);

            return true;
        }
    }
}
