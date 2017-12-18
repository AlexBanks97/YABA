using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaba.App.ViewModels
{
	public interface IView
	{
		void OpenUriInWebView(Uri uri);
		void ActivateFailruePopup();
	}
}
