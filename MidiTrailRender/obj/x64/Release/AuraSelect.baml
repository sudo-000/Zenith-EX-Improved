   M S B A M L   `   `   `  ���� G  CZenithEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG3clr-namespace:ZenithEngine.UI;assembly=ZenithEngineZenithEngine.UI  J FMIDITrailRender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MIDITrailRenderMIDITrailRender     MIDITrailRender.AuraSelect   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Bui3clr-namespace:ZenithEngine.UI;assembly=ZenithEngine     @d2http://schemas.microsoft.com/expression/blend/2008     /localclr-namespace:MIDITrailRender     :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     $��342.588��$��518.468��  � 	Resources   �� MergedDictionaries �� Source��  �� $9 3pack://siteoforigin:,,,/Languages/en/miditrail.xaml?��� $D >pack://application:,,,/ZenithEngine;component/UI/Material.xaml?�%    .���  � ColumnDefinitions �� $	��200 ��� .+��� -    ��Name$ imagePreview��$��1��$��10q�$��2��]� ��. $��10q�.+��� $	��Top=�   	imageHint#���   $��0q�$
��Wrap=�$	��Top=�$   ZenithEngine.UI.BetterCheckbox  -     Name$ auraEnabled��  auraEnabled   Text# �  $
��Left=�$��0,10,0,0q�$	��Top=�$	��Top=��� -    ��Name$ reload�� 
 reload#���  $��0,10,0,0q�$��26��$	��Top=�$	��Top=��� -   $ 
openFolder��$��Bottom=�  openAuraFolder#���  $��0,5,0,0q�$��26��$	��Top=�]� $��Bottom=�$
��Left=���.  $��0,0,0,5q�$	��Top=�.+���   auraStrength#���  $
��Left=�$��0q�$��Bottom=�$
��Left=�"   ZenithEngine.UI.NumberSelect  -     Name$ auraStrength��	   Value$	 1~�
   Maximum$
 3~�   Minimum$ 1~�$��5,0,0,2q�$
��Left=�$��77��$
��Left=��� -    ��Name$ 
imagesList��$	��0,5q�