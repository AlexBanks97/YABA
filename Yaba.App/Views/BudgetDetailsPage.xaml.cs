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
using Yaba.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Yaba.Common.Budget.DTO;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BudgetDetailsPage : Page
    {
	    private readonly BudgetsDetailViewModel _vm;
		
        public BudgetDetailsPage()
        {
            InitializeComponent();
	        _vm = App.ServiceProvider.GetService<BudgetsDetailViewModel>();
	        DataContext = _vm;
        }

	    protected override async void OnNavigatedTo(NavigationEventArgs e)
	    {
		    base.OnNavigatedTo(e);
		    if (!(e.Parameter is BudgetViewModel budget)) return;
		    _vm.Frame = Detail;
		    await _vm.Initialize(budget);
	    }
    }
}
