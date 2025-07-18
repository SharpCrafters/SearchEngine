﻿#pragma checksum "..\..\..\SearchWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E2E99872A2D94F04B630F980C28FEDE84253C599"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using SearchEngine;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace SearchEngine {
    
    
    /// <summary>
    /// SearchWindow
    /// </summary>
    public partial class SearchWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CreatorComboBox;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TechnologyComboBox;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ColorCaptureComboBox;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ReleaseYearComboBox;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PriceComboBox;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchScannerButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SearchEngine;component/searchwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SearchWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CreatorComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 32 "..\..\..\SearchWindow.xaml"
            this.CreatorComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxSelected);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TechnologyComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 56 "..\..\..\SearchWindow.xaml"
            this.TechnologyComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxSelected);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ColorCaptureComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 80 "..\..\..\SearchWindow.xaml"
            this.ColorCaptureComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxSelected);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ReleaseYearComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 104 "..\..\..\SearchWindow.xaml"
            this.ReleaseYearComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxSelected);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PriceComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 128 "..\..\..\SearchWindow.xaml"
            this.PriceComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxSelected);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SearchScannerButton = ((System.Windows.Controls.Button)(target));
            
            #line 150 "..\..\..\SearchWindow.xaml"
            this.SearchScannerButton.Click += new System.Windows.RoutedEventHandler(this.SearchScannerButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

