﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Yaba.Common.Budget.DTO;
using Yaba.UWPApp.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.UWPApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class BudgetView : Page
	{
		private readonly BudgetDetailViewModel _vm;
		public BudgetView()
		{
			this.InitializeComponent();
			

			DataContext = _vm;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var w = e.Parameter as BudgetDto;
			base.OnNavigatedTo(e);
		}
	}
}