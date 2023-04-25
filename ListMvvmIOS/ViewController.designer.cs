// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ListMvvmIOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton addButton { get; set; }

		[Outlet]
		UIKit.UITextField categoryTextField { get; set; }

		[Outlet]
		UIKit.UIButton clearButton { get; set; }

		[Outlet]
		UIKit.UITextField descriptionTextField { get; set; }

		[Outlet]
		UIKit.UITableView tasksTableView { get; set; }

		[Outlet]
		UIKit.UITextField titleTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (titleTextField != null) {
				titleTextField.Dispose ();
				titleTextField = null;
			}

			if (descriptionTextField != null) {
				descriptionTextField.Dispose ();
				descriptionTextField = null;
			}

			if (categoryTextField != null) {
				categoryTextField.Dispose ();
				categoryTextField = null;
			}

			if (addButton != null) {
				addButton.Dispose ();
				addButton = null;
			}

			if (clearButton != null) {
				clearButton.Dispose ();
				clearButton = null;
			}

			if (tasksTableView != null) {
				tasksTableView.Dispose ();
				tasksTableView = null;
			}
		}
	}
}
