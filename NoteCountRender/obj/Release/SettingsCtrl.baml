   M S B A M L   `   `   `  ���� G  CZenithEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG3clr-namespace:ZenithEngine.UI;assembly=ZenithEngineZenithEngine.UI  J FNoteCountRender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:NoteCountRenderNoteCountRender "   NoteCountRender.SettingsCtrl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Bui3clr-namespace:ZenithEngine.UI;assembly=ZenithEngine     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     /localclr-namespace:NoteCountRender     :xctk)http://schemas.xceed.com/wpf/xaml/toolkit       � 	Resources   �� MergedDictionaries �� Source �� 
TargetType   SubTabs �� BasedOn  SubTabItems��  ��  �� $; 5pack://siteoforigin:,,,/Languages/en/notecounter.xaml?��� $D >pack://application:,,,/ZenithEngine;component/UI/Material.xaml?�%:   '��       7   '��      7  �� " ��8   �� " ��8   .��� .+��� $��10,10,10,10q�.@���  
 Render#���  .��]� $��10q�.+�]� $	��Top=�.+��� $	��530��.+�]� $
��Left=���.  $��0,0,0,0q�$	��Top=�.+��� $
��Left=�  fontSize#���  $	��Top=�"   ZenithEngine.UI.NumberSelect  -     Name$ fontSize��   Value$ 1~�   Maximum$ 15360~�   Minimum$ 1~�$
��Left=�$��99��$
��Left=�$��5,0,0,0q�{��� *	IsEnabled+	 �� ElementName$	 notPreviewingOrRendering���� $
��Left=�  font#���  $	��Top=��� -   
 ��Name$
 
fontSelect��$
��Left=�$	��120��$��5,0,0,0q��� -   $
 	fontStyle��$
��Left=�$��10,0,0,0q�$	��120��]� $
��Left=���.  $��0,10,0,0q�$	��Top=�.+���   
alignments#���  $
��Left=�$	��Top=�$
��Left=��� -   $
 alignSelect��$
��Left=�$
��Left=�$��5,0,0,0q�$	��Top=�$��26�� �� SelectedIndex$ 0��.@���   	alTopLeft#���  ��   
alTopRight#���  ��   alBottomLeft#���  ��  	 alBottomRight#��� 	 ��  
 alTopSpread#��� 
 ��   alBottomSpread#���  ]� $
��Left=���.  $��0,10,0,0q�$	��Top=�.+��� -   $
 	templates��$
��Left=�$	��120���� -    ��Name$ reload�� 
 reload#���  $
��Left=�$��	10,0,10,0q�$
��Left=�$��5,0,0,0q��� -   $ 
openFolder��  
openFolder#���  $
��Left=�$��	10,0,10,0q�$
��Left=�$��5,0,0,0q��� -   $ 
newProfile��  saveNew#���  $
��Left=�$��15,0,0,0q�$��	10,0,10,0q�$
��Left=��� -	    ��Name$ profileName��$
��Left=�$
��Left=�$��5,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��110���� $��Right=�$��Right=�$
��.txt���� $��30,0,0,0q�.+���   thousandsSeparator#���  !   ZenithEngine.UI.BetterRadio  -
     Name$ 	useCommas��   	IsChecked .  
 commas   Text# �  $��0,0,0,5q�  -   $ 
useNothing��  nothing# �  $��0,0,0,5q�$   ZenithEngine.UI.BetterCheckbox  -     Name$ ZeroPadding��$��-4,2,0,0q�  ZeroPadding   Text# �  �� -   $ textTemplate�� �� AcceptsReturn . $	��Top=�$��0,10,0,0q�$
 Wrap=�$��TextBox����   SaveCSV#���  .���� $��10q�.+�  -   $��saveCsv��  
saveToFile# �  $��0,0,0,0q�]� $��26����. $��0,10,0,0q�.+��� $
��Left=�$��0,0,0,0q�  savePath#���  �� -   $ browseOutputSaveButton�� 
 browse#���  $
��Left=�$��10,0,0,0q�$��	20,0,20,0q��� -   $ csvPath��$��10,0,0,0q�{�.  $
 Wrap=�$�� ��]� $��26����. $��0,10,0,0q�.+��� $
��Left=�$��0,0,0,0q�  formatString#���  �� -   $ 	csvFormat��$��10,0,0,0q�$
 Wrap=�$�� ����   Padding#���  .���� $��10q�.+��  � ColumnDefinitions �� $��2* ��� $��2* ��� $��3* ��� $��2* ��� $��9* � � RowDefinitions �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��15 ��� $��30 �.+��� $��Right=�$��0,1,0,0q� 
 BPMint#���    -   $ BPMint��$��1��$��0��$ 3~�$ 5~�$ 1~�$��64��$
��Left=�$��	10,-2,0,0q�$��32��$	��Top=�{��� *	IsEnabled+$	 notPreviewingOrRendering���� $��2��$��0��$��Right=�$��10,1,0,0q�  BPMfrac#���    -   $ BPMDecPt��$��3��$��0��$ 2~�$ 12~�$ 0~�$��64��$
��Left=�$��	10,-2,0,0q�{��� *	IsEnabled+$	 notPreviewingOrRendering���� $��0��$��1��$��Right=�$��0,0,0,0q�  	NoteCount#���    -   $ 	NoteCount��$��1��$��1��$ 5~�$ 12~�$ 1~�$��64��$
��Left=�$��	10,-2,0,0q�{��� *	IsEnabled+$	 notPreviewingOrRendering���� $��0��$��2��$��Right=�$��0,0,0,0q�  	Polyphony#���    -   $ 	Polyphony��$��1��$��2��$ 3~�$ 9~�$ 1~�$��64��$
��Left=�$��	10,-2,0,0q�{��� *	IsEnabled+$	 notPreviewingOrRendering���� $��0��$��3��$��Right=�$��0,0,0,0q�  NPS#���    -   $	 NPS��$��1��$��3��$ 3~�$ 9~�$ 1~�$��64��$
��Left=�$��	10,-2,0,0q�{��� *	IsEnabled+$	 notPreviewingOrRendering���� $��0��$��4��$��Right=�$��0,0,0,0q� 	 Ticks#���    -   $ Ticks��$��1��$��4��$ 5~�$ 9~�$ 1~�$��64��$
��Left=�$��	10,-2,0,0q�{��� *	IsEnabled+$	 notPreviewingOrRendering���� $��0��$��5��$��Right=�$��0,0,0,0q�  Bars#���    -   $
 Bars��$��1��$��5��$ 3~�$ 5~�$ 1~�$��64��$
��Left=�$��	10,-2,0,0q�{��� *	IsEnabled+$	 notPreviewingOrRendering���� $��0��$��6��$��Right=�$��0,0,0,0q� 
  Frames#���     -   $ Frames��$��1��$��6��$ 5~�$ 5~�$ 1~�$��64��$
��Left=�$��	10,-2,0,0q�{��� *	IsEnabled+$	 notPreviewingOrRendering���� -   $ 
SetDefault��$��2��$��8��$��Center=� ! 
SetDefault#��� ! $��25��$��	20,2,20,2q�