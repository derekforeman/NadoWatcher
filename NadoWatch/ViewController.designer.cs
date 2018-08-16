// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace NadoWatch
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnRefresh { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtDescription { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BtnRefresh != null) {
                BtnRefresh.Dispose ();
                BtnRefresh = null;
            }

            if (txtDescription != null) {
                txtDescription.Dispose ();
                txtDescription = null;
            }
        }
    }
}