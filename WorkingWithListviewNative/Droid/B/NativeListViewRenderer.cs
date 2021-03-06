﻿using System;
using Xamarin.Forms;
using WorkingWithListviewNative;
using WorkingWithListviewNative.Droid;
using Xamarin.Forms.Platform.Android;
using System.Collections;
using System.Linq;

[assembly: ExportRenderer (typeof (NativeListView), typeof (NativeListViewRenderer))]

namespace WorkingWithListviewNative.Droid
{
	public class NativeListViewRenderer : ViewRenderer<NativeListView, global::Android.Widget.ListView>
	{
		public NativeListViewRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<NativeListView> e)
		{
			base.OnElementChanged (e);

			if (Control == null) {
				SetNativeControl (new global::Android.Widget.ListView (Forms.Context));
			}

			if (e.OldElement != null) {
				// unsubscribe
				Control.ItemClick -= clicked;
			}

			if (e.NewElement != null) {
				// subscribe

				Control.Adapter = new NativeListViewAdapter (Forms.Context as Android.App.Activity, e.NewElement);
				Control.ItemClick += clicked;
			}
		}

		void clicked (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) {
			Element.NotifyItemSelected (Element.Items.ToList()[e.Position]);
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName == NativeListView.ItemsProperty.PropertyName) {
				// update the Items list in the UITableViewSource

				Control.Adapter = new NativeListViewAdapter (Forms.Context as Android.App.Activity, Element);
			}
		}
	}
}