using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Models;
using Caliburn.Micro;
using System.Windows;

namespace FeatureAdmin.ViewModels
{
    public class DialogViewModel : IHaveDisplayName
    {
        public DialogViewModel(string displayName, string dialogText)
        {
            DisplayName = string.Format("Confirmation for {0}", displayName);
            DialogText = dialogText;
        }

        public string DialogText { get; private set; }

        public string DisplayName { get; set; }

        public void Ok()
        {
            //TODO: get task id and publish it
            MessageBox.Show("OK");
        }

        public void Cancel()
        {
            //TODO: Log dialog canceled
            MessageBox.Show("Cancel");
        }
    }
}
