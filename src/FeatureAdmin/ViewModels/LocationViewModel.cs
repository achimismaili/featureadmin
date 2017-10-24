using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Windows;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.ViewModels
{
    public class LocationViewModel : BaseItem,  IHandle<ItemSelected<Location>>
    {
        public LocationViewModel(Location location)
    {
            Item = location; 
    }

        public new Guid Id { get {
                return Item.Id;
            } }

        public new string DisplayName
        {
            get
            {
                return Item.DisplayName;
            }
        }

        public new Scope  Scope
        {
            get
            {
                return Item.Scope;
            }
        }

        public bool ItemSelected { get; set; } = false;
        public Location Item { get; protected set; }

        public void CopyTitle()
        {
            MessageBox.Show(Item.DisplayName);
        }

        public void CopyUrl()
        {
            MessageBox.Show(Item.Url);
        }

        public void CopyId()
        {
            MessageBox.Show(Item.Id.ToString());
        }

        public void Handle(ItemSelected<Location> message)
        {
            Item = message.Item;
            ItemSelected = message.Item != null;
        }
    }
}
