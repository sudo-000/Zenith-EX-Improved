   M S B A M L   `   `   `  ����G  CZenithEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG3clr-namespace:ZenithEngine.UI;assembly=ZenithEngineZenithEngine.UI  5      I EScriptedRender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ScriptedRenderScriptedRender 5      A0clr-namespace:ZenithEngine;assembly=ZenithEngineZenithEngine  5      !   ScriptedRender.SettingsCtrl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Bui3clr-namespace:ZenithEngine.UI;assembly=ZenithEngine     5      .localclr-namespace:ScriptedRender     5      @bme0clr-namespace:ZenithEngine;assembly=ZenithEngine     5        � 	Resources  5       �� MergedDictionaries �� Source �� 
TargetType   SubTabs �� BasedOn  SubTabItems�� 5   
    5      �� 5       5      �� 5      $8 2pack://siteoforigin:,,,/Languages/en/scripted.xaml?�6-   5      �� 5      $D >pack://application:,,,/ZenithEngine;component/UI/Material.xaml?�6%   %`   '��       7   '�� "     7  �� 5      " ��6   8   6,   �� 5      " ��6   8   6)   5      .��5      � .+�5   
   �� $��10q�6   .@�5      ��   resourcesTab#���  6   .��5      � $��10q�6    � ColumnDefinitions 5      �� 5      $
��204* �6+   �� 5      $
��371* �6+   �� 5       $
��199* �6+   .+�5"      '   !ZenithEngine.NoteColorPalettePick  -   5          Name$ paletteList��5"   H   $��0q�6]   $��2��6h   $��2��6x   ]� 5#      ��. $��2��65   .+�5$      �� -   5         ��Name$ reloadListButton��5$   !     
reloadList#���  6;   $��0,0,0,0q�6b   $��26��6s   $	��Top=�6   6�   �� 5%      -   5        $ reloadPackButton��5%   !     reloadScript#���  6;   $��0,10,0,0q�6d   $��26��6v   $	��Top=�6�   6�   �� 5&      -   5        $ openFolderButton��5&   !     
openFolder#���  6;   $��0,10,0,0q�6b   $��26��6t   $��Bottom=�6�   6�   �� 5'      -   5         ��Name$ 
pluginList��5'   "   $��0,10,0,0q�66   6H   5(      ]� 5)      $��1��6    ��. .+�5*      �� -   5        	 ��Name$	 
pluginDesc��5*   a   $	��100��6"   $��Bottom=�62   
 �� TextAlignment$
 Center=�6J   $��1��6u   $��
10,10,10,0q�6�    �� TextWrapping$
 Wrap=�6�   {�.  $��1��6�   �� 5+      -   5         ��Name$ 
previewImg��5+       $��1��64   $��	10,0,10,0q�6D   5,      5-      5.      �� 5/      -   5        $��	switchTab��5/   =     settingsTab#���  6   .��50      ]� $��
10,10,10,0q�6   .+�51      ]� -	   5        $��profileDock��51   7   $y�	Collapsed=�6    $	��Top=�6J   ��.  $��0,0,0,10q�6u   .+�52      ��   profile#���  6    �� 53      -
   5        $��profileSelect��53   Q   $	��120��6f   $��5,0,0,0q�6r   6�   �� 54      -   5        $��profileName��54   "   $��5,0,0,0q�65   $	��120��6F   �� 55      -   5        $��saveProfile��55   !   $��5,0,0,0q�64   $
��20,0q�6E     save#���  6T   6u   �� 56      -   5        $��deleteProfile��56   !   $��5,0,0,0q�66   $
��20,0q�6G    
	 delete#��� 	 6V   6y   �� 57      -   5        $��profileDefaults��57   !   $��Right=�68   $
��20,0q�6O    
 defaults#��� 
 6^   6�   58      ]� 59      -   5        $��settingsPanel��59       ��.  5;      5<      5=      �� 5>        miscTab#���  6   .��5?      �� $��10q�6   .+�5@      ]� $
��Left=�6    ��.  $��0,0,0,0q�6Q   $	��Top=�6b   .+�5A      ��   	firstNote#���  6    $
��Left=�6F   $	��Top=�6a   $
��Left=�6y   "   ZenithEngine.UI.NumberSelect  5B      -   5          Name$ 	firstNote��5B   *      Value$ 0~�6=      Maximum$	 254~�6G      Minimum$ 0~�6U   $��5,0,0,0q�6a   $
��Left=�6r   $��80��6�   $��26��6�   $	��Top=�6�   6�   �� 5C        lastNote#���  6    $
��Left=�6E   $��10,0,0,0q�6`   $	��Top=�6r     5D      -   5        $ lastNote��5D   *   $	 127~�6<   $	 255~�6H   $ 1~�6V   $��5,0,0,0q�6b   $
��Left=�6s   $��80��6�   $��26��6�   $	��Top=�6�   6�   5E      ]� 5F      $
��Left=�6    ��.  $��0,10,0,0q�6Q   $	��Top=�6c   $	��528��6{   .+�5G      ��   noteScreenTime#���  6    $
��Left=�6K   $��0,0,0,0q�6f   $	��Top=�6w   !   ZenithEngine.UI.ValueSlider  5H      -   5          Name$ noteDeltaScreenTime��5H   )      Maximum$ 12Q�6F      DecimalPoints$ 2��6S      Minimum$ 2Q�6e      TrueMin$ 1~�6q      TrueMax$ 100000~�6}   $	��305��6�   $	��Top=�6�   5I      5J      5K      5L      5M      5N      