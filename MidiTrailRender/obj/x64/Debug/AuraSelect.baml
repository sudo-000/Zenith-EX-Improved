   M S B A M L   `   `   `  ����G  CZenithEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG3clr-namespace:ZenithEngine.UI;assembly=ZenithEngineZenithEngine.UI  5      J FMIDITrailRender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MIDITrailRenderMIDITrailRender 5          MIDITrailRender.AuraSelect   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Bui3clr-namespace:ZenithEngine.UI;assembly=ZenithEngine     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:MIDITrailRender     5      :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     5      $��342.588��5	      $��518.468��60     � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $9 3pack://siteoforigin:,,,/Languages/en/miditrail.xaml?�6%   �� 5      $D >pack://application:,,,/ZenithEngine;component/UI/Material.xaml?�6%   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      .+�5   
   �� -   5         ��Name$ imagePreview��5      $��1��6&   $��10q�66   $��2��6B   ]� 5   
   ��. $��10q�6)   .+�5      �� $	��Top=�6      	imageHint#���   6-   $��0q�6P   $
��Wrap=�6[   $	��Top=�6o   $   ZenithEngine.UI.BetterCheckbox  5      -   5          Name$ auraEnabled��5         auraEnabled   Text# �  65   $
��Left=�6Z   $��0,10,0,0q�6u   $	��Top=�6�   $	��Top=�6�   �� 5      -   5         ��Name$ reload��5       
 reload#���  6%   $��0,10,0,0q�6H   $��26��6Z   $	��Top=�6f   $	��Top=�6�   �� 5      -   5        $ 
openFolder��5   -   $��Bottom=�6     openAuraFolder#���  6A   $��0,5,0,0q�6l   $��26��6}   $	��Top=�6�   6�   ]� 5      $��Bottom=�6   $
��Left=�60   ��.  $��0,0,0,5q�6a   $	��Top=�6r   .+�5      ��   auraStrength#���  6   $
��Left=�6A   $��0q�6\   $��Bottom=�6g   $
��Left=�6�   "   ZenithEngine.UI.NumberSelect  5      -   5          Name$ auraStrength��5   "   	   Value$	 1~�68   
   Maximum$
 3~�6B      Minimum$ 1~�6N   $��5,0,0,2q�6Z   $
��Left=�6k   $��77��6�   $
��Left=�6�   5       �� 5!      -   5         ��Name$ 
imagesList��5!      $	��0,5q�6*   67   5"      5$      5%      