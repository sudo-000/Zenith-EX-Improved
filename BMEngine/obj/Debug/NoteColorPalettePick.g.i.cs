﻿#pragma checksum "..\..\NoteColorPalettePick.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B735C52CFC5E77EFD2C003FB3DD63E9DE9B18E52C6DBE1C135A8AA3EBFB9062D"
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
using ZenithEngine;
using ZenithEngine.UI;


namespace ZenithEngine {
    
    
    /// <summary>
    /// NoteColorPalettePick
    /// </summary>
    public partial class NoteColorPalettePick : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\NoteColorPalettePick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button reloadButton;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\NoteColorPalettePick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button openPaletteFolder;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\NoteColorPalettePick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZenithEngine.UI.BetterCheckbox randomiseOrder;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\NoteColorPalettePick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox paletteList;
        
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
            System.Uri resourceLocater = new System.Uri("/ZenithEngine;component/notecolorpalettepick.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NoteColorPalettePick.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.reloadButton = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\NoteColorPalettePick.xaml"
            this.reloadButton.Click += new System.Windows.RoutedEventHandler(this.ReloadButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.openPaletteFolder = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\NoteColorPalettePick.xaml"
            this.openPaletteFolder.Click += new System.Windows.RoutedEventHandler(this.openPaletteFolder_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.randomiseOrder = ((ZenithEngine.UI.BetterCheckbox)(target));
            return;
            case 4:
            this.paletteList = ((System.Windows.Controls.ListBox)(target));
            
            #line 16 "..\..\NoteColorPalettePick.xaml"
            this.paletteList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PaletteList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

