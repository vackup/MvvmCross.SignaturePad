﻿using System;
using System.Drawing;
using UIKit;
using SignaturePad;


namespace Acr.MvvmCross.Plugins.SignaturePad.Touch {

    public class MvxSignatureView : UIView {

        public SignaturePadView Signature { get; set; }
        public UIButton SaveButton { get; private set; }
        public UIButton CancelButton { get; private set; }


        public MvxSignatureView() {
            //BackgroundColor = UIColor.White;
            this.SaveButton = UIButton.FromType(UIButtonType.RoundedRect);
            this.CancelButton = UIButton.FromType(UIButtonType.RoundedRect);
            this.Frame = UIScreen.MainScreen.ApplicationFrame;

            this.Signature = new SignaturePadView();
            this.TranslatesAutoresizingMaskIntoConstraints = false;

            this.AddSubview(this.Signature);
            this.AddSubview(this.SaveButton);
            this.AddSubview(this.CancelButton);
        }


        public override void LayoutSubviews() {
            //            if (new Version(MonoTouch.Constants.Version) >= new Version (7, 0)) {
            var frame = this.Frame;
            var sbframe = UIApplication.SharedApplication.StatusBarFrame;
            var portrait = UIApplication.SharedApplication.StatusBarOrientation.HasFlag(UIInterfaceOrientation.Portrait);

            var width = portrait
                ? frame.Size.Width
                : frame.Size.Width - sbframe.Width;

            var height = portrait
                ? frame.Size.Height - sbframe.Height
                : frame.Size.Height;

            var x = portrait
                ? 0
                : frame.Location.X + sbframe.Width;

            var y = portrait
                ? frame.Location.Y + sbframe.Height
                : 0;

            this.Frame = new RectangleF((float)x, (float)y, (float)width, (float)height);
            //            }


            ///Using different layouts for the iPhone and iPad, so setup device specific requirements here.
            //            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
            this.Signature.Frame = new RectangleF(10, 10, (float)Bounds.Width - 20, (float)Bounds.Height - 60);
            this.Signature.BackgroundImageView.Frame = this.Signature.Bounds;
            //            else 
            //                this.Signature.Frame = new RectangleF (84, 84, Bounds.Width - 168, Bounds.Width / 2);
            //

            //Button locations are based on the Frame, so must have their own frames set after the view's
            //Frame has been set.
            this.SaveButton.Frame = new RectangleF(10, (float)this.Bounds.Height - 40, 120, 37);
            this.CancelButton.Frame = new RectangleF((float)this.Bounds.Width - 130, (float)this.Bounds.Height - 40, 120, 37);
        }
    }
}