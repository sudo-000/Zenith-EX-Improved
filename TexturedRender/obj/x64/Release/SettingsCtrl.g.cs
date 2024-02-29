﻿#pragma checksum "..\..\..\SettingsCtrl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0EB9C243065AFC0A41A9DA203354195B0677F2C7241A341026F5C5ACD71A2BAB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TexturedRender;
using ZenithEngine;
using ZenithEngine.UI;


namespace TexturedRender {
    
    
    /// <summary>
    /// SettingsCtrl
    /// </summary>
    public partial class SettingsCtrl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        /// <summary>
        /// paletteList Name Field
        /// </summary>
        
        #line 35 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public ZenithEngine.NoteColorPalettePick paletteList;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button reloadListButton;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button reloadPackButton;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button openFolderButton;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox pluginList;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pluginDesc;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image previewImg;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem switchTab;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel switchPanel;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZenithEngine.UI.NumberSelect firstNote;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZenithEngine.UI.NumberSelect lastNote;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZenithEngine.UI.ValueSlider noteDeltaScreenTime;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\SettingsCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZenithEngine.UI.BetterCheckbox blackNotesAbove;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TexturedRender;component/settingsctrl.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SettingsCtrl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.paletteList = ((ZenithEngine.NoteColorPalettePick)(target));
            return;
            case 2:
            this.reloadListButton = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\SettingsCtrl.xaml"
            this.reloadListButton.Click += new System.Windows.RoutedEventHandler(this.ReloadButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.reloadPackButton = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\SettingsCtrl.xaml"
            this.reloadPackButton.Click += new System.Windows.RoutedEventHandler(this.ReloadPackButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.openFolderButton = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\SettingsCtrl.xaml"
            this.openFolderButton.Click += new System.Windows.RoutedEventHandler(this.openFolderButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.pluginList = ((System.Windows.Controls.ListBox)(target));
            
            #line 40 "..\..\..\SettingsCtrl.xaml"
            this.pluginList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PluginList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.pluginDesc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.previewImg = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.switchTab = ((System.Windows.Controls.TabItem)(target));
            return;
            case 9:
            this.switchPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.firstNote = ((ZenithEngine.UI.NumberSelect)(target));
            
            #line 58 "..\..\..\SettingsCtrl.xaml"
            this.firstNote.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<decimal>(this.Nud_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.lastNote = ((ZenithEngine.UI.NumberSelect)(target));
            
            #line 60 "..\..\..\SettingsCtrl.xaml"
            this.lastNote.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<decimal>(this.Nud_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.noteDeltaScreenTime = ((ZenithEngine.UI.ValueSlider)(target));
            
            #line 64 "..\..\..\SettingsCtrl.xaml"
            this.noteDeltaScreenTime.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.NoteDeltaScreenTime_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            this.blackNotesAbove = ((ZenithEngine.UI.BetterCheckbox)(target));
            
            #line 66 "..\..\..\SettingsCtrl.xaml"
            this.blackNotesAbove.CheckToggled += new System.Windows.RoutedPropertyChangedEventHandler<bool>(this.BlackNotesAbove_Checked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

