﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.SimpleChildWindow;

namespace VirtualEnvPanel
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //private List<StartupCommand> commands;
        private PanelMetadata panelMetadata;
        public MainWindow()
        {
            InitializeComponent();
            InitializeVirtualEnvCommand();
        }
        private bool InitializeVirtualEnvCommand()
        {
            var self = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            var jsonfile= System.IO.Path.GetDirectoryName(self) +"\\VirtualEnvPanel.json";
            //var jsonmeta = new ResloveJSONMetadata(jsonfile);
            //commands = jsonmeta.StartupCommandGet();
            panelMetadata = PanelMetadata.Builder(jsonfile);
            int offset = 40;
            int left = 40;
           foreach(var c in panelMetadata.App)
            {
                Tile tile = new Tile();
                tile.Title = c.Title;
                tile.Name = c.Name;
                tile.MouseRightButtonUp += new MouseButtonEventHandler(EditWindowEvent);
                //tile.Style.BasedOn
                tile.Width = 80;
                tile.Height = 80;
                
                tile.HorizontalAlignment = HorizontalAlignment.Left;
                tile.VerticalAlignment = VerticalAlignment.Top;
                tile.Visibility = Visibility.Visible;
                tile.Margin = new Thickness(offset, left, 0, 0);
                tile.Click+= new RoutedEventHandler(TileClickEvent);
                offset += 90;
                if (offset + 80 > Width)
                {
                    offset = 40;
                    left += 90;
                }
                baseGrid.Children.Add(tile);
                baseGrid.RegisterName(c.Name, tile);
                //tile.RegisterName()
                //
            }
            return true;
        }
        private void TileClickEvent(object sender, RoutedEventArgs e)
        {
            Tile tile = sender as Tile;
            foreach(var c in panelMetadata.App)
            {
                if (tile.Name == c.Name)
                {
                    c.Execute();
                    return;
                }
            }
            //StartupCommand cmd = (StartupCommand)sender;
        }
        private bool FileIcon(StartupCommand sc)
        {
            if (sc.Icon != null)
            {
             //if(System.IO.File.Exists(sc.Icon))   
            }
            return true;
        }
        private async void DisplayEditWindow(object sender, RoutedEventArgs e)
        {
            await this.ShowChildWindowAsync(new EditCommandWindow() { IsModal=false});
        }
        private void EditWindowEvent(object sender, RoutedEventArgs e)
        {
            DisplayEditWindow(sender, e);
        }
    }
}
